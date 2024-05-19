﻿using DocHub.Core.Entities;
using DocHub.Data.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Data.Repositories
{
    public class CategoriesRepository : EFCoreRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(DocHubDbContext docHubDbContext) : base(docHubDbContext)
        {
        }

        public override void SoftDelete(Guid id)
        {
            throw new NotImplementedException();
        }

        public override void SoftDeleteByIntId(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Category obj)
        {
            Dbset.Update(obj);
            Commit();
        }
    }
}