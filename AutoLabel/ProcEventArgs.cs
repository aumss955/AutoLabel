using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLabel
{
    public class ProcEventArgs : EventArgs
    {
        public DataStorage EvDataStorage { set; get; }
        public double EvPercentCompleted { set; get; }
    }
}
