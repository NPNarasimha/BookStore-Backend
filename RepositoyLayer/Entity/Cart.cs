using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoyLayer.Entity
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual Users User { get; set; }
        [ForeignKey("Books")]
        public int BookId { get; set; }
        public virtual Books Book { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public bool IsPurchased { get; set; } = false;
    }
}
