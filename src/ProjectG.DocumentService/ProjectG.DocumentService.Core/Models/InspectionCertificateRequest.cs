namespace ProjectG.DocumentService.Core.Models
{
    public class InspectionCertificateRequest
    {
        public int Id { get; set; }

        public string PassportNumber { get; set; }

        public bool? HasOutstandingDebt { get; set; }

        public bool? HasAccessToSecrets { get; set; }

        public bool? IsRestricted { get; set; }
    }
}
