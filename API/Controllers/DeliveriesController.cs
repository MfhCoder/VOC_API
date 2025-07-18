//using API.Controllers;
//using Core.DTOs;
//using API.RequestHelpers;
//using Application.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using AutoMapper;
//using Application.Interfaces.Delivery;
//using Core.Entities;

//[Authorize]
//public class DeliveriesController(IUnitOfWork unit, IMapper mapper) : BaseApiController
//{
//    //private readonly IMapper mapper;
//    //private readonly IDeliveryService _deliveryService;
//    //private readonly ILogger<DeliveriesController> _logger;

//    //public DeliveriesController(
//    //    IUnitOfWork unitOfWork,
//    //    IMapper mapper,
//    //    //IDeliveryService deliveryService,
//    //    ILogger<DeliveriesController> logger)
//    //{
//    //    _unitOfWork = unitOfWork;
//    //    mapper = mapper;
//    //    _deliveryService = deliveryService;
//    //    _logger = logger;
//    //}

//    [HttpGet]
//    public async Task<ActionResult<Pagination<DeliveryDto>>> GetDeliveries(
//        [FromQuery] DeliverySpecParams specParams)
//    {
//        var spec = new DeliverySpecifications(specParams);
  
//        var listSpec = new DeliverySpecifications(specParams);
//        var deliveries = await unit.Repository<SurveyBatch>().ListAsync(listSpec);

//        var data = mapper.Map<IReadOnlyList<DeliveryDto>>(deliveries);

//        return await CreatePagedResult(unit.Repository<SurveyBatch>(), spec, specParams.PageIndex, specParams.PageSize);
//    }

//}

//[HttpGet("{batchId}")]
//    public async Task<ActionResult<DeliveryDetailDto>> GetDelivery(string batchId)
//    {
//        var spec = new DeliveryWithDetailsSpec(batchId);
//        var delivery = await _unitOfWork.Repository<Delivery>().GetEntityWithSpec(spec);

//        if (delivery == null) return NotFound();

//        return mapper.Map<DeliveryDetailDto>(delivery);
//    }

//    [HttpGet("summary")]
//    public async Task<ActionResult<DeliverySummaryDto>> GetDeliverySummary(
//        [FromQuery] DeliveryFilterParams filterParams)
//    {
//        var summary = await _unitOfWork.Repository<Delivery>()
//            .GetDeliverySummaryAsync(filterParams);

//        return Ok(new DeliverySummaryDto
//        {
//            TotalDeliveries = summary.TotalDeliveries,
//            TotalSent = summary.TotalSent,
//            TotalDelivered = summary.TotalDelivered,
//            DeliveryRate = summary.TotalSent > 0 ?
//                (summary.TotalDelivered / (decimal)summary.TotalSent) * 100 : 0,
//            TotalResponses = summary.TotalResponses,
//            ResponseRate = summary.TotalDelivered > 0 ?
//                (summary.TotalResponses / (decimal)summary.TotalDelivered) * 100 : 0
//        });
//    }

//    [HttpPost("{batchId}/retry")]
//    public async Task<ActionResult> RetryFailedDeliveries(string batchId)
//    {
//        var delivery = await _unitOfWork.Repository<Delivery>()
//            .GetDeliveryWithDetailsAsync(batchId);

//        if (delivery == null) return NotFound();

//        var result = await _deliveryService.RetryFailedDeliveriesAsync(delivery);

//        if (!result.Succeeded) return BadRequest(result.Message);

//        return NoContent();
//    }

//    [HttpPost("export")]
//    public async Task<ActionResult> ExportDeliveries(
//        [FromQuery] DeliveryFilterParams filterParams,
//        [FromQuery] string format = "csv")
//    {
//        var spec = new DeliveriesWithFiltersSpec(filterParams);
//        var deliveries = await _unitOfWork.Repository<Delivery>().ListAsync(spec);

//        var fileContent = _deliveryService.GenerateExportFile(deliveries, format);

//        return File(fileContent.Content, fileContent.ContentType, fileContent.FileName);
//    }
//}