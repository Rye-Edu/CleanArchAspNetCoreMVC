using AutoMapper;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using Northwind_App.Interfaces.IRepositories;
using Northwind_Infrastructure.Data;

namespace Northwind_Infrastructure.Repositories
{
    public class AsyncBaseRepository<TEntity> : IAsyncBaseRepository<TEntity> where TEntity : class
    {
        private readonly NorthwindContext _northwndContext;
        //private readonly IMapper _mapper;
        private DbSet<TEntity> _dbEntities;
        public AsyncBaseRepository(NorthwindContext northwndContext)
        {
            _northwndContext = northwndContext;
           // _mapper = mapper;
            _dbEntities = northwndContext.Set<TEntity>();
        }
        public async Task<TEntity> AddEntityAsync(TEntity entity)
        {
            //var entityType = _mapper.Map<TEntity>(entity);
            var added = await _dbEntities.AddAsync(entity);
            await _northwndContext.SaveChangesAsync();

            return added.Entity;
        }

        public async Task<TEntity> DeleteEntityAsync(int entity)
        {
            //if (entity != null) {
            //    var found = _dbEntities.FindAsync(entity);
            //    var removeEntity = _dbEntities.Remove(Ted);
            //    return Task.FromResult(removeEntity);
            //}
            //return entity;
            var found = await _dbEntities.FindAsync(entity);
            if (found != null)
            {
                
                 _northwndContext.Remove(found);
                await _northwndContext.SaveChangesAsync();

                return Task.FromResult(found).Result;
            }
            else {
                throw new Exception("No data found"); 
            }
           
        }

        public async Task<TEntity> GetByIDAsync(int id)
        {
            var s = _dbEntities;
            var single = await _dbEntities.FindAsync(id);
            if (single == null)
            {
                throw new Exception("Now Data");
            }
            else {
                return Task.FromResult(single).Result;
            }
        }

        public async Task<IEnumerable<TEntity>> GetListEntitiesAsync()
        {
            return await _dbEntities.ToListAsync();
        }

        public async Task<TEntity> UpdateEntityAsync(TEntity entity)
        {
            var update =  _northwndContext.Set<TEntity>().Update(entity);
            await _northwndContext.SaveChangesAsync();

            return  update.Entity;
        }

        public Task SaveDB()
        {
            return Task.FromResult(_northwndContext.SaveChangesAsync());
        }

    }
    }
