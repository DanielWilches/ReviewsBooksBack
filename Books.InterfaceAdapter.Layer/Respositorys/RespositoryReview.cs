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
    public class RespositoryReview(AppDbConext dbContext) : IRepository<ReviewEntity>
    {
        private readonly AppDbConext _context = dbContext;
        public async Task<IEnumerable<ReviewEntity>> GetAllAsync()
        {
            return await Task.FromResult(_context.Reviews.AsEnumerable());
        }

        public async Task<ReviewEntity> GetByIdAsync(int id)
        {
            return await _context.FindAsync<ReviewEntity>(id) ?? new ReviewEntity();
        }

        public async Task<IEnumerable<ReviewEntity>> GetListAsync(Expression<Func<ReviewEntity, bool>> predicate)
        {
            return await _context.Reviews.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(ReviewEntity entity)
        {
            await _context.Reviews.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Reviews.Where(b => b.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return (await _context.Reviews.FindAsync(id) ?? new ReviewEntity())?.Id != null;
        }

        public async Task UpdateAsync(ReviewEntity entity)
        {
            await _context.Reviews.Where(b => b.UserId == entity.UserId && b.Id == entity.Id && b.BookId == entity.BookId)
                .ExecuteUpdateAsync(b => b
                    .SetProperty(b => b.ModifiedDate, DateTime.UtcNow)
                    .SetProperty(b => b.Rating, entity.Rating)
                    .SetProperty(b => b.ReviewText, entity.ReviewText)
                );
        }
    }
}
