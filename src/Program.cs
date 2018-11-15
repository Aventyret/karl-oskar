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
            optScriptDirectory.IsRequired();
            var optionConnectionString = app.Option("-cs|--connectionstring <CONNECTIONSTRING>", "Connection string", CommandOptionType.SingleValue);
            optionConnectionString.IsRequired();
            var optAfterScriptDirectory = app.Option("-after|--after-script-directory <DIRECTORY>", "Path to after script directory", CommandOptionType.SingleValue);
            var optBeforeScriptDirectory = app.Option("-before|--before-script-directory <DIRECTORY>", "Path to before script directory", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                var dir = optScriptDirectory.Value();
                var cs = optionConnectionString.Value();
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
