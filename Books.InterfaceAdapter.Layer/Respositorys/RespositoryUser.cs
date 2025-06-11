using Books.ApplicationBusiness.Layer.Interfaces;
using Books.EnterpriseBusiness.Layer.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Books.InterfaceAdapter.Layer.Respositorys
{
    public class RespositoryUser(AppDbConext dbContext) : IRepository<UserEntity>
    {
        private readonly AppDbConext _context = dbContext;

        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await Task.FromResult(_context.Users.AsEnumerable());
        }

        public async Task<UserEntity> GetByIdAsync(int id)
        {
            return await _context.FindAsync<UserEntity>(id) ?? new UserEntity();
        }

        public async Task<IEnumerable<UserEntity>> GetListAsync(Expression<Func<UserEntity, bool>> predicate)
        {
            await _context.Users.FindAsync(predicate);
            return _context.Users.Where(predicate).AsEnumerable();
        }

        public async Task AddAsync(UserEntity entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }


        public Task UpdateAsync(UserEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Users.Where(b => b.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return (await _context.Users.FindAsync(id) ?? new UserEntity())?.Id != null;
        } 
    }
}
