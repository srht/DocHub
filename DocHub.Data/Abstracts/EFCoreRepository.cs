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
    public abstract class EFCoreRepository<T> : IDisposable, IGenericRepository<T> where T : class
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
            if(DocHubDbContext.Database.CurrentTransaction == null)
            DocHubTransaction = DocHubDbContext.Database.BeginTransaction();
        }

        public IQueryable<T> GetAll()
        {
            return Dbset;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return Dbset.Where(predicate);
        }

        public void Delete(Guid id)
        {
            var found = Dbset.Find(id);
            Dbset.Remove(found);
            Commit();
        }

        public void DeleteByIntId(int id)
        {
            var found = Dbset.Find(id);
            Dbset.Remove(found);
            Commit();
        }

        public IEnumerable<T> GetList()
        {
            return Dbset.ToList();
        }
        public T GetObjectById(Guid id)
        {
            var found = Dbset.Find(id);
            return found;
        }

        public T GetObjectByIntId(int id)
        {
            var found = Dbset.Find(id);
            return found;
        }

        public void Insert(T obj)
        {
            Dbset.Add(obj);
            Commit();
        }

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

         int SaveChanges()
        {
            var result = DocHubDbContext.SaveChanges();
            return result;
        }

        public bool Commit(bool state = true)
        {
            try
            {
                SaveChanges();
                if (state)
                    DocHubTransaction.Commit();
                else
                    DocHubTransaction.Rollback();
                return true;
            }
            catch (Exception ex)
            {
                DocHubTransaction.Rollback();
                //Logger.LogError(exception: ex,null);
                return false;
            }
            finally
            {
                Dispose(true);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract void SoftDelete(Guid id);

        public abstract void SoftDeleteByIntId(int id);

        public abstract void Update(T obj);

    }
}
