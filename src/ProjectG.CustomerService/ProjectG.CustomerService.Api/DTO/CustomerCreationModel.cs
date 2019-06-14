namespace ProjectG.CustomerService.Api.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class CustomerCreationModel
    {
        [Required, StringLength(maximumLength: 255)]
        public string FirstName { get; set; }

        [Required, StringLength(maximumLength: 255)]
        public string Surname { get; set; }

        [Required, StringLength(maximumLength: 255)]
        public string Address { get; set; }

        [Required, StringLength(maximumLength: 255)]
        public string Email { get; set; }
    }
}