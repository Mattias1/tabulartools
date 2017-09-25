using System;
using System.Linq;
using MattyControls;

namespace TabularTool
{
    class ParserControl : MattyUserControl
    {
        private Btn _btnNext;
        private Db _dbParser;
        private RichTb _tbInput;

        // These are specific for the newline parser. Since that currently is the only one we have, I'm going to leave it here.
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

        public ParserControl() {
            _btnNext = new Btn("Next", this);
            _btnNext.Click += OnNextClick;

            _dbParser = new Db(this);
            _dbParser.SelectedIndexChanged += OnParserChange;
            _dbParser.Items.AddRange(Parsers.All.Select(p => p.Name).ToArray());
            _dbParser.SelectedIndex = Parsers.IndexOf(Settings.Get.SelectedParser);

            _tbInput = new RichTb(this);
            _tbInput.Multiline = true;
            _tbInput.AddLabel("Input:", false);

            InitColumnControls();
        }

        private void InitColumnControls() {
            _tbColumnRegex = new Tb(this);
            _dbColumnPresets = new Db(this);
            _dbColumnPresets.Items.AddRange(ColumnPresets.Select(p => p.name).ToArray());
            _dbColumnPresets.SelectedIndexChanged += (o, e) => { }; // TODO
        }

        public override void OnResize() {
            _dbParser.PositionTopRightInside(this);
            _dbColumnPresets.PositionLeftOf(_dbParser);
            _tbColumnRegex.PositionLeftOf(_dbColumnPresets);

            _btnNext.PositionBottomRightInside(this);

            _tbInput.PositionBelow(_dbParser);
            _tbInput.StretchLeftInside(this);
            _tbInput.StretchRightInside(this);
            _tbInput.StretchDownTo(_btnNext);
            _tbInput.Label.PositionAbove(_tbInput);
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
            Settings.Get.SelectedParser = Parsers.All[_dbParser.SelectedIndex];
        }
    }
}
