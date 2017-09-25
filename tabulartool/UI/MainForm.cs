using System.Drawing;
using MattyControls;

namespace TabularTool
{
    class MainForm : MattyForm
    {
        private static Size MIN_SIZE = new Size(220, 150);
        private static Size DEFAULT_SIZE = new Size(700, 450);

        public MainForm() : base(MIN_SIZE, DEFAULT_SIZE, Settings.Get) {
            Text = "TabularTool";
            Icon = Properties.Resources.EyesHybrid;

            AddUserControl(new ParserControl(), new EditDataControl(), new CompilerControl());
            ShowUserControl<ParserControl>();
        }
    }
}
