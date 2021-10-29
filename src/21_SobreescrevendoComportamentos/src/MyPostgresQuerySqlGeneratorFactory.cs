using System;
using Microsoft.EntityFrameworkCore.Query;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Internal;

namespace src
{
    public class MyPostgresQuerySqlGeneratorFactory : NpgsqlQuerySqlGeneratorFactory
    {
        private readonly QuerySqlGeneratorDependencies _dependencies;

        public MyPostgresQuerySqlGeneratorFactory(QuerySqlGeneratorDependencies dependencies, INpgsqlOptions npgsqlOptions) : base(dependencies, npgsqlOptions)
            => _dependencies = dependencies;

        public override QuerySqlGenerator Create() => new MyPostgresQuerySqlGenerator(_dependencies, false, new Version("1.0.0.0"));
    }
}