using API.RequestHelpers;
using Core.Entities;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Specifications;
using AutoMapper;

//namespace API.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class BaseApiController : ControllerBase
//{
//    protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repo,
//        ISpecification<T> spec, int pageIndex, int pageSize) where T : BaseEntity
//    {
//        var items = await repo.ListAsync(spec);
//        var count = await repo.CountAsync(spec);

//        var pagination = new Pagination<T>(pageIndex, pageSize, count, items);

//        return Ok(pagination);
//    }

//    protected async Task<ActionResult> CreatePagedResult<T, TDto>(IGenericRepository<T> repo,
//        ISpecification<T> spec, int pageIndex, int pageSize, Func<T, TDto> toDto) where T 
//            : BaseEntity, IDtoConvertible
//    {
//        var items = await repo.ListAsync(spec);
//        var count = await repo.CountAsync(spec);

//        var dtoItems = items.Select(toDto).ToList();

//        var pagination = new Pagination<TDto>(pageIndex, pageSize, count, dtoItems);

//        return Ok(pagination);
//    }
//}

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected readonly IMapper _mapper;

    public BaseApiController(IMapper mapper)
    {
        _mapper = mapper;
    }

    // Generic paginated result with direct entity return
    protected async Task<ActionResult> CreatePagedResult<T>(
        IGenericRepository<T> repository,
        BaseSpecification<T> spec,
        int pageIndex,
        int pageSize) where T : BaseEntity
    {
        //spec.ApplyPaging(pageSize * (pageIndex - 1), pageSize);

        var items = await repository.ListAsync(spec);
        var count = await repository.CountAsync(spec);

        var pagination = new Pagination<T>(pageIndex, pageSize, count, items);
        return Ok(pagination);
    }

    // Generic paginated result with DTO mapping
    protected async Task<ActionResult> CreatePagedResult<T, TDto>(
        IGenericRepository<T> repository,
        BaseSpecification<T> spec,
        int pageIndex,
        int pageSize) where T : BaseEntity
    {
       // spec.ApplyPaging(pageSize * (pageIndex - 1), pageSize);

        var items = await repository.ListAsync(spec);
        var count = await repository.CountAsync(spec);

        var dtoItems = _mapper.Map<IReadOnlyList<TDto>>(items);

        var pagination = new Pagination<TDto>(pageIndex, pageSize, count, dtoItems);

        return Ok(pagination);
    }

    // Simplified success response
    protected ActionResult Success<T>(T data, string message = null)
    {
        return Ok(new ApiResponse<T>(true, message, data));
    }

    // Error response
    protected ActionResult Error(string message, int statusCode = 400)
    {
        return StatusCode(statusCode, new ApiResponse<object>(false, message));
    }
}

// Supporting classes
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public ApiResponse(bool success, string message = null, T data = default)
    {
        Success = success;
        Message = message;
        Data = data;
    }
}
