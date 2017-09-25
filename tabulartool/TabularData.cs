using System.Collections.Generic;

namespace TabularTool
{
    public class TabularData
    {
        public List<List<string>> Data { get; private set; }

        public TabularData() {
            Data = new List<List<string>>();
        }
    }
}
