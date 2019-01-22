using System;
using DbUp;
using DbUp.Builder;
using DbUp.Helpers;

namespace karl_oskar
{
    public class DbType : IEquatable<DbType>
    {
        public static DbType Sql = new DbType("sql");
        public static DbType Postgresql = new DbType("postgresql");

        private string _name;
        private DbType(string name)
        {
            _name = name;
        }

        public static DbType Parse(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = "postgresql";
                
            if (name.ToLower() == Sql._name)
                return Sql;

            return Postgresql;
        }

        public bool Equals(DbType other) => _name == other._name;

        public UpgradeEngineBuilder GetBuilder(string cn)
        {
            if (this == DbType.Sql)
                return DeployChanges.To.SqlDatabase(cn);

            return DeployChanges.To.PostgresqlDatabase(cn);
        }

        public void EnsureDb(string cn)
        {
            if (this == DbType.Sql)
                EnsureDatabase.For.SqlDatabase(cn);
            else
                EnsureDatabase.For.PostgresqlDatabase(cn);
        }
    }
}