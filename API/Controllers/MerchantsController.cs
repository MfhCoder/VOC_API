using API.RequestHelpers;
using Core.Entities;
using Core.Helpers;
using Application.Interfaces;
using Application.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using AutoMapper;
using Infrastructure.Data;
using Application.Dtos.Merchant;

namespace API.Controllers;

//[Authorize]
public class MerchantsController : BaseApiController
{
    private readonly IUnitOfWork unit;
    private readonly IMerchantService merchantService;

    public MerchantsController(
        IUnitOfWork unitOfWork,
      IMapper mapper,IMerchantService merchantService) : base(mapper)
    {
        unit = unitOfWork;
        this.merchantService = merchantService;
    }

    [HttpGet]
    public async Task<ActionResult> GetMerchants([FromQuery] MerchantSpecParams filterParams)
    {
        var spec = new MerchantSpecification(filterParams);
        return await CreatePagedResult<Merchant, MerchantDto>(
            unit.Repository<Merchant>(),
            spec,
            filterParams.PageIndex,
            filterParams.PageSize);
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
        return File(await merchantService.ExportMerchantsCSV(specParams), "text/csv", "merchants.csv");
    }

    [HttpGet("export-pdf")]
    public async Task<IActionResult> ExportMerchantsPdf([FromQuery] MerchantSpecParams specParams)
    {
        return File(await merchantService.ExportMerchantsPDF(specParams), "application/pdf", "merchants.pdf");
    }



}
