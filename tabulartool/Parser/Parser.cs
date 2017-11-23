using MattyControls;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TabularTool
{
    public abstract class Parser
    {
        public virtual bool IsInitialized { get; protected set; }

        public virtual Control[] Controls { get; protected set; }

        public virtual void InitControls(MattyUserControl userControl) => IsInitialized = true;

        public virtual void RemoveControls(MattyUserControl userControl) {
            foreach (var c in Controls) {
                userControl.Controls.Remove(c);
                c.Dispose();
            }

            IsInitialized = false;
        }

        public abstract TabularData Parse(string input);

        protected bool StartsWith(string line, string start) {
            return MatchesPattern(line, $@"^\s*{start}");
        }

        protected bool EndsWith(string line, string end) {
            return MatchesPattern(line, $@"{end}\s*$");
        }

        protected bool MatchesPattern(string line, string pattern) {
            return new Regex(pattern).IsMatch(line);
        }

        protected string GetPatternMatch(string line, string pattern, int groupNr = 1) {
            var matches = new Regex(pattern).Matches(line, 0);
            if (matches.Count == 0)
                return null;
            var groups = matches[0].Groups;

            if (groups.Count <= groupNr)
                return null;
            return groups[groupNr].Value;
        }

        protected string[] GetPatternMatches(string line, string pattern, params int[] groupNrs) {
            var matches = new Regex(pattern).Matches(line, 0);
            if (matches.Count == 0)
                return null;
            var groups = matches[0].Groups;

            string[] result = new string[groupNrs.Length > 0 ? groupNrs.Length : groups.Count - 1];
            int r = 0;
            for (int i = 1; i < groups.Count; i++) {
                if (groupNrs.Length == 0 || groupNrs.Contains(i)) {
                    result[r++] = groups[i].Value;
                }
            }
            return result;
        }
    }
}
