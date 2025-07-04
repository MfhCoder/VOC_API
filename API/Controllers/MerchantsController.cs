using API.RequestHelpers;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;

namespace API.Controllers;

[Authorize]
public class MerchantsController(IUnitOfWork unit) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Merchant>>> GetMerchants(
        [FromQuery] MerchantSpecParams specParams)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        ParseTenure(specParams);

        var spec = new MerchantSpecification(specParams);
        return await CreatePagedResult(unit.Repository<Merchant>(), spec, specParams.PageIndex, specParams.PageSize);
    }

    [HttpGet("industries")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetIndustries()
    {
        var spec = new IndustryListSpecification();
        return Ok(await unit.Repository<Merchant>().ListAsync(spec));
    }

    [HttpGet("locations")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetLocations()
    {
        var spec = new LocationListSpecification();
        return Ok(await unit.Repository<Merchant>().ListAsync(spec));
    }

    [HttpGet("export")]
    public async Task<IActionResult> ExportMerchants([FromQuery] MerchantSpecParams specParams)
    {
        ParseTenure(specParams);

        var spec = new MerchantSpecification(specParams);
        var merchants = await unit.Repository<Merchant>().ListAsync(spec);
        var csv = GenerateCsv(merchants);

        var bytes = System.Text.Encoding.UTF8.GetBytes(csv.ToString());
        return File(bytes, "text/csv", "merchants.csv");
    }

    [HttpGet("export-pdf")]
    public async Task<IActionResult> ExportMerchantsPdf([FromQuery] MerchantSpecParams specParams)
    {
        ParseTenure(specParams);

        var spec = new MerchantSpecification(specParams);
        var merchants = await unit.Repository<Merchant>().ListAsync(spec);
        var document = new MerchantsPdfDocument(merchants);
        var pdfBytes = document.GeneratePdf();

        return File(pdfBytes, "application/pdf", "merchants.pdf");
    }

    private void ParseTenure(MerchantSpecParams specParams)
    {
        if (!string.IsNullOrEmpty(specParams.MinTenure))
            specParams.MinTenureInDays = TenureHelper.ParseTenureToDays(specParams.MinTenure);

        if (!string.IsNullOrEmpty(specParams.MaxTenure))
            specParams.MaxTenureInDays = TenureHelper.ParseTenureToDays(specParams.MaxTenure);
    }

    private System.Text.StringBuilder GenerateCsv(IReadOnlyList<Merchant> merchants)
    {
        var csv = new System.Text.StringBuilder();
        csv.AppendLine("Id,Name,Industry,Location,Tenure,TenureInDays,PhoneNo,LastSurvey,LastFeedback,Ledger,LastTransaction,LastTicket,CreatedOn,LastEscalation");

        foreach (var m in merchants)
        {
            csv.AppendLine($"{m.Id},{CsvHelper.EscapeCsv(m.Name)},{CsvHelper.EscapeCsv(m.Industry)},{CsvHelper.EscapeCsv(m.Location)},{CsvHelper.EscapeCsv(m.Tenure)},{m.TenureInDays},{CsvHelper.EscapeCsv(m.PhoneNo)},{CsvHelper.EscapeCsv(m.LastSurvey?.ToString())},{CsvHelper.EscapeCsv(m.LastFeedback?.ToString())},{CsvHelper.EscapeCsv(m.Ledger?.ToString())},{CsvHelper.EscapeCsv(m.LastTransaction?.ToString())},{CsvHelper.EscapeCsv(m.LastTicket?.ToString())},{CsvHelper.EscapeCsv(m.CreatedOn.ToString())},{CsvHelper.EscapeCsv(m.LastEscalation?.ToString())}");
        }

        return csv;
    }
}
