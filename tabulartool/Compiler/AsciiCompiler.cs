using System;
using System.Linq;

namespace TabularTool
{
    public class AsciiCompiler : Compiler
    {
        protected override void Compile() {
            var cols = Data.Columns.ToArray();
            var widths = cols.Select(cs => cs.Max(c => c.Length + 1)).ToArray();

            for (int y = 0; y < Data.Height; y++) {
                for (int x = 0; x < Data.Width; x++) {
                    string val = Data[x, y];
                    StringBuilder.Append(val.PadRight(widths[x]));
                }
                StringBuilder.AppendLine();

                if (Data.FirstRowIsHeader && y == 0) {
                    StringBuilder.AppendLine("".PadRight(widths.Sum(), '-'));
                }
            }
        }
    }
}
