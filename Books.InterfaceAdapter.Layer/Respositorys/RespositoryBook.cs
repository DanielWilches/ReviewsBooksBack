using Books.ApplicationBusiness.Layer.Interfaces;
using Books.EnterpriseBusiness.Layer.Entitys;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;


namespace Books.InterfaceAdapter.Layer.Respositorys
{
    public class RespositoryBook(AppDbConext dbContext) : IRepository<BookEntity>
    {
        private readonly AppDbConext _context = dbContext;

        public async Task<IEnumerable<BookEntity>> GetAllAsync()
        {
            return await _context.books.ToListAsync();
        }

        public async Task<BookEntity> GetByIdAsync(int id)
        {
            return await _context.FindAsync<BookEntity>(id) ?? new BookEntity();
        }

        public async Task<IEnumerable<BookEntity>> GetListAsync(Expression<Func<BookEntity, bool>> predicate)
        {

            return await dbContext.Set<BookEntity>().Where(predicate).ToListAsync();
        }

        public async Task AddAsync(BookEntity entity)
        {
            await _context.books.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BookEntity entity)
        {
          await _context.books.Where(b => b.Id == entity.Id)
                .ExecuteUpdateAsync(b => b.SetProperty(b => b.Title, entity.Title)
                .SetProperty(b => b.Author, entity.Author)
                .SetProperty(b => b.Category, entity.Category)
                .SetProperty(b => b.Summary, entity.Summary)
                .SetProperty(b => b.ModifiedDate, DateTime.UtcNow));

        }

        public async Task DeleteAsync(int id)
        {
            await _context.books.Where(b => b.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return (await _context.books.FindAsync(id) ?? new BookEntity())?.Id != null;
        }



       
    }
}
