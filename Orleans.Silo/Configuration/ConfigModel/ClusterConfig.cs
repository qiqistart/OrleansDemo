using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Silo.Configuration.ConfigModel;

public class ClusterConfig
{
    /// <summary>
    /// 
    /// </summary>
    public string ClusterId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ServiceId { get; set; }
}
