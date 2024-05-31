using DocHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DocHub.Data.Abstracts
{
    public abstract class EFCoreRepository<T> : IGenericRepository<T> where T : class
    {
        private bool DbContextDisposed;
        public DbSet<T> Dbset { get; }

        private IDbContextTransaction DocHubTransaction;
        public DocHubDbContext DocHubDbContext { get; }
        public ILogger Logger { get; }

        public EFCoreRepository(DocHubDbContext docHubDbContext)
        {
            DocHubDbContext = docHubDbContext;
            Dbset =DocHubDbContext.Set<T>();
            //if(DocHubDbContext.Database.CurrentTransaction == null)
            //DocHubTransaction = DocHubDbContext.Database.BeginTransaction();
        }

        public IQueryable<T> GetAll()
        {
            return Dbset;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return Dbset.Where(predicate);
        }

        public async Task DeleteAsync(Guid id)
        {
            var found = Dbset.Find(id);
            Dbset.Remove(found);
            await CommitAsync();
        }

        public async Task DeleteByIntIdAsync(int id)
        {
            var found = Dbset.Find(id);
            Dbset.Remove(found);
            await CommitAsync();
        }

        public IEnumerable<T> GetList()
        {
            return Dbset.ToList();
        }
        public virtual T GetObjectById(Guid id)
        {
            var found = Dbset.Find(id);
            return found;
        }

        public virtual T GetObjectByIntId(int id)
        {
            var found = Dbset.Find(id);
            return found;
        }

        public virtual void Insert(T obj)
        {
            Dbset.Add(obj);
        }
        /*
        protected virtual void Dispose(bool disposing)
        {
            if (!DbContextDisposed)
            {
                if (disposing)
                {
                    DocHubDbContext.Dispose();
                }
            }

            DbContextDisposed = true;
        }
        */

        async Task<int> SaveChanges()
        {
            var result = await DocHubDbContext.SaveChangesAsync();
            return result;
        }

        public async Task<bool> CommitAsync(bool state = true)
        {
            try
            {
                int result = await SaveChanges();
                return result>0;
                /*
                if (state)
                    DocHubTransaction.Commit();
                else
                    DocHubTransaction.Rollback();
                return true;
                */
            }
            catch (Exception ex)
            {
                //DocHubTransaction.Rollback();
                //Logger.LogError(exception: ex,null);
                return false;
            }
        }

        public abstract Task SoftDeleteAsync(Guid id);

        public abstract Task SoftDeleteByIntIdAsync(int id);

        public abstract Task UpdateAsync(T obj);

    }
}
