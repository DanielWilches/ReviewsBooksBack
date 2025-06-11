using Books.ApplicationBusiness.Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.ApplicationBusiness.Layer
{
    public class BookController<T>(IRepository<T> repository)
    {
        private readonly IRepository<T> _repository = repository;

        public void GetBooks() { }
        public void GetBookById() { }
        public void GetBookByauthor() { }
        public void GetBookByTitle() { }
        public void GetBooksByCategory() { }
    }
}
