using System.Linq;

namespace TabularTool
{
    class Parsers
    {
        private static Option<Parser>[] _options;
        public static Option<Parser>[] All => _options ?? BuildOptions();

        private static Option<Parser>[] BuildOptions() {
            return _options = new[]
            {
                new Option<Parser>("Newlines", "newlines", new NewlineParser())
            };
        }

        public static Option<Parser> FromSlug(string slug) => (Option<Parser>.FromSlug(All, slug));
        public static int IndexOf(Option<Parser> option) => (Option<Parser>.IndexOf(All, option));
    }

    class Compilers
    {
        private static Option<Compiler>[] _options;
        public static Option<Compiler>[] All => _options ?? BuildOptions();

        private static Option<Compiler>[] BuildOptions() {
            return _options = new[]
            {
                new Option<Compiler>("Ascii", "ascii", new AsciiCompiler()),
                new Option<Compiler>("SqlInsertScript", "sql-insert-script", new SqlInsertScriptCommpiler())
            };
        }

        public static Option<Compiler> FromSlug(string slug) => (Option<Compiler>.FromSlug(All, slug));
        public static int IndexOf(Option<Compiler> option) => (Option<Compiler>.IndexOf(All, option));
    }

    class Option<T>
    {
        public string Name { get; private set; }
        public string Slug { get; private set; }

        public T Value { get; private set; }

        public Option(string name, T value) : this(name, name, value) { }

        public Option(string name, string slug, T value) {
            Name = name;
            Slug = slug;
            Value = value;
        }

        public static Option<T> FromSlug(Option<T>[] all, string slug) {
            return all.FirstOrDefault(f => f.Slug == slug) ?? all.First();
        }

        public static int IndexOf(Option<T>[] all, Option<T> option) {
            for (int i = 0; i < all.Length; i++) {
                if (all[i] == option) {
                    return i;
                }
            }
            return -1;
        }
    }
}
