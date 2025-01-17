﻿using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers;

/// <summary>
/// Base controller
/// </summary>
[Authorize]
[Route("[controller]")]
[ApiController]
public class BaseApiController : Controller
{
    private IMediator _mediator;

    /// <summary>
    /// It is for getting the Mediator instance creation process from the base controller.
    /// </summary>
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult GetResponse<T>(IDataResult<T> result)
        => result.Success ? Ok(result) : BadRequest(result);

    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult GetResponseOnlyResult(IResult result)
        => result.Success ? Ok(result) : BadRequest(result);

    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult GetResponseOnlyResultMessage(IResult result)
        => result.Success ? Ok(result.Message) : BadRequest(result.Message);

    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult GetResponseOnlyResultData<T>(IDataResult<T> result)
        => result.Success ? Ok(result.Data) : BadRequest(result.Message);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="internalMessage"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Success<T>(string message, string internalMessage, T data)
        => Success(new ApiResult<T>
        {
            Success = true,
            Message = message,
            InternalMessage = internalMessage,
            Data = data
        });

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Success<T>(ApiResult<T> data)
        => Ok(data);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="internalMessage"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Created<T>(string message, string internalMessage, T data)
        => Created(new ApiResult<T>
        {
            Success = true,
            Message = message,
            InternalMessage = internalMessage,
            Data = data
        });

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Created<T>(ApiResult<T> data)
        => StatusCode(201, data);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="internalMessage"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult NoContent<T>(string message, string internalMessage, T data)
        => NoContent(new ApiResult<T>
        {
            Success = true,
            Message = message,
            InternalMessage = internalMessage,
            Data = data
        });

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult NoContent<T>(ApiResult<T> data)
        => StatusCode(204, data);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="internalMessage"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult BadRequest<T>(string message, string internalMessage, T data)
        => BadRequest(new ApiResult<T>
        {
            Success = false,
            Message = message,
            InternalMessage = internalMessage,
            Data = data
        });

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult BadRequest<T>(ApiResult<T> data)
        => StatusCode(400, data);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="internalMessage"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Unauthorized<T>(string message, string internalMessage, T data)
        => Unauthorized(new ApiResult<T>
        {
            Success = false,
            Message = message,
            InternalMessage = internalMessage,
            Data = data
        });

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Unauthorized<T>(ApiResult<T> data)
        => StatusCode(401, data);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="internalMessage"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Forbidden<T>(string message, string internalMessage, T data)
        => Forbidden(new ApiResult<T>
        {
            Success = false,
            Message = message,
            InternalMessage = internalMessage,
            Data = data
        });

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Forbidden<T>(ApiResult<T> data)
        => StatusCode(403, data);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="internalMessage"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult NotFound<T>(string message, string internalMessage, T data)
        => NotFound(new ApiResult<T>
        {
            Success = false,
            Message = message,
            InternalMessage = internalMessage,
            Data = data
        });

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult NotFound<T>(ApiResult<T> data)
        => StatusCode(404, data);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="internalMessage"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Error<T>(string message, string internalMessage, T data)
        => Error(new ApiResult<T>
        {
            Success = false,
            Message = message,
            InternalMessage = internalMessage,
            Data = data
        });

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    [NonAction]
    protected IActionResult Error<T>(ApiResult<T> data)
        => StatusCode(500, data);
}
