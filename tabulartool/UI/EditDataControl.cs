using MattyControls;
using System;

namespace TabularTool
{
    class EditDataControl : MattyUserControl
    {
        private Btn _btnNext, _btnPrevious;

        public TabularData Data { private get; set; }

        public EditDataControl() {
            _btnNext = new Btn("Next", this);
            _btnNext.Click += OnNextClick;

            _btnPrevious = new Btn("Previous", this);
            _btnPrevious.Click += (o, e) => { ShowUserControl<ParserControl>(); };
        }

        public override void OnResize() {
            _btnNext.PositionBottomRightInside(this);
            _btnPrevious.PositionLeftOf(_btnNext);
        }

        public override void OnShow() { }

        private void OnNextClick(object o, EventArgs e) {
            var next = ParentMattyForm.GetUserControl<CompilerControl>();
            next.Data = Data;
            ShowUserControl(next);
        }
    }
}
