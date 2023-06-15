using System.Collections.Generic;
using Newtonsoft.Json;

namespace LightSpeed.Api.Reporting;

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