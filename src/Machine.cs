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

        public void Run() 
        {;

            EnsureDatabase.For.PostgresqlDatabase(_connectionString);

            if (_beforeScriptDirectory != null)
            {
                var upgraderBefore = DeployChanges.To
                .PostgresqlDatabase(_connectionString)
                .WithScriptsFromFileSystem(_beforeScriptDirectory)
                .LogToConsole()
                .LogScriptOutput()
                .JournalTo(new NullJournal())
                .Build();

                var resultBefore = upgraderBefore.PerformUpgrade();

                if (!resultBefore.Successful)
                {
                    throw resultBefore.Error;
                }
            }

            var upgrader =
                DeployChanges.To
                .PostgresqlDatabase(_connectionString)
                .WithScriptsFromFileSystem(_scriptDirectory)
                .LogToConsole()
                .LogScriptOutput()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                throw result.Error;
            }

            if (_afterScriptDirectory != null)
            {
                var upgraderAfter = DeployChanges.To
                .PostgresqlDatabase(_connectionString)
                .WithScriptsFromFileSystem(_afterScriptDirectory)
                .LogToConsole()
                .LogScriptOutput()
                .JournalTo(new NullJournal())
                .Build();
            
                var resultAfter = upgraderAfter.PerformUpgrade();

                if (!resultAfter.Successful)
                {
                    throw resultAfter.Error;
                }
            }
        }
    }
}