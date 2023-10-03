using System.Net;

namespace LightSpeed.Api.Client.V2;

public class ConnectionResponse
{
    public bool Successful { get; set; }
    public HttpStatusCode ResponseStatusCode { get; set; }
    public string Message { get; set; }
}