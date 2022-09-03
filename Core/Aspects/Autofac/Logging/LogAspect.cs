using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Settings;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Core.Aspects.Autofac.Logging;

/// <summary>
/// LogAspect
/// </summary>
public class LogAspect : MethodInterception
{
    private readonly LoggerServiceBase _loggerServiceBase;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Stopwatch _stopwatch;
    public LogAspect() : this(DevArchitectureSettings.Loggers.LogAspectLogger)
    {
        Priority = DevArchitectureSettings.Priorities.LogAspectPriority;
    }

    public LogAspect(Type loggerService)
    {
        if (loggerService.BaseType != typeof(LoggerServiceBase))
        {
            throw new ArgumentException(AspectMessages.WrongLoggerType);
        }

        _loggerServiceBase = (LoggerServiceBase)ServiceTool.ServiceProvider.GetService(loggerService);
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
    }

    protected override void OnBefore(IInvocation invocation)
    {
        _stopwatch.Start();
        _loggerServiceBase?.Info(GetLogDetail(invocation));
    }
    protected override void OnAfter(IInvocation invocation)
    {       
        _loggerServiceBase?.Info(GetLogDetail(invocation));
        _stopwatch.Reset();
    }
    private string GetLogDetail(IInvocation invocation)
    {
        var tenantId = _httpContextAccessor.HttpContext?.User.Claims
             .FirstOrDefault(x => x.Type.EndsWith("tenantId"))?.Value;
        var logParameters = new List<LogParameter>();
        for (var i = 0; i < invocation.Arguments.Length; i++)
        {
            logParameters.Add(new LogParameter
            {
                Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                Value = invocation.Arguments[i],
                Type = invocation.Arguments[i].GetType().Name,
            });
        }

        var logDetail = new LogDetail
        {
            MethodName = invocation.Method.Name,
            Parameters = logParameters,
            User = (_httpContextAccessor.HttpContext == null ||
                    _httpContextAccessor.HttpContext.User.Identity.Name == null)
                ? "?"
                : _httpContextAccessor.HttpContext.User.Identity.Name,

            TenantId = (_httpContextAccessor.HttpContext == null ||
                    tenantId == null)
                ? "?"
                : tenantId,
            IpAddress = (_httpContextAccessor.HttpContext == null || _httpContextAccessor.HttpContext.Connection.RemoteIpAddress == null)
                ? "UnknownIp"
                : _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
            UserAgent = (_httpContextAccessor.HttpContext == null || _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString() == null)
            ? "Unknown"
                : _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString(),
            ProcessTime = _stopwatch.Elapsed.TotalSeconds.ToString()
        };
        return JsonConvert.SerializeObject(logDetail);
    }
}
