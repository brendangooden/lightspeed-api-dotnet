using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;

namespace LightSpeed.Api.Reporting;

public class ReportSummaryData : RowObject
{
    public DateTime Date { get; set; }
}