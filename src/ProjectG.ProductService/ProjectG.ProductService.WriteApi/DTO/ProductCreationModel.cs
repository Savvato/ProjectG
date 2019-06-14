namespace ProjectG.ProductService.WriteApi.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class ProductCreationModel
    {
        [Required, StringLength(maximumLength: 255)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int Count { get; set; } = 0;

        [Required]
        public float Price { get; set; }
    }
}
