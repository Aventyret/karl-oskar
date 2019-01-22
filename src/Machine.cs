using System;
using System.IO;
using DbUp;
using DbUp.Builder;
using DbUp.Helpers;

namespace karl_oskar 
{
    public class Machine 
    {
        private string _connectionString;
        private string _scriptDirectory;
        private string _afterScriptDirectory;
        private string _beforeScriptDirectory;
        private readonly DbType _dbType;

        public Machine(string cs, string dir, string after, string before, DbType dbType) 
        {
            _connectionString = cs;
            _scriptDirectory = dir;
            _afterScriptDirectory = after;
            _beforeScriptDirectory = before;
            _dbType = dbType;
        }

        private void UpgradeWithoutJournaling(string dir) => Upgrade(dir, true);

        private void Upgrade(string dir, bool skipJournaling = false)
        {
            if (string.IsNullOrWhiteSpace(dir))
                return;

            System.Console.WriteLine($"Running scripts from {Path.Join(Directory.GetCurrentDirectory(), dir)}");

            var builder = _dbType.GetBuilder(_connectionString)
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
            _dbType.EnsureDb(_connectionString);
            UpgradeWithoutJournaling(_beforeScriptDirectory);
            Upgrade(_scriptDirectory);
            UpgradeWithoutJournaling(_afterScriptDirectory);
        }
    }
}