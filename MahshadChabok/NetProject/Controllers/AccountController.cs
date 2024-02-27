using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using AspNetCoreRateLimit;
using NetProject.Data;
using System.Text.Json;
using NetProject.Dto;
using NetProject.model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetProject.Controllers
{
    [Route("api/[controller]")]
   
    [ApiController]
    //[RateLimit]
    public class AccountController : ControllerBase
    {
        private readonly AccountDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        private readonly IMemoryCache _inmemcache;

        public AccountController(AccountDbContext context, IMapper mapper, IDistributedCache cache, IMemoryCache inmemcache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
            _inmemcache = inmemcache;
        }

        [HttpPost("Add")]
        //[RequireRateLimiting("fixed")]
        public IActionResult AddUser([FromBody] AccountDto accountDto)
        {
            if (accountDto == null)
            {
                return BadRequest("Invalid user data");
            }

            Account account = _mapper.Map<Account>(accountDto);

            _context.Accounts.Add(account);
            _context.SaveChanges();

         
            _cache.Remove("GetAll");

            return Ok("User added successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cachedAccountData = await _cache.GetStringAsync($"Account_{id}");

            if (cachedAccountData != null)
            {
                return Ok(cachedAccountData);
            }

            var account = _context.Accounts.Find(id);

            if (account == null)
            {
                return NotFound($"Account with id {id} not found");
            }

            var accountJson = JsonSerializer.Serialize(account);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(30)
            };
            await _cache.SetStringAsync($"Account_{id}", accountJson, cacheOptions);

            return Ok(account);
        }

        [HttpGet("{id1}/{id2}")]
        public IActionResult Get(int id1, int id2)
        {
            if (_inmemcache.TryGetValue($"Account_{id1}", out Account accountFromCache))
            {
                if (accountFromCache.OwnerId == id2)
                {
                    return Ok(accountFromCache);
                }

                return NotFound($"Account with id {accountFromCache.Id} does not have OwnerId equal to {id2}");
            }

            var account = _context.Accounts.Find(id1);

            if (account == null)
            {
                return NotFound($"Account with id {id1} not found");
            }

            var cacheOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(30)
            };
            _inmemcache.Set($"Account_{id1}", account, cacheOptions);

            if (account.OwnerId == id2)
            {
                return Ok(account);
            }

            return NotFound($"Account with id {account.Id} does not have OwnerId equal to {id2}");
        }

        [HttpGet("GetAll")]
        [ResponseCache(Duration = 30)] // Cache for 30 seconds
        public IActionResult GetAll()
        {
            
            var cachedResult = _cache.GetString("GetAll");
            if (cachedResult != null)
            {
                return Ok(JsonSerializer.Deserialize<List<AccountDto>>(cachedResult));
            }

            var accounts = _context.Accounts.ToList();
            var accountDtos = _mapper.Map<List<AccountDto>>(accounts);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(30)
            };
            _cache.SetString("GetAll", JsonSerializer.Serialize(accountDtos), cacheOptions);

            return Ok(accountDtos);
        }

        [HttpGet("GetByOwnerName/{name}")]
        public IActionResult GetByOwnerName(string name)
        {
            var accounts = _context.Accounts
                .Where(account => account.Owner == name)
                .ToList();

            var accountDtos = _mapper.Map<List<AccountDto>>(accounts);

            return Ok(accountDtos);
        }
    }
}
