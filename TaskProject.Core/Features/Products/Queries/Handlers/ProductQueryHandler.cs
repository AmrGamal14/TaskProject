using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Bases;
using TaskProject.Core.Features.Products.Queries.Models;
using TaskProject.Core.Features.Products.Queries.Results;
using TaskProject.Core.Resources;
using TaskProject.Core.Wrappers;
using TaskProject.Data.Entities;
using TaskProject.Service.Abstracts;

namespace TaskProject.Core.Features.Products.Queries.Handlers
{
    public class ProductQueryHandler : ResponseHandler, IRequestHandler<GetProductListQuery, Response<List<GetProductListResponse>>>,
                                                        IRequestHandler<GetProductByIdQuery, Response<GetProductListResponse>>,
                                                        IRequestHandler<GetProductListByCategoryId, Response<List<GetProductListByCategoryIDResponse>>>,
                                                        IRequestHandler<GetProductPaginatedListQuery,PaginatedResult<GetProductPaginatedListResponse>>

    {
        #region Fields
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion
        #region Constructors
        public ProductQueryHandler(IProductService productService,
                                    IMapper mapper,
                                     IStringLocalizer<SharedResources> stringLocalizer)
        {
            _productService=productService;
            _mapper=mapper;
            _stringLocalizer=stringLocalizer;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<List<GetProductListResponse>>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productService.GetProductsListAsync();
            var ProductListMapper = _mapper.Map<List<GetProductListResponse>>(productList);
            return success (ProductListMapper);
        }
        public async Task<Response<GetProductListResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductByIdasync(request.productId);
            var ProductMapper = _mapper.Map<GetProductListResponse>(product);
            return success(ProductMapper);
        }
        public async Task<Response<List<GetProductListByCategoryIDResponse>>> Handle(GetProductListByCategoryId request, CancellationToken cancellationToken)
        {
            var productList = await _productService.GetProductListByCategoryId(request.Id);
            var ProductListMapper = _mapper.Map<List<GetProductListByCategoryIDResponse>>(productList);
            return success(ProductListMapper);
        }

        public async Task<PaginatedResult<GetProductPaginatedListResponse>> Handle(GetProductPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Product, GetProductPaginatedListResponse>> expression = e => new GetProductPaginatedListResponse(e.Id,e.Name,e.Price,e.Quantitylimit, e.Availablequantity,e.briefdescription, e.ImageUrl);
            var querable = _productService.GetProductQuerable();
            var paginatedList = await querable.Select(expression).ToPaginatedListAsynnc(request.PageNumber, request.PageSize);
            return paginatedList;
        }
        #endregion
    }
}
