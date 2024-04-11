using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Silo.Configuration.ConfigModel;

public class Clustering
{
    /// <summary>
    /// 
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Invariant { get; set; }
}
