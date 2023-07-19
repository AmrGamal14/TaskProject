using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Bases;
using TaskProject.Core.Features.Categorys.Queries.Models;
using TaskProject.Core.Features.Categorys.Queries.Results;
using TaskProject.Core.Features.Products.Queries.Models;
using TaskProject.Core.Features.Products.Queries.Results;
using TaskProject.Data.Entities;
using TaskProject.Service.Abstracts;
using TaskProject.Service.Implementations;

namespace TaskProject.Core.Features.Categorys.Queries.Handlers
{
    public class CategoryHandler : ResponseHandler, IRequestHandler<GetCategoryListQuery, Response<List<GetCategoryListResponse>>>
    {
        #region Fields
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructors
        public CategoryHandler(ICategoryService categoryService , IMapper mapper)
        {
            _categoryService=categoryService;
            _mapper=mapper; 
        }
        #endregion
        #region Handle Functions
        public async Task<Response<List<GetCategoryListResponse>>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
          
            var categoryList = await _categoryService.GetCategorysListAsync();
            var categoryListMapper = _mapper.Map<List<GetCategoryListResponse>>(categoryList);
              return success(categoryListMapper);
        }
        #endregion
    }
}