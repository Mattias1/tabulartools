using MattyControls;
using System;
using System.Linq;

namespace TabularTool
{
    class CompilerControl : MattyUserControl
    {
        private Btn _btnPrevious;
        private Db _dbCompiler;
        private RichTb _tbOutput;

        public TabularData Data { private get; set; }

        public CompilerControl() {
            _btnPrevious = new Btn("Previous", this);
            _btnPrevious.Click += (o, e) => { ShowUserControl<EditDataControl>(); };

            _dbCompiler = new Db(this);
            _dbCompiler.SelectedIndexChanged += OnCompilerChange;
            _dbCompiler.Items.AddRange(Compilers.All.Select(p => p.Name).ToArray());
            _dbCompiler.SelectedIndex = Compilers.IndexOf(Settings.Get.SelectedCompiler);

            _tbOutput = new RichTb(this);
            _tbOutput.Multiline = true;
            _tbOutput.AddLabel("Output:", false);
        }

        public override void OnResize() {
            _dbCompiler.PositionTopRightInside(this);

            _btnPrevious.PositionBottomRightInside(this);

            _tbOutput.PositionBelow(_dbCompiler);
            _tbOutput.StretchLeftInside(this);
            _tbOutput.StretchRightInside(this);
            _tbOutput.StretchDownTo(_btnPrevious);
            _tbOutput.Label.PositionAbove(_tbOutput);
        }

        public override void OnShow() {
            _tbOutput.Select();
        }

        private void OnCompilerChange(object o, EventArgs e) {
            Settings.Get.SelectedCompiler = Compilers.All[_dbCompiler.SelectedIndex];
        }
    }
}
