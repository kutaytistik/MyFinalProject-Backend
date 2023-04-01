using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Loosely Coupled --->Bir bağımlılığı var ama soyuta bağımlı
        //Fieldların default değeri private
        //private IProductService _productService
        //naming convention ---> _productService
        //IoC Container ----> Inversion of Control

        IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult Get()
        {
            //Dependency Chain
            var result = _productService.GetAll();
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("getbyid")]
        public IActionResult Get(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }


    //C#'da  --> Attribute --> [ApiController]
    //Attribute bir class ile ilgili bilgi verme imzalama yöntemidir [ApiController] altındaki classın bir controller olduğunu söylüyoruz
    //Javada --> Annotation
    //Bir Sınıfın Controller Olabilmesi İçin ControllerBase'den Inherit edilmiş olması gerekli
    //Route bize nasıl istekte bulunacağını gösterir
    //HttpGet Attribute 
    //Breakpoint

}
