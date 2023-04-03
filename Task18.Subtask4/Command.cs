using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task18.Subtask4
{
    abstract class Command
    {
        public abstract Task Run(string url);
        public abstract void Cancel();
    }
}
