using DocHub.Common.DTO;
using DocHub.Common.Enums;
using DocHub.Service.Abstracts;
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
        [HttpGet]
        public IEnumerable<DocumentDto> Get()
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
        [HttpPost]
        public void Post([FromBody] DocumentDto value)
        {
            DocumentsService.AddDocument(value);
        }

        // PATCH api/<DocumentsController>/5
        [HttpPatch("{id}")]
        public void Patch(string id, [FromBody] DocumentDto value)
        {
            DocumentsService.UpdateDocument(value);
        }

        // PUT api/<DocumentsController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] DocumentDto value)
        {
            DocumentsService.UpdateDocument(value);
        }

        // DELETE api/<DocumentsController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var guid = Guid.Parse(id);
            DocumentsService.DeleteDocument(guid);
        }
    }
}
