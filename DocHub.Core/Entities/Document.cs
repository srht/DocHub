using DocHub.Core.Abstracts;
using DocHub.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.Entities
{
    public class Document:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public DocumentTypes DocumentType { get; set; }
    }
}
