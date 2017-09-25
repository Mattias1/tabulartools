using System.Windows.Forms;

namespace TabularTool
{
    static class Program
    {
        static void Main(string[] args) {
            Settings.Get.Load();

            MainForm main = new MainForm();

            Application.EnableVisualStyles();
            Application.Run(main);

            Settings.Get.Save();
        }
    }
}
