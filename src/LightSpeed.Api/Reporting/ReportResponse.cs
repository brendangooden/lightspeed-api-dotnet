using System.Collections.Generic;
using Newtonsoft.Json;

namespace LightSpeed.Api.Reporting;

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

public class ReportResponseExtended : ReportResponse
{
    /// <summary>
    /// Contains the formatted response data from the report.
    /// </summary>
    public IList<ReportSummaryData> RowsFormatted { get; set; }
}