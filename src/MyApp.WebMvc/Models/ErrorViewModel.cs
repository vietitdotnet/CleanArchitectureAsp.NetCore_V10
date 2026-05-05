namespace MyApp.WebMvc.Models;

public class ErrorViewModel
{
    public string? ErrorCode { get; set; }
    public int StatusCode { get;  set; }
    public string? RequestId { get; set; }
    public bool ShowRequestId { get; set; }
    public string? Message { get; set; }
    public bool ShowMessage => !string.IsNullOrEmpty(Message);


 
}
