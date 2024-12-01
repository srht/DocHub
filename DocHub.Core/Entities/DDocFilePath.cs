using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.Entities
{
    public class DDocFilePath
    {
        [Key]
        public int Id { get; set; }
        public DDocument Doc { get; set; }
        public string FilePath { get; set; }
    }
}
