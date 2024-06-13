﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DocHub.Common.DTO;
using DocHub.Core.Entities;
using DocHub.Data.Repositories;

namespace DocHub.Service.Mappers
{
    public class TagMapper:Profile
    {
        public TagMapper() {
            CreateMap<Tag, TagDto>()
                .ReverseMap();
        }
    }
}
