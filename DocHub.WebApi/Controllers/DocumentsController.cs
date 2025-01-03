﻿using DocHub.Common.DTO;
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
        [HttpGet("list")]
        public async Task<IEnumerable<DocumentDto>> List(string q="")
        {
            return await DocumentsService.GetDocumentsAsync(q);
        }

        // GET: api/<DocumentsController>
        //[Authorize(Roles = "Admin")]
        [HttpGet("category")]
        public IEnumerable<DocumentDto> Category(int categoryId)
        {
            var results=  DocumentsService.GetDocumentsByCategory(categoryId);
            return results;
        }


        // GET api/<DocumentsController>/5
        [HttpGet("{id}")]
        public async Task<DocumentDto> Get(string id)
        {
            var guid = Guid.Parse(id);
            var foundDocument=await DocumentsService.GetDocumentAsync(guid);
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
            await DocumentsService.AddDocumentAsync(value);
        }

        // PUT api/<DocumentsController>/5
        [HttpPut("{id}")]
        public async Task<DocumentDto> Put(string id, [FromBody] DocumentDto value)
        {
           var retval=await DocumentsService.UpdateDocumentAsync(value);
            return retval;
        }

        // DELETE api/<DocumentsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            var guid = Guid.Parse(id);
            await DocumentsService.DeleteDocumentAsync(guid);
        }
    }
}
