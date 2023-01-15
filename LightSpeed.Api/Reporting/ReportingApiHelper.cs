using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using LightSpeed.Api.Configuration;
using LightSpeed.Api.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NullValueHandling = Newtonsoft.Json.NullValueHandling;

namespace LightSpeed.Api.Reporting;

public static class ReportingApiHelper
{
    /// <summary>
    /// Reporting is not included in the public API for LightSpeed.
    /// This is reverse engineered from their website API Calls.
    /// </summary>
    /// <param name="apiConfiguration"></param>
    /// <param name="dateFrom"></param>
    /// <param name="dateTo"></param>
    /// <param name="groupBy"></param>
    /// <returns></returns>
    public static async Task<IReadOnlyList<ReportSummaryData>> GetListAsync(ILightSpeedApiConfiguration apiConfiguration, DateTime dateFrom, DateTime dateTo, GroupBy groupBy)
    {
        var reportConfig = CreateRequest(dateFrom, dateTo, groupBy);

        var reportConfigJson = reportConfig.ToJson();

        var request = await apiConfiguration.CreateHttpRequestMessageAsync(CancellationToken.None).ConfigureAwait(false);
        request.Method = HttpMethod.Get;

        var baseUrl = Flurl.Url.Parse(apiConfiguration.BaseUrl).RemovePath();

        request.RequestUri = new Uri(Url.Combine(baseUrl.ToString(), $"api/2.0/report?params={reportConfigJson}"));

        try
        {
            var client = new HttpClient();
            var responseMessage = await client.SendAsync(request).ConfigureAwait(false);

            var payload = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ReportResponse>(payload);

                var rows = result.Rows.SelectMany(x => x).SelectMany(x => x).ToList();

                var columns = result.Headers.Columns.SelectMany(a => a).ToList();

                if (rows.Count != columns.Count)
                    throw new Exception($"Number of rows/columns must match!\r\n\r\n{request.RequestUri}");

                return columns.Select((a, i) =>
                {
                    var rowObj = rows[i];
                    return new ReportSummaryData
                    {
                        Date = DateTimeOffset.FromUnixTimeMilliseconds((long)Convert.ToDouble(a)).Date,
                        GrossProfit = rowObj.GrossProfit,
                        Margin = rowObj.Margin,
                        TotalCost = rowObj.TotalCost,
                        TotalRevenue = rowObj.TotalRevenue,
                        TotalTax = rowObj.TotalTax
                    };
                }).ToList();


            }

            client.Dispose();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return null;
    }

    public static ReportRequest CreateRequest(DateTime dateFrom, DateTime dateTo, GroupBy groupBy = GroupBy.day)
    {
        var reportConfig = new ReportRequest()
        {
            Dimensions = new Dimensions()
            {
                Column = new List<Column>()
                {
                    new Column()
                    {
                        Key = groupBy
                    }
                }
            },
            Aggregates = new Aggregates()
            {
                Cell = new List<string>()
                {
                    "total_revenue",
                    "total_cost",
                    "gross_profit",
                    "margin",
                    "total_tax"
                }
            },
            Constraints = new List<Constraint>()
            {
                new Constraint()
                {
                    Id = "true",
                    Type = "exclude_gift_cards" // exclude sale of gift cards
                }
            },
            Order = new List<Order>()
            {
                new Order()
                {
                    Alphabetical = false,
                    Dimension = "summary",
                    Direction = "desc",
                    Metric = "total_revenue"
                }
            },
            From = 0,
            Size = 1000,
            Periods = new List<Period>()
            {
                new Period()
                {
                    DenormaliseTime = false,
                    Start = new DateTime(dateFrom.Ticks, DateTimeKind.Utc),
                    End = new DateTime(dateTo.Ticks, DateTimeKind.Utc)
                }
            }
        };
        return reportConfig;
    }
}


public class ReportSummaryData : RowObject
{
    public DateTime Date { get; set; }
}

public class Column
{

    [JsonProperty("key")]
    public GroupBy Key { get; set; }

    [JsonProperty("metadata")]
    public IList<object> Metadata { get; set; } = new List<object>();
}

public class Dimensions
{

    [JsonProperty("row")]
    public IList<object> Row { get; set; } = new List<object>();

    [JsonProperty("column")]
    public IList<Column> Column { get; set; }
}

public class Aggregates
{

    [JsonProperty("cell", NullValueHandling = NullValueHandling.Ignore)]
    public IList<string> Cell { get; set; }

    [JsonProperty("row", NullValueHandling = NullValueHandling.Ignore)]
    public IList<string> Row { get; set; }
    [JsonProperty("column", NullValueHandling = NullValueHandling.Ignore)]
    public IList<string> Column { get; set; }
    [JsonProperty("table", NullValueHandling = NullValueHandling.Ignore)]
    public IList<string> Table { get; set; }
}

public class Constraint
{

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}

public class Order
{

    [JsonProperty("dimension")]
    public string Dimension { get; set; }

    [JsonProperty("direction")]
    public string Direction { get; set; }

    [JsonProperty("metric")]
    public string Metric { get; set; }

    [JsonProperty("alphabetical")]
    public bool Alphabetical { get; set; }
}

public class ReportRequest
{

    [JsonProperty("dimensions")]
    public Dimensions Dimensions { get; set; }

    [JsonProperty("aggregates")]
    public Aggregates Aggregates { get; set; }

    [JsonProperty("constraints")]
    public IList<Constraint> Constraints { get; set; }

    [JsonProperty("order")]
    public IList<Order> Order { get; set; }

    [JsonProperty("from")]
    public int From { get; set; }

    [JsonProperty("size")]
    public int Size { get; set; }

    [JsonProperty("periods")]
    public IList<Period> Periods { get; set; }
}


[JsonConverter(typeof(StringEnumConverter))]
public enum GroupBy
{
    day,
    month,
    year
}

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

public class Headers
{

    [JsonProperty("columns")]
    public IList<IList<string>> Columns { get; set; }

    [JsonProperty("column_titles")]
    public IList<string> ColumnTitles { get; set; }

    [JsonProperty("rows")]
    public IList<IList<object>> Rows { get; set; }

    [JsonProperty("row_titles")]
    public IList<object> RowTitles { get; set; }

    [JsonProperty("row_ids")]
    public IList<IList<object>> RowIds { get; set; }

    [JsonProperty("row_metadata")]
    public IList<IList<object>> RowMetadata { get; set; }

    [JsonProperty("row_metadata_keys")]
    public IList<object> RowMetadataKeys { get; set; }
}

public class Table
{
}

public class Period
{

    [JsonProperty("start")]
    public DateTime Start { get; set; }

    [JsonProperty("end")]
    public DateTime End { get; set; }

    [JsonProperty("denormalise_time")]
    public bool DenormaliseTime { get; set; }
}

public class Currency
{

    [JsonProperty("symbol")]
    public string Symbol { get; set; }
}

public class ReportResponse
{

    [JsonProperty("rows")]
    public IList<IList<IList<RowObject>>> Rows { get; set; }

    [JsonProperty("headers")]
    public Headers Headers { get; set; }

    [JsonProperty("periods")]
    public IList<Period> Periods { get; set; }

    [JsonProperty("currency")]
    public Currency Currency { get; set; }

    [JsonProperty("more_results")]
    public bool MoreResults { get; set; }
}
