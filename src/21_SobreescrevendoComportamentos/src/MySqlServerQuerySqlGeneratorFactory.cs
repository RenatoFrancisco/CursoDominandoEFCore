using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace src
{
    public class MySqlServerQuerySqlGeneratorFactory : SqlServerQuerySqlGeneratorFactory
    {
        private readonly QuerySqlGeneratorDependencies _dependencies;
        public MySqlServerQuerySqlGeneratorFactory(QuerySqlGeneratorDependencies dependencies) : base(dependencies) =>
            _dependencies = dependencies;

        public override QuerySqlGenerator Create() => new MySqlServerQuerySqlGenerator(_dependencies);
    }
}