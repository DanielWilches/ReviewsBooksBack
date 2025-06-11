using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.EnterpriseBusiness.Layer.Models
{
    public class BookModel
    {

 
        public int Id { get; set; }

     
        public string Title { get; set; }


        public string Author { get; set; }

      
        public string Category { get; set; }

       
        public string Summary { get; set; }

       
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        
        public DateTime? ModifiedDate { get; set; }

    }
}
