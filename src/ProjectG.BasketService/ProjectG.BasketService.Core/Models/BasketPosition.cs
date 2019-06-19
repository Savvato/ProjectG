namespace ProjectG.BasketService.Core.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BasketPosition
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long CustomerId { get; set; }

        [Required]
        public long ProductId { get; set; }

        [Required, Column(TypeName = "varchar(255)")]
        public string ProductName { get; set; }

        [Column(TypeName = "text")]
        public string ProductDescription { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public float Price { get; set; }
    }
}
