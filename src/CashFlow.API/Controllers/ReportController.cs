using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.PDF;
using CashFlow.Communication.Requests;
using CashFlow.Exception;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet("Excel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        
        public async Task<IActionResult> GetExcel([FromServices] IGenerateExpensesReportExcelUseCase useCase, [FromHeader] DateOnly month)
        {
            byte[] file = await useCase.Execute(month);

            if(file.Length > 0)
            {
                return File(file, MediaTypeNames.Application.Octet, "Report.xlsx");
            }

            return NoContent();
        }

        [HttpGet("PDF")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]


        public async Task<IActionResult> GetPdf([FromServices] IGenerateExpensesReportPdfUseCase useCase, [FromQuery] DateOnly month)
        {
            byte[] file = await useCase.Execute(month);

            if (file.Length > 0)
            {
                return File(file, MediaTypeNames.Application.Pdf, "Report.pdf");
            }

            return NoContent();
        }
    }
}
