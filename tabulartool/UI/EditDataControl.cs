using MattyControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TabularTool
{
    class EditDataControl : MattyUserControl
    {
        private Btn _btnNext, _btnPrevious;
        private DataGrid _grid;

        public TabularData Data { private get; set; }

        public EditDataControl() {
            _grid = new DataGrid();
            Controls.Add(_grid);

            _btnPrevious = new Btn("Previous", this, (o, e) => { ShowUserControl<ParserControl>(); });
            _btnNext = new Btn("Next", this, OnNextClick);
        }

        private void FillDataGrid() {
            var t = new DataTable();

            for (int x = 0; x < Data.Width; x++) {
                t.Columns.Add();
            }
            foreach (var row in Data.Rows) {
                t.Rows.Add(row.ToArray());
            }

            _grid.DataSource = t;
        }

        public override void OnResize() {
            _btnPrevious.PositionLeftOf(_btnNext);
            _btnNext.PositionBottomRightInside(this);

            _grid.Size = new Size(this.Width, _btnNext.Location.Y);
        }

        public override void OnShow() {
            FillDataGrid();

            _btnNext.Focus();
        }

        private void OnNextClick(object o, EventArgs e) {
            var next = ParentMattyForm.GetUserControl<CompilerControl>();
            next.Data = ParseDataGrid();
            ShowUserControl(next);
        }

        private TabularData ParseDataGrid() {
            var dataRows = ((DataTable)_grid.DataSource).Rows;

            var resultRows = Enumerable.Range(0, dataRows.Count).Select(_ => new List<string>()).ToList();
            for (int i = 0; i < dataRows.Count; i++) {
                resultRows[i] = dataRows[i].ItemArray.Select(obj => obj.ToString()).ToList();
            }
            return TabularData.FromRows(resultRows);
        }
    }
}
