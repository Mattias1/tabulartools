using System.Linq;
using MattyControls;

namespace TabularTool
{
    class Settings : SettingsSingleton
    {
        protected override string Name => "TabularTool";

        public static Settings Get => GetSingleton<Settings>();

        public Option<Parser> SelectedParser {
            get { return Parsers.FromSlug(get("selected-parser", Parsers.All.First().Slug)); }
            set { set("selected-parser", value.Slug); }
        }

        public Option<Compiler> SelectedCompiler {
            get { return Compilers.FromSlug(get("selected-compiler", Compilers.All.First().Slug)); }
            set { set("selected-compiler", value.Slug); }
        }
    }
}
