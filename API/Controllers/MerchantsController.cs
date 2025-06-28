using API.RequestHelpers;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class MerchantsController(IUnitOfWork unit) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Merchant>>> GetMerchants(
        [FromQuery] MerchantSpecParams specParams)
    {
        // Parse string tenure to days
        if (!string.IsNullOrEmpty(specParams.MinTenure))
            specParams.MinTenureInDays = TenureHelper.ParseTenureToDays(specParams.MinTenure);

        if (!string.IsNullOrEmpty(specParams.MaxTenure))
            specParams.MaxTenureInDays = TenureHelper.ParseTenureToDays(specParams.MaxTenure);

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

}
