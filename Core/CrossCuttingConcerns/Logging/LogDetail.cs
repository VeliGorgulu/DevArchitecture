namespace Core.CrossCuttingConcerns.Logging;

public class LogDetail
{
    public string FullName { get; set; }
    public string MethodName { get; set; }
    public string User { get; set; }
    public string TenantId { get; set; }
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }
    public string ProcessTime { get; set; }
    public List<LogParameter> Parameters { get; set; }
}
