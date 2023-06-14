using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightSpeed.Api.Shared.Interfaces;

public interface ILightSpeedStore
{
    /// <summary>
    /// The domain portion of the store URL e.g. "outpost"  from https://outpost.vendhq.com
    /// </summary>
    string StoreDomain { get; }
}