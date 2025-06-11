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
    public class RespositoryUser(AppDbConext dbContext) : IRepository<CustomUserProfile>
    {
        private readonly AppDbConext _context = dbContext;

        public async Task<IEnumerable<CustomUserProfile>> GetAllAsync()
        {
            return await Task.FromResult(_context.CustomUserProfiles.AsEnumerable());
        }

        public async Task<CustomUserProfile> GetByIdAsync(int id)
        {
            return await _context.FindAsync<CustomUserProfile>(id) ?? new CustomUserProfile();
        }

        public async Task<IEnumerable<CustomUserProfile>> GetListAsync(Expression<Func<CustomUserProfile, bool>> predicate)
        {
            await _context.CustomUserProfiles.FindAsync(predicate);
            return _context.CustomUserProfiles.Where(predicate).AsEnumerable();
        }

        public async Task AddAsync(CustomUserProfile entity)
        {
            await _context.CustomUserProfiles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }


        public Task UpdateAsync(CustomUserProfile entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            await _context.CustomUserProfiles.Where(b => b.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return (await _context.CustomUserProfiles.FindAsync(id) ?? new CustomUserProfile()).Id != null;
        } 
    }
}
