using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoyLayer.Entity
{
    public class Purchese
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurcheseId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual Users User { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Books Book { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public DateTime PurcheseDate { get; set; }

    }
}
