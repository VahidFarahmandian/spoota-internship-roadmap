using AutoMapper;
using FirstWeb.API.CustomActionFilters;
using FirstWeb.API.Model.Domain;
using FirstWeb.API.Model.DTO;
using FirstWeb.API.Repositories;
using FirstWeb.API.Repositories.ADO.Net;
using FirstWeb.API.Repositories.Dapper;
using FirstWeb.API.Services;
using FirstWeb.API.Services.In_Memory_Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace FirstWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepositoryEFCore productRepositoryEFCore;
        private readonly IProductRepoitoryDapper productRepositoryDapper;
        private readonly IProductRepositoryADO productRepositoryADO;
        private readonly ICacheServiceDistributed cacheService;
        private readonly ICacheServiceInMemory cacheServiceInMemory;
        private readonly IMapper mapper;
        public ProductController(
            IProductRepositoryEFCore _productRepositoryEFCore,
            IProductRepoitoryDapper _productRepositoryDapper,
            ICacheServiceDistributed _cacheService,
            ICacheServiceInMemory _cacheServiceInMemory,
            IProductRepositoryADO _productRepositoryADO,
            IMapper _mapper)
        {
            this.productRepositoryEFCore = _productRepositoryEFCore;
            this.productRepositoryDapper = _productRepositoryDapper;
            this.productRepositoryADO = _productRepositoryADO;
            this.cacheService = _cacheService;
            this.cacheServiceInMemory = _cacheServiceInMemory;
            this.mapper = _mapper;
        }

        [HttpGet()]
        [Route("all")]
        [OutputCache(PolicyName = "ExpireIn30s")]
        [ResponseCache(Duration =180,Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> GetAll()
        {
            // Check cache data In local Memory
            var cacheData = cacheServiceInMemory.getData<IEnumerable<Product>>("products");

            if (cacheData != null && cacheData.Count() > 0)
                return Ok(cacheData);

            // Set data to cache In local Memory
            cacheData = await productRepositoryDapper.GetAllAsync();

            var expiryTime = DateTimeOffset.Now.AddMinutes(1);

            cacheServiceInMemory.setData<IEnumerable<Product>>("products", cacheData, expiryTime);

            // Get Data Form Database - Domain models
            var productsDomain = await productRepositoryDapper.GetAllAsync();

            // Return product Dto Model
            return Ok(mapper.Map<List<ProductDto>>(productsDomain));
        }

        [HttpGet]
        [Route("{id}")]
        [ValidateModel]
        [OutputCache(PolicyName = "evict")]
        [ResponseCache(Duration = 180,Location = ResponseCacheLocation.Client,NoStore = true)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // Check cache data exist
            var cacheData = cacheService.getData<Product>($"product{id}");

            if (cacheData != null)
                return Ok(cacheData);

            // Get product by id
            var productDomain = await productRepositoryDapper.GetByIdAsync(id);
            // Set data to cache
            var expiryTime = DateTimeOffset.Now.AddMinutes(1);
            cacheService.setData<Product>($"product{id}", productDomain, expiryTime);

            // Check product domain exist
            if (productDomain == null)
                return NotFound();

            // Return Dto back to client
            return Ok(mapper.Map<ProductDto>(productDomain));

        }

        [HttpGet]
        [Route("name")]
        [ValidateModel]
        [OutputCache(PolicyName = "evict", VaryByQueryKeys = new[] { "name" })]
        [ResponseCache(Duration = 180,Location = ResponseCacheLocation.None,NoStore = true)]
        public IActionResult GetByName(string name)
        {
            // Check cache data
            var cacheData = cacheService.getData<Product>($"productName:{name}");
            if (cacheData != null)
                return Ok(cacheData);

            // Get data by ADO.Net
            var productDomain = productRepositoryADO.getByName(name);

            // Exist data check
            if (productDomain == null)
                return NotFound();

            // Set data to redis cache
            var expiryTime = DateTimeOffset.Now.AddMinutes(1);
            cacheService.setData<Product>($"productName:{name}", productDomain, expiryTime);

            //Return product Dto to Client
            return Ok(mapper.Map<ProductDto>(productDomain));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddProductRequestDto addProductRequestDto, IOutputCacheStore cache)
        {
            // Map DTO to Domain Model
            var productDomainModel = mapper.Map<Product>(addProductRequestDto);

            // Create data
            productDomainModel = await productRepositoryEFCore.CreateAsync(productDomainModel);

            // Set product to cache
            var expiryTime = DateTimeOffset.Now.AddMinutes(1);
            cacheService.setData<Product>($"product{productDomainModel.Id}", productDomainModel, expiryTime);

            // Map Domain Model to DTO
            var productDto = mapper.Map<ProductDto>(productDomainModel);

            // Evict product by tag
            await cache.EvictByTagAsync("tag-product", default);

            return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);

        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductRequestDto updateProductRequestDto)
        {
            // Map DTO request to Domain Model
            var productDomainModel = mapper.Map<Product>(updateProductRequestDto);

            // Update Domain Model by id
            productDomainModel = await productRepositoryEFCore.UpdateAsync(id, productDomainModel);

            // Check Domain Model exist
            if (productDomainModel == null)
                return NotFound();

            // Map Domain Model to DTO
            var ProductDto = mapper.Map<ProductDto>(productDomainModel);

            return Ok(ProductDto);
        }

        [HttpDelete]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IResult> Delete([FromRoute] int id)
        {
            // Delete data by id
            var productDomainModel = await productRepositoryEFCore.DeleteAsync(id);

            // Remove data from cache
            cacheService.removeData($"product{id}");

            // Check data exist
            if (productDomainModel == null)
                return Results.NotFound();

            // Map Domain Model to DTO
            var productDto = mapper.Map<ProductDto>(productDomainModel);

            return Results.Created<ProductDto>("The product has been deleted.", productDto);
        }
    }
}
