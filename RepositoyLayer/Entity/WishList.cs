using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoyLayer.Entity
{
    public class WishList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WishListId { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        public Users User { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Books Book { get; set; }
    }
}
