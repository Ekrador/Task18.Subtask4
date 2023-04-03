using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task18.Subtask4
{
    internal class Sender
    {
        private Command command;

        public void SetCommand(Command command)
        {
            this.command = command;
        }

        public async Task Run(string url)
        {
           await command.Run(url);
        }

        public void Cancel()
        {
            command.Cancel();
        }
    }
}
