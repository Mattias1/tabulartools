using System;

namespace TabularTool
{
    public class NewlineParser : Parser
    {
        public override TabularData Parse(string input) {
            return new TabularData();
        }

        private string[] SplitLines(string input) {
            return input
                .Replace("\r\n", "\n")
                .Split(new char[] { '\n' });
        }
    }
}
