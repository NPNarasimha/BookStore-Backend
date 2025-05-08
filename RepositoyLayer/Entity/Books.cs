using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoyLayer.Entity
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string Author { get; set; }
       
        public string Description { get; set; }

        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public int Quantity { get; set; }
        public string BookImage { get; set; }
        public string AdminUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
