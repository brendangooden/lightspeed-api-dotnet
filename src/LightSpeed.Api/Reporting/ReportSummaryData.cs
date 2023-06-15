using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using LightSpeed.Api.Client.Configuration.Interfaces;
using LightSpeed.Api.Client.Extensions;

namespace LightSpeed.Api.Reporting;

public class ReportSummaryData : RowObject
{
    public DateTime Date { get; set; }
}