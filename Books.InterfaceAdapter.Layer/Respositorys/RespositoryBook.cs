using Books.ApplicationBusiness.Layer.Interfaces;
using Books.EnterpriseBusiness.Layer.Entitys;
using Microsoft.EntityFrameworkCore;


namespace Books.InterfaceAdapter.Layer.Respositorys
{
    public class RespositoryBook(AppDbConext dbContext) : IRepository<BookEntity>
    {
        private readonly AppDbConext _context = dbContext;

        public Task<IEnumerable<BookEntity>> GetAllAsync()
        {
            return Task.FromResult(_context.Books.AsEnumerable());
        }

        public async Task<BookEntity> GetByIdAsync(int id)
        {
            return await _context.FindAsync<BookEntity>(id) ?? new BookEntity();
        }

        public async Task<IEnumerable<BookEntity>> GetListAsync(Func<BookEntity, bool> predicate)
        {
            await _context.Books.FindAsync(predicate);
            return _context.Books.Where(predicate).AsEnumerable();
        }

        public async Task AddAsync(BookEntity entity)
        {
            await _context.Books.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BookEntity entity)
        {
          await _context.Books.Where(b => b.Id == entity.Id)
                .ExecuteUpdateAsync(b => b.SetProperty(b => b.Title, entity.Title)
                .SetProperty(b => b.Author, entity.Author)
                .SetProperty(b => b.Category, entity.Category)
                .SetProperty(b => b.Summary, entity.Summary)
                .SetProperty(b => b.ModifiedDate, DateTime.UtcNow));

        }

        public async Task DeleteAsync(int id)
        {
            await _context.Books.Where(b => b.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return (await _context.Books.FindAsync(id) ?? new BookEntity())?.Id != null;
        }



       
    }
}
