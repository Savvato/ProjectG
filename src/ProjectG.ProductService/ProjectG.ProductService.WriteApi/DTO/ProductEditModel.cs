namespace ProjectG.ProductService.WriteApi.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class ProductEditModel
    {
        [Required]
        public long Id { get; set; }

        [StringLength(maximumLength: 255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public float Price { get; set; }
    }
}