using MattyControls;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TabularTool
{
    class CompilerControl : MattyUserControl
    {
        private Btn _btnExit, _btnPrevious;
        private Db _dbCompiler;
        private RichTb _tbOutput;

        public TabularData Data { private get; set; }

        public CompilerControl() {
            _btnPrevious = new Btn("Previous", this, (o, e) => { ShowUserControl<EditDataControl>(); });
            _btnExit = new Btn("Exit", this, (o, e) => { Application.Exit(); });

            _dbCompiler = new Db(this);
            _dbCompiler.SelectedIndexChanged += OnCompilerChange;
            _dbCompiler.Items.AddRange(Compilers.All.Select(p => p.Name).ToArray());
            _dbCompiler.SelectedIndex = Compilers.IndexOf(Settings.Get.SelectedCompiler);

            _tbOutput = new RichTb(this);
            _tbOutput.Multiline = true;
            _tbOutput.AddLabel("Output:", false);
            _tbOutput.Font = new Font(FontFamily.GenericMonospace, _tbOutput.Font.Size);
        }

        public override void OnResize() {
            _dbCompiler.PositionTopRightInside(this);

            _btnExit.PositionBottomRightInside(this);
            _btnPrevious.PositionLeftOf(_btnExit);

            _tbOutput.PositionBelow(_dbCompiler);
            _tbOutput.StretchLeftInside(this);
            _tbOutput.StretchRightInside(this);
            _tbOutput.StretchDownTo(_btnPrevious);
            _tbOutput.Label.PositionAbove(_tbOutput);
        }

        public override void OnShow() {
            _tbOutput.Select();
            Compile();
        }

        private void OnCompilerChange(object o, EventArgs e) {
            Settings.Get.SelectedCompiler = Compilers.All[_dbCompiler.SelectedIndex];
            if (_tbOutput != null && Visible) {
                Compile();
            }
        }

        private void Compile() {
            var compiler = Settings.Get.SelectedCompiler.Value;
            _tbOutput.Text = compiler.Compile(Data);
        }
    }
}
