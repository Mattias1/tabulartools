using MattyControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TabularTool
{
    public class NewlineParser : Parser
    {
        private Tb _tbColumnRegex;
        private Db _dbColumnPresets;

        private (string name, string regex)[] ColumnPresets {
            get => new[] {
                ("Whitespace", @"\ \ +|\ *\t+\ *"),
                ("Commas", @",\s*"),
                ("Semicolons", @";"),
                ("Vertical bars", @"|")
            };
        }

        public override void InitControls(MattyUserControl userControl) {
            _tbColumnRegex = new Tb(userControl);

            _dbColumnPresets = new Db(userControl);
            _dbColumnPresets.Items.AddRange(ColumnPresets.Select(p => p.name).ToArray());
            _dbColumnPresets.SelectedIndexChanged += (o, e) => {
                _tbColumnRegex.Text = ColumnPresets[_dbColumnPresets.SelectedIndex].regex;
            };
            // TODO: add these to options and read from there
            if (_dbColumnPresets.SelectedIndex < 0) {
                _dbColumnPresets.SelectedIndex = 0;
            }

            Controls = new Control[] { _tbColumnRegex, _dbColumnPresets };
            base.InitControls(userControl);
        }

        public override TabularData Parse(string input) {
            var lines = SplitLines(input);
            var regex = new Regex(_tbColumnRegex.Text);

            var rows = lines.Select(l => regex.Replace(l, "\n").Split('\n'));
            rows = FilterWhitespace(rows);
            return TabularData.FromRows(rows);
        }

        private string[] SplitLines(string input) {
            return input
                .Replace("\r\n", "\n")
                .Split('\n');
        }

        private IEnumerable<T> FilterWhitespace<T>(IEnumerable<T> rows) where T : IEnumerable<string> {
            return rows.Where(l => l.Any(s => !string.IsNullOrWhiteSpace(s)));
        }
    }
}
