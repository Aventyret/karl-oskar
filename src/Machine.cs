using DbUp;
using DbUp.Helpers;

namespace karl_oskar 
{
    public class Machine 
    {
        private string _connectionString;
        private string _scriptDirectory;
        private string _afterScriptDirectory;
        private string _beforeScriptDirectory;

        public Machine(string cs, string dir, string after, string before) 
        {
            _connectionString = cs;
            _scriptDirectory = dir;
            _afterScriptDirectory = after;
            _beforeScriptDirectory = before;
        }

        private void UpgradeWithoutJournaling(string dir) => Upgrade(dir, true);

        private void Upgrade(string dir, bool skipJournaling = false)
        {
            if (string.IsNullOrWhiteSpace(dir))
                return;

            var builder = DeployChanges.To
                .PostgresqlDatabase(_connectionString)
                .WithScriptsFromFileSystem(dir)
                .LogToConsole()
                .LogScriptOutput();

            if (skipJournaling)
            {
                builder.JournalTo(new NullJournal());
            }

            var result = builder.Build().PerformUpgrade();

            if (!result.Successful)
            {
                throw result.Error;
            }
        }

        public void Run() 
        {
            EnsureDatabase.For.PostgresqlDatabase(_connectionString);
            UpgradeWithoutJournaling(_beforeScriptDirectory);
            Upgrade(_scriptDirectory);
            UpgradeWithoutJournaling(_afterScriptDirectory);
        }
    }
}