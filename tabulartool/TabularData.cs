using System.Collections.Generic;
using System.Linq;

namespace TabularTool
{
    public class TabularData
    {
        private string[,] _data;

        public bool FirstRowIsHeader { get; set; }

        public string this[int x, int y] => _data[x, y];
        public int Width => _data.GetLength(0);
        public int Height => _data.GetLength(1);

        public IEnumerable<string> Column(int x) => Enumerable.Range(0, Height).Select(y => this[x, y]);
        public IEnumerable<string> Row(int y) => Enumerable.Range(0, Width).Select(x => this[x, y]);

        public IEnumerable<IEnumerable<string>> Columns => Enumerable.Range(0, Width).Select(Column);
        public IEnumerable<IEnumerable<string>> Rows => Enumerable.Range(0, Height).Select(Row);

        public TabularData() : this(new string[0, 0], false) { }

        public TabularData(string[,] data, bool firstRowIsHeader) {
            _data = data;
            FirstRowIsHeader = firstRowIsHeader;
        }

        public static TabularData FromRows(IEnumerable<IEnumerable<string>> rows) {
            string[][] raw = rows.Select(r => r.ToArray()).ToArray();
            int height = raw.Count();
            int width = raw.Max(r => r.Count());

            string[,] data = new string[width, height];

            for (int y = 0; y < height; y++) {
                var row = raw[y];
                for (int x = 0; x < width; x++) {
                    data[x, y] = x < row.Count() ? (raw[y][x] ?? "") : "";
                }
            }

            bool firstRowIsHeader = Settings.Get.FirstRowIsHeader;

            return new TabularData(data, firstRowIsHeader);
        }
    }
}
