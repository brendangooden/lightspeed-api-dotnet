﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using LightSpeed.Api.Extensions;
using LightSpeed.Api.Reporting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LightSpeed.Api.Client.V2;

public partial class LightSpeedApiClientV2
{
    partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings)
    {
        settings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        settings.ContractResolver = new SafeContractResolver();
    }

    // See https://github.com/RicoSuter/NSwag/issues/1991
    // Issue when generating properties that aren't required.
    class SafeContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProp = base.CreateProperty(member, memberSerialization);
            jsonProp.Required = Required.Default;
            return jsonProp;
        }
    }

    /// <summary>
    /// Reporting is not included in the public API for LightSpeed.
    /// This is reverse engineered from their website API Calls.
    /// </summary>
    /// <param name="dateFrom"></param>
    /// <param name="dateTo"></param>
    /// <param name="groupBy"></param>
    /// <returns></returns>
    public async Task<ReportResponseExtended> GenerateReportAsync( DateTime dateFrom, DateTime dateTo, GroupBy groupBy)
    {
        var request = CreateRequest(dateFrom, dateTo, groupBy);
        return await GenerateReportAsync(request);
    }

    /// <summary>
    /// Reporting is not included in the public API for LightSpeed.
    /// This is reverse engineered from their website API Calls.
    /// </summary>
    /// <param name="requestObj"></param>
    /// <returns></returns>
    public async Task<ReportResponseExtended> GenerateReportAsync(ReportRequest requestObj)
    {
        var request = await CreateHttpRequestMessageAsync(CancellationToken.None).ConfigureAwait(false);
        request.Method = HttpMethod.Post;

        request.RequestUri = new Uri(Flurl.Url.Parse(BaseUrl).RemovePath().AppendPathSegment("api/2.0/report"));

        var reportConfigJson = requestObj.ToJson();
        request.Content = new System.Net.Http.StringContent(reportConfigJson, System.Text.Encoding.UTF8, "application/json");

        var responseMessage = await _httpClient.SendAsync(request).ConfigureAwait(false);

        var payload = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

        responseMessage.EnsureSuccessStatusCode();

        var result = JsonConvert.DeserializeObject<ReportResponseExtended>(payload);

        var rows = result.Rows.SelectMany(x => x).SelectMany(x => x).ToList();

        var columns = result.Headers.Columns.SelectMany(a => a).ToList();

        if (rows.Count != columns.Count)
            throw new Exception($"Number of rows/columns must match!\r\n\r\n{request.RequestUri}");

        result.RowsFormatted = columns.Select((a, i) =>
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

        return result;
    }

    private static ReportRequest CreateRequest(DateTime dateFrom, DateTime dateTo, GroupBy groupBy = GroupBy.day)
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

    /// <summary>
    /// Attempt a test API call to make sure that the API key/URL are valid etc. 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>An object detailing if the response was successful, and if not, the message and/or HttpStatusCode returned from the API.</returns>
    public async Task<ConnectionResponse> TryConnectAsync(CancellationToken cancellationToken = default)
    {
        var request = await CreateHttpRequestMessageAsync(cancellationToken).ConfigureAwait(false);
        request.Method = HttpMethod.Get;

        request.RequestUri = new Uri(Flurl.Url.Parse(BaseUrl).RemovePath().AppendPathSegment("api/2.0/outlets"));
        
        var responseMessage = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

        var connectionResponse = new ConnectionResponse
        {
            Successful = responseMessage.IsSuccessStatusCode,
            ResponseStatusCode = responseMessage.StatusCode
        };

        try
        {
            var response = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
            connectionResponse.Message = $"Response Status Code: {responseMessage.StatusCode}\r\n{response}";
        }
        catch (Exception ex)
        {
            // eat.
            connectionResponse.Successful = false;
            connectionResponse.Message = ex.Message;
        }

        return connectionResponse;
    }
}