namespace ProjectG.DocumentService.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/inspection-certificate")]
    [ApiController]
    public class InspectionCertificateController : ControllerBase
    {
        [HttpPost(template: "request")]
        public IActionResult Post()
        {
            return this.Ok("Okay");
        }
    }
}