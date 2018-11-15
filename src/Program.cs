using System;
using McMaster.Extensions.CommandLineUtils;

namespace karl_oskar
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var app = new CommandLineApplication();

            app.HelpOption();
            var optScriptDirectory = app.Option("-dir|--script-directory <DIRECTORY>", "Path to migration script directory", CommandOptionType.SingleValue);
            optScriptDirectory.IsRequired(true);
            var optionConnectionString = app.Option("-cs|--connectionstring <CONNECTIONSTRING>", "Connection string", CommandOptionType.SingleValue);
            optionConnectionString.IsRequired(true);
            var optAfterScriptDirectory = app.Option("-after|--after-script-directory <DIRECTORY>", "Path to after script directory", CommandOptionType.SingleValue);
            optAfterScriptDirectory.IsRequired(false);
            var optBeforeScriptDirectory = app.Option("-before|--before-script-directory <DIRECTORY>", "Path to before script directory", CommandOptionType.SingleValue);
            optBeforeScriptDirectory.IsRequired(false);

            app.OnExecute(() =>
            {
                var dir = optScriptDirectory.HasValue() ? optScriptDirectory.Value() : throw new ArgumentException($"{optScriptDirectory.LongName} is mandatory");
                var cs = optionConnectionString.HasValue() ? optionConnectionString.Value() : throw new ArgumentException($"{optScriptDirectory.LongName} is mandatory");
                var after = optAfterScriptDirectory.Value();
                var before = optBeforeScriptDirectory.Value();

                var machine = new Machine(cs, dir, after, before);
                machine.Run();

                return 0;
            });

            return app.Execute(args);
        }
    }
}
