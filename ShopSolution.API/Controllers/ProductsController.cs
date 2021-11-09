using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopSolution.API.Dtos;
using ShopSolution.API.Errors;
using ShopSolution.API.Helpers;
using ShopSolution.Core.Entities;
using ShopSolution.Core.Interfaces;
using ShopSolution.Core.Specifications;

namespace ShopSolution.API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> productBrandRepository,
            IGenericRepository<ProductType> productTypeRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);
            var totalItems = await _productRepository.CountAsync(spec);
            var products = await _productRepository.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>,
                IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, 
                productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id:int}")] 
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepository.GetEntityWithSpec(spec);
            if (product == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<Product, ProductToReturnDto>(product); 
        } 

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepository.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepository.ListAllAsync());
        }
    }
} 