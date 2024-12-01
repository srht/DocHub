using DocHub.Common.Enums;
using DocHub.Core.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.Entities
{
    public class DDocument:BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<DDocFilePath>? FilePaths { get; set; }
        public List<Tag>? Tags { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public DocumentTypes DocumentType { get; set; } = DocumentTypes.Text;
    }
}
