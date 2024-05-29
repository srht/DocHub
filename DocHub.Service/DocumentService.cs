using AutoMapper;
using DocHub.Common.DTO;
using DocHub.Common.Enums;
using DocHub.Core.Entities;
using DocHub.Data.Abstracts;
using DocHub.Data.Migrations;
using DocHub.Service.Abstracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DocHub.Service
{
    public class DocumentService : IDocumentsService
    {
        public DocumentService(IDocumentRepository documentRepository, ICategoriesRepository categoriesRepository, IMapper mapper)
        {
            DocumentRepository = documentRepository;
            CategoriesRepository = categoriesRepository;
            Mapper = mapper;
        }

        public IDocumentRepository DocumentRepository { get; }
        public ICategoriesRepository CategoriesRepository { get; }
        public IMapper Mapper { get; }
        public ILogger Logger { get; }

        public void AddDocument(DocumentDto documentDto)
        {
            var documentDb = new DDocument();
            documentDb = Mapper.Map<DDocument>(documentDto);
            //documentDb.Id = Guid.NewGuid();
            documentDb.DocumentType = documentDto.DocumentType.HasValue ? documentDto.DocumentType.Value : DocumentTypes.Text;
            documentDb.CreatedAt = DateTime.Now;

            DocumentRepository.Insert(documentDb);
           
        }

        public void DeleteDocument(Guid id)
        {
            DocumentRepository.SoftDelete(id);
        }

        public DocumentDto GetDocument(Guid id)
        {
            var documentDb=DocumentRepository.GetDocumentById(id);
            var document = new DocumentDto();
            document = Mapper.Map<DocumentDto>(documentDb);
            return document;
        }

        public List<DocumentDto> GetDocuments(string query="")
        {
            query = query.ToLower();
            var documents = DocumentRepository.GetList("Tags","Categories")
                .Where(i=>i.Title.ToLower().Contains(query))
                .Where(i=>!i.IsDeleted).Select(documentDb =>Mapper.Map<DocumentDto>(documentDb)).ToList();

            return documents;
        }

        public List<DocumentDto> GetDocumentsByCategory(int categoryId)
        {
            var documents = DocumentRepository.GetList("Tags", "Categories")
                .Where(i => i.Categories.Any(c=>c.Id==categoryId)).Where(i => !i.IsDeleted)
                .Select(documentDb => Mapper.Map<DocumentDto>(documentDb)).ToList();

            return documents;
        }

        public void UpdateDocument(DocumentDto documentDto)
        {
            if (documentDto == null || documentDto.Id==null)
                throw new Exception("No document passed to update");

            var documentDb = DocumentRepository.GetDocumentById(documentDto.Id.Value);
            if (documentDb == null)
                throw new Exception("Document not found with document id: " + documentDto.Id);

            if (documentDb.Categories == null)
                documentDb.Categories = new List<Category>();
            if(!string.IsNullOrEmpty(documentDto.Title))
            documentDb.Title=documentDto.Title;
            documentDb.UpdatedAt = DateTime.Now;
            documentDb.DocumentType = documentDto.DocumentType.HasValue? documentDto.DocumentType.Value:DocumentTypes.Text;
            if (!string.IsNullOrEmpty(documentDto.Description))
                documentDb.Description = documentDto.Description;
            if (!string.IsNullOrEmpty(documentDto.FilePath))
                documentDb.FilePath = documentDto.FilePath;
            if (documentDto.Tags != null && documentDto.Tags.Any())
                documentDb.Tags = documentDto?.Tags?.Select(i => new Tag { Id = i.Id, Name = i.Name }).ToList();
            
            if (documentDto.Categories != null && documentDto.Categories.Any()) {
                var broughtCategories = documentDto.Categories;
                foreach (var c in broughtCategories)
                {
                    var categoryDb = CategoriesRepository.GetObjectByIntId(c.Id);
                    if(categoryDb!=null)
                    documentDb.Categories.Add(categoryDb);
                }
            }

            DocumentRepository.Update(documentDb);
        }
    }
}
