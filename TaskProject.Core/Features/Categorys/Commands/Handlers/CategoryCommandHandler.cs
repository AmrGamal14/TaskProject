using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Bases;
using TaskProject.Core.Features.Categorys.Commands.Models;
using TaskProject.Core.Features.Products.Commands.Models;
using TaskProject.Core.Resources;
using TaskProject.Data.Entities;
using TaskProject.Service.Abstracts;
using TaskProject.Service.Implementations;

namespace TaskProject.Core.Features.Categorys.Commands.Handlers
{
    public class CategoryCommandHandler : ResponseHandler, IRequestHandler<AddCategoryCommand, Response<string>>, 
                                                           IRequestHandler<DeleteCategoryCommand, Response<string>>
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public CategoryCommandHandler(IMapper mapper, ICategoryService categoryService, IStringLocalizer<SharedResources> stringLocalizer)
        {
            _categoryService=categoryService;
            _mapper=mapper;
            _stringLocalizer=stringLocalizer;
        }

        public async Task<Response<string>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var categorymapper = _mapper.Map<Category>(request);
            var result = await _categoryService.AddAsync(categorymapper);
            return success(result);
        }
      

        public async Task<Response<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var product = await _categoryService.GetProductByIdasync(request.Id);
            if (product==null) return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var result = await _categoryService.DeleteAsync(product);
            return success("");
        }
    }
}
