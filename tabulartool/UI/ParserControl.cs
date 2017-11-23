using System;
using System.Drawing;
using System.Linq;
using MattyControls;

namespace TabularTool
{
    class ParserControl : MattyUserControl
    {
        private Btn _btnNext;
        private Db _dbParser;
        private RichTb _tbInput;

        public ParserControl() {
            _btnNext = new Btn("Next", this, OnNextClick);

            _dbParser = new Db(this);
            _dbParser.SelectedIndexChanged += OnParserChange;
            _dbParser.Items.AddRange(Parsers.All.Select(p => p.Name).ToArray());
            _dbParser.SelectedIndex = Parsers.IndexOf(Settings.Get.SelectedParser);

            _tbInput = new RichTb(this);
            _tbInput.Multiline = true;
            _tbInput.AddLabel("Input:", false);
            _tbInput.Font = new Font(FontFamily.GenericMonospace, _tbInput.Font.Size);

        }

        public override void OnResize() {
            _dbParser.PositionTopRightInside(this);

            _btnNext.PositionBottomRightInside(this);

            _tbInput.PositionBelow(_dbParser);
            _tbInput.StretchLeftInside(this);
            _tbInput.StretchRightInside(this);
            _tbInput.StretchDownTo(_btnNext);
            _tbInput.Label.PositionAbove(_tbInput);

            var parserControls = Settings.Get.SelectedParser.Value.Controls.ToList();
            ControlHelpers.AnchorLoop(parserControls, c => c.PositionRightOf(_tbInput.Label), (o, c) => c.PositionRightOf(o));
        }

        public override void OnShow() {
            _tbInput.Select();
        }

        private void OnNextClick(object o, EventArgs e) {
            var tabularData = Settings.Get.SelectedParser.Value.Parse(_tbInput.Text);
            var next = ParentMattyForm.GetUserControl<EditDataControl>();
            next.Data = tabularData;
            ShowUserControl(next);
        }

        private void OnParserChange(object o, EventArgs e) {
            if (Settings.Get.SelectedParser.Value.IsInitialized) {
                Settings.Get.SelectedParser.Value.RemoveControls(this);
            }
            Settings.Get.SelectedParser = Parsers.All[_dbParser.SelectedIndex];
            Settings.Get.SelectedParser.Value.InitControls(this);
        }
    }
}
