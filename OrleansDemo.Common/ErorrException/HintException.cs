using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrleansDemo.Common.ErorrException
{
    /// <summary>
    ///             
    /// </summary>
    public class HintException : Exception
    {
        /// <summary>
        ///                            
        /// </summary>
        /// <param name="message"></param>
        public HintException(string message) : base(message)
        {

        }
    }
}
