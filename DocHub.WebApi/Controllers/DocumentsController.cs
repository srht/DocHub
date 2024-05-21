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
    public class DocumentsController : ControllerBase
    {
        public IDocumentsService DocumentsService { get; }

        public DocumentsController(IDocumentsService documentsService)
        {
            DocumentsService = documentsService;
        }
        // GET: api/<DocumentsController>
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IEnumerable<DocumentDto>> Get()
        {
            return DocumentsService.GetDocuments().ToList();
        }

        // GET api/<DocumentsController>/5
        [HttpGet("{id}")]
        public DocumentDto Get(string id)
        {
            var guid = Guid.Parse(id);
            var foundDocument=DocumentsService.GetDocument(guid);
            return foundDocument;
        }

        // POST api/<DocumentsController>
        /// <summary>
        /// Creates new document from the object passed from the request body. 
        /// </summary>
        /// <param name="value">DocumentDto type contains document specifications.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task Post([FromBody] DocumentDto value)
        {
            DocumentsService.AddDocument(value);
        }

        // PUT api/<DocumentsController>/5
        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody] DocumentDto value)
        {
            DocumentsService.UpdateDocument(value);
        }

        // DELETE api/<DocumentsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            var guid = Guid.Parse(id);
            DocumentsService.DeleteDocument(guid);
        }
    }
}
