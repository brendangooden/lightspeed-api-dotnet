using Newtonsoft.Json;

namespace LightSpeed.Api.Reporting;

public class RowObject
{
    [JsonProperty("gross_profit")]
    public double GrossProfit { get; set; }
    [JsonProperty("margin")]
    public double Margin { get; set; }
    [JsonProperty("total_cost")]
    public double TotalCost { get; set; }
    [JsonProperty("total_revenue")]
    public double TotalRevenue { get; set; }
    [JsonProperty("total_tax")]
    public double TotalTax { get; set; }
}