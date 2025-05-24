using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Ttp.Arquitectura.Users.Domain;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;

namespace Ttp.Arquitectura.Users.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected DbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(UsersContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }

            // Obtener el ID del usuario (suponiendo que TEntity es User)
            var userId = ((User)(object)entityToDelete).Id; // Conversión explícita si TEntity es genérico

            // Remover las direcciones asociadas al usuario
            var userAddresses = context.Set<UserAddress>().Where(a => a.IdUser == userId);
            context.Set<UserAddress>().RemoveRange(userAddresses);

            // Remove User
            dbSet.Remove(entityToDelete);

            context.SaveChanges(); // Guarda los cambios
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
