using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Data;
using ShopApp.Api.Dtos.ProductDtos;
using ShopApp.Core.Entities;
using ShopApp.Core.Repositories;

namespace ShopApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;

        public ProductsController(IProductRepository productRepository,IBrandRepository brandRepository)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
        }
        [HttpPost("")]
        public IActionResult Create(ProductPostDto postDto)
        {
            if (!_brandRepository.IsExist(x => x.Id == postDto.BrandId))
            {
                ModelState.AddModelError("BrandId", $"Brand not found by Id {postDto.BrandId}");
                return BadRequest(ModelState);
            }
            Product product = new Product
            {
                BrandId = postDto.BrandId,
                Name = postDto.Name,
                CostPrice = postDto.CostPrice,
                SalePrice = postDto.SalePrice,
                CreatedAt = DateTime.UtcNow.AddHours(4),
                ModifiedAt = DateTime.UtcNow.AddHours(4),
            };
            _productRepository.Add(product);
            _productRepository.Commit();

            return StatusCode(201, new { Id = product.Id });
        }
        [HttpGet("{id}")]
        public ActionResult<ProductGetDto> Get(int id)
        {
            Product product = _productRepository.Get(x => x.Id == id, "Brand");
            if(product == null) return NotFound();

            ProductGetDto productDto = new ProductGetDto
            {
                Name = product.Name,
                CostPrice = product.CostPrice,
                SalePrice = product.SalePrice,
                Brand = new BrandInProductGetDto
                {
                    Id = product.Id,
                    Name = product.Brand.Name,
                }
            };
            return Ok(productDto);
        }
        [HttpGet("all")]
        public ActionResult<List<ProductGetAllItemDto>> GetAll()
        {
            var productDtos = _productRepository.GetQueryable(x => true, "Brand").Select(x => new ProductGetAllItemDto { Id = x.Id, Name = x.Name, BrandName = x.Brand.Name }).ToList();
            return Ok(productDtos);
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id, ProductPutDto putDto)
        {
            Product product = _productRepository.Get(x => x.Id == id);
            if (product == null) return NotFound();

            if (product.Name != putDto.Name && _productRepository.IsExist(x => x.Name == putDto.Name))
            {
                ModelState.AddModelError("Name", "Name is already exit");
                return BadRequest(ModelState);
            }
            product.Name = putDto.Name;
            _productRepository.Commit();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _productRepository.Get(x => x.Id == id);
            if (product == null) return NotFound();

            _productRepository.Remove(product);
            _productRepository.Commit();

            return NoContent();
        }
    }
}
