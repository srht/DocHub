using DocHub.Common.DTO;
using DocHub.Common.Enums;
using DocHub.Core.Entities;
using DocHub.Data.Abstracts;
using DocHub.Service.Abstracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Service
{
    public class DocumentService : IDocumentsService
    {
        public DocumentService(IDocumentRepository documentRepository, ICategoriesRepository categoriesRepository)
        {
            DocumentRepository = documentRepository;
            CategoriesRepository = categoriesRepository;
        }

        public IDocumentRepository DocumentRepository { get; }
        public ICategoriesRepository CategoriesRepository { get; }
        public ILogger Logger { get; }

        public void AddDocument(DocumentDto documentDto)
        {
            var documentDb = new DDocument();
            documentDb.Id = Guid.NewGuid();
            documentDb.Title = documentDto.Title;
            documentDb.DocumentType = documentDto.DocumentType.HasValue ? documentDto.DocumentType.Value : DocumentTypes.Text;
            documentDb.Description = documentDto.Description;
            documentDb.FilePath = documentDto.FilePath;
            documentDb.CreatedAt = DateTime.Now;
            documentDb.Tags = documentDto.Tags?.Select(i=>new Tag { Id=i.Id, Name=i.Name })?.ToList();

            DocumentRepository.Insert(documentDb);
           
        }

        public void DeleteDocument(Guid id)
        {
            DocumentRepository.SoftDelete(id);
        }

        public DocumentDto GetDocument(Guid id)
        {
            var documentDb=DocumentRepository.GetObjectById(id);
            var document = new DocumentDto();
            document.Id=documentDb.Id;
            document.Title = documentDb.Title;
            document.UpdatedAt = documentDb.UpdatedAt;
            document.DocumentType = documentDb.DocumentType;
            document.Description = documentDb.Description;
            document.FilePath = documentDb.FilePath;
            document.CreatedAt = documentDb.CreatedAt;  
            document.Tags=documentDb.Tags?.Select(i => new TagDto { Id = i.Id, Name = i.Name })?.ToList();
            return document;
        }

        public List<DocumentDto> GetDocuments()
        {
            var documents = DocumentRepository.GetList("Tags","Categories").Where(i=>!i.IsDeleted).Select(documentDb => new DocumentDto
            {
                Id=documentDb.Id,
                Title=documentDb.Title,
                UpdatedAt = documentDb.UpdatedAt,
                DocumentType = documentDb.DocumentType,
                Description = documentDb.Description,
                FilePath = documentDb.FilePath,
                CreatedAt = documentDb.CreatedAt,
                Tags = documentDb.Tags?.Select(i => new TagDto { Id = i.Id, Name = i.Name })?.ToList(),
                Categories = documentDb.Categories?.Select(i => new CategoryDto { Id = i.Id, Name = i.Name })?.ToList(),
            }).ToList();

            return documents;
        }


        public void UpdateDocument(DocumentDto documentDto)
        {
            if (documentDto == null || documentDto.Id==null)
                throw new Exception("No document passed to update");

            var documentDb = DocumentRepository.GetObjectById(documentDto.Id.Value);
            if (documentDb == null)
                throw new Exception("Document not found with document id: " + documentDto.Id);

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
