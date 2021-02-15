﻿using NawafizApp.Domain.Entities;
using NawafizApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NawafizApp.Data.Repositories
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private ApplicationDbContext _context;
        private DbSet<TEntity> _set;

        internal Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> Set
        {
            get { return _set ?? (_set = _context.Set<TEntity>()); }
        }

        public List<TEntity> GetAll()
        {

           
            return Set.ToList();
        }



        public Task<List<TEntity>> GetAllAsync()
        {
            return Set.ToListAsync();
        }

        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Set.ToListAsync(cancellationToken);
        }

        public List<TEntity> PageAll(int skip, int take)
        {
            return Set.Skip(skip).Take(take).ToList();
        }

        public Task<List<TEntity>> PageAllAsync(int skip, int take)
        {
            return Set.Skip(skip).Take(take).ToListAsync();
        }

        public Task<List<TEntity>> PageAllAsync(CancellationToken cancellationToken, int skip, int take)
        {
            return Set.Skip(skip).Take(take).ToListAsync(cancellationToken);
        }

        public TEntity FindById(object id)
        {
            return Set.Find(id);
        }

        public Task<TEntity> FindByIdAsync(object id)
        {
            return Set.FindAsync(id);
        }

        public Task<TEntity> FindByIdAsync(CancellationToken cancellationToken, object id)
        {
            return Set.FindAsync(cancellationToken, id);
        }

        public void Add(TEntity entity)
        {
            Set.Add(entity);
        }

        public void Update(TEntity entity)
        {
            var entry = _context.Entry(entity);
            
            if (entry.State == EntityState.Detached)
            {
                Set.Attach(entity);
                entry = _context.Entry(entity);
            }
            entry.State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            Set.Remove(entity);
        }

        

        public Guid getManIdforRoom(int rid)
        {

            var ctx = new ApplicationDbContext();



            Guid ManId = ctx.Database.SqlQuery<Guid>(@"SELECT  UserRole.UserId
                         FROM HotelBlocks INNER JOIN
                         Rooms ON HotelBlocks.Id = Rooms.HotelBlock_Id INNER JOIN
                         [User] ON HotelBlocks.Id = [User].HotelBlock_Id INNER JOIN
                         UserRole ON[User].UserId = UserRole.UserId INNER JOIN
                         Role ON UserRole.RoleId = Role.RoleId where Rooms.Id = '" + rid + "' and Role.Name = 'BlockSupervisor'").Single();
            

            return ManId;

        }

        
        public TEntity FindSingleBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Set.Where(predicate).FirstOrDefault();
        }
        public List<TEntity> FindBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Set.Where(predicate).ToList();
        }
    }
}
