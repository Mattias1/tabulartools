using MattyControls;
using System;

namespace TabularTool
{
    class EditDataControl : MattyUserControl
    {
        private Btn _btnNext, _btnPrevious;

        public TabularData Data { private get; set; }

        public EditDataControl() {
            _btnPrevious = new Btn("Previous", this, (o, e) => { ShowUserControl<ParserControl>(); });
            _btnNext = new Btn("Next", this, OnNextClick);
        }

        public override void OnResize() {
            _btnPrevious.PositionLeftOf(_btnNext);
            _btnNext.PositionBottomRightInside(this);
        }

        public override void OnShow() {
            _btnNext.Focus();
        }

        private void OnNextClick(object o, EventArgs e) {
            var next = ParentMattyForm.GetUserControl<CompilerControl>();
            next.Data = Data;
            ShowUserControl(next);
        }
    }
}
