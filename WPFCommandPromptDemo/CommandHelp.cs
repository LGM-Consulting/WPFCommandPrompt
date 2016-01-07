using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFCommandPromptDemo
{
    public class CommandHelp
    {
        public string Command { get; set; }
        public string CommandDescription { get; set; }
        public string CommandArguments { get; set; }
        public List<string> CommandDetails { get; set; }

        public string ShortDescription()
        {
            return Command + " \t" + CommandDescription;
        }

        public override string ToString()
        {
            StringBuilder help = new StringBuilder();
            help.Append(Command + "\t" + CommandDescription + "\r\t" + CommandArguments);

            foreach (string h in CommandDetails)
            {
                help.Append("\r\t" + h);
            }

            return help.ToString();
        }
    }
}
