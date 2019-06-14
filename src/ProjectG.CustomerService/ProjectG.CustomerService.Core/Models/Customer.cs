namespace ProjectG.CustomerService.Core.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Customer
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, Column(TypeName = "varchar(255)")]
        public string FirstName { get; set; }

        [Required, Column(TypeName = "varchar(255)")]
        public string Surname { get; set; }

        [Required, Column(TypeName = "text")]
        public string Address { get; set; }

        [Required, Column(TypeName = "varchar(255)")]
        public string Email { get; set; }
    }
}