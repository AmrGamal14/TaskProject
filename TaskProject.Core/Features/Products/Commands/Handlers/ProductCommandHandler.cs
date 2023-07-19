using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using System.IO;
using TaskProject.Core.Bases;
using TaskProject.Core.Features.Products.Commands.Models;
using TaskProject.Core.Resources;
using TaskProject.Data.Entities;
using TaskProject.Service.Abstracts;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace TaskProject.Core.Features.Products.Commands.Handlers
{
    public class ProductCommandHandler : ResponseHandler, IRequestHandler<AddProductCommand,Response<string>>
                                                        , IRequestHandler<EditProductCommand, Response<string>>
                                                        , IRequestHandler<DeleteProductCommand, Response<string>>
       
    {
        #region Fields
        private readonly IProductService _productService;
        private readonly IHostingEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
       
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public ProductCommandHandler(IProductService productService,IMapper mapper, IHostingEnvironment hostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<SharedResources> stringLocalizer)

        {
            _productService = productService;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
           
        }

        public async Task<Response<string>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var FolderName = "Image";
            var FileName = request.Image.FileName;


            var path = Path.Combine(_hostEnvironment.WebRootPath, FolderName, FileName);
            if(request.Image != null)
            {
                using (FileStream File = new FileStream(path,FileMode.Create))
                {
                    await request.Image.CopyToAsync(File, cancellationToken);
                }
            }
            var location = GenerateUrl(_httpContextAccessor, FolderName, FileName);

            var productmapper = _mapper.Map<Product>(request);

            productmapper.ImageUrl = location.AbsoluteUri;
            productmapper.ImagePath = location.AbsolutePath;

            var  result = await _productService.AddAsync(productmapper);
            return success(result);
        }
        public static Uri GenerateUrl(IHttpContextAccessor accessor, string folder, string file)
        {
            if(!String.IsNullOrEmpty(folder) && !String.IsNullOrEmpty(file))
            {
                Uri uri = new Uri($"{accessor.HttpContext.Request.Scheme}://{accessor.HttpContext.Request.Host}/{folder}/{file}");
                return uri;
            }
            return null;
        }

        public async Task<Response<string>> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var oldProduct = await _productService.GetProductByIdasync(request.Id);
            if (oldProduct == null)
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var FolderName = "Image";
            var FileName = request.Image.FileName;

            if (oldProduct.ImagePath != null)
            {
                //delete old image 
                FileInfo oldImage = new FileInfo(oldProduct.ImagePath);
                if (oldImage.Exists) oldImage.Delete();
            }
            //create new one
            var path = Path.Combine(_hostEnvironment.WebRootPath, FolderName, FileName);
            if (request.Image != null)
            {
                using (FileStream File = new FileStream(path, FileMode.Create))
                {
                    await request.Image.CopyToAsync(File, cancellationToken);
                }
            }
            var location = GenerateUrl(_httpContextAccessor, FolderName, FileName);
            

            var Productmapper = _mapper.Map<Product>(request);
            Productmapper.ImageUrl = location.AbsoluteUri;
            Productmapper.ImagePath = location.AbsolutePath;
            var result = await _productService.EditAsync(Productmapper);
            if (result=="Success") return success("Edit Successfully");
            else return BadRequest<string>();

        }

        public async Task<Response<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductByIdasync(request.Id);
            if (product==null) return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var result = await _productService.DeleteAsync(product);
            return success("");

        }
        #endregion

      

    }
}
