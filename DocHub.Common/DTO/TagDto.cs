﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Common.DTO
{
    public class TagDto:IDataTransfer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
