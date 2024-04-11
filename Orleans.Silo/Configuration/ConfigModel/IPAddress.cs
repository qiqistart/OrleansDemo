using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Silo.Configuration.ConfigModel;
public class IPAddress
{
    public string ipString { get; set; }


    public int siloPort { get; set; }


    public int gatewayPort { get; set; }
}
