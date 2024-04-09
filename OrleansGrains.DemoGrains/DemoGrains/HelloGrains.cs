using Orleans;
using OrleansGrains.DemoGrains.DemoIGrains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OrleansGrains.DemoGrains.DemoGrains
{
    public class HelloGrains : Grain, IHalloGrains
    {
        public async Task Hallo()
        {
            await Task.FromResult("1111");
        }
    }
}
