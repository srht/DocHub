using DocHub.Common.DTO;
using DocHub.Common.DTO.Users;
using DocHub.Common.Enums;
using DocHub.Service.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DocHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public ICategoriesService CategoriesService { get; }

        public CategoriesController(ICategoriesService categoriesService)
        {
            CategoriesService = categoriesService;
        }
        // GET: api/<DocumentsController>
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> Get()
        {
            return CategoriesService.GetCategories().ToList();
        }

        // GET api/<DocumentsController>/5
        [HttpGet("{id}")]
        public async Task<CategoryDto> Get(int id)
        {
            var foundDocument=CategoriesService.GetCategoryDto(id);
            return foundDocument;
        }

        // POST api/<DocumentsController>
        /// <summary>
        /// Creates new document from the object passed from the request body. 
        /// </summary>
        /// <param name="value">DocumentDto type contains document specifications.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task Post([FromBody] CategoryDto value)
        {
            CategoriesService.AddCategory(value);
        }

        // PUT api/<DocumentsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] CategoryDto value)
        {
            CategoriesService.UpdateCategory(value);
        }

        // DELETE api/<DocumentsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            CategoriesService.DeleteCategory(id);
        }
    }
}
