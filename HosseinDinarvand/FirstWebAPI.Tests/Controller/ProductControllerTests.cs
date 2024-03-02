using AutoMapper;
using FakeItEasy;
using FirstWeb.API.Controllers;
using FirstWeb.API.Model.Domain;
using FirstWeb.API.Model.DTO;
using FirstWeb.API.Model.DTO.Product;
using FirstWeb.API.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebAPI.Tests.Controller
{
    public class ProductControllerTests
    {
        private readonly IProductRepositoryEFCore _productRepository;
        private readonly IMapper _mapper;
        public ProductControllerTests()
        {
            this._productRepository = A.Fake<IProductRepositoryEFCore>();
            this._mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void ProductController_GetAll_ReturnOk()
        {
            //Arrange
            var products = A.Fake<ICollection<Product>>();
            var productList = A.Fake<List<ProductDto>>();
            A.CallTo(() => _mapper.Map<List<ProductDto>>(products)).Returns(productList);

            var controller = new ProductController(_productRepository,_mapper);
            //Act
            var result = controller.GetAll();

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void ProductController_CreateProduct_ReturnActionResult()
        {
            //Arrange
            var productDomain = A.Fake<Product>();
            var ProductDto = A.Fake<ProductDto>();
            var addProductDto = A.Fake<AddProductRequestDto>();
            var products = A.Fake<ICollection<ProductDto>>();
            var productList = A.Fake<IList<ProductDto>>();
            A.CallTo(() => _mapper.Map<Product>(addProductDto)).Returns(productDomain);
            A.CallTo(() => _productRepository.CreateAsync(productDomain)).Returns(productDomain);
            var controller = new ProductController(_productRepository,_mapper);
            //Act
            var result = controller.Create(addProductDto);

            //Assert
            result.Should().NotBeNull();
        }
    }
}
