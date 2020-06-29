using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TestFluentMigrator
{
    /// <summary>
    /// https://github.com/fluentmigrator/
    /// https://fluentmigrator.github.io/articles/fluent-interface.html
    /// https://fluentmigrator.github.io/
    /// https://fluentmigrator.github.io/articles/quickstart.html?tabs=runner-dotnet-fm
    /// https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/fluent/types-and-properties/
    /// https://fluentmigrator.github.io/articles/quickstart.html?tabs=runner-in-process
    /// https://bitbucket.org/quentin-starin/easymigrator/wiki/Home
    /// https://volkanceylan.gitbooks.io/serenity-zh-cn/tutorials/movies/creating_movie_table.html
    /// https://www.cnblogs.com/Leo_wl/p/9958495.html
    /// https://www.cnblogs.com/lwqlun/p/10649949.html
    /// EFCore.Sharding(EFCore开源分表框架):https://www.cnblogs.com/coldairarrow/p/12733886.html
    ///
    ///
    ///
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serviceProvider = CreateServices();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddSqlServer2012()
                    //.AddSQLite()
                    // Set the connection string
                    .WithGlobalConnectionString("Server=localhost; Database=ExportDataTarget01; Trusted_Connection=True;")
                    //.WithGlobalConnectionString("Data Source=test.db")
                    // Define the assembly containing the migrations
                    //.ScanIn(typeof(AddLogTable).Assembly).For.Migrations()
                    )
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            //runner.MigrateUp();
            runner.Up(new AddLogTable2("2020-01"));
        }
    }

    [Migration(20180430121800)]
    public class AddLogTable : Migration
    {
        public override void Up()
        {
            Create.Table("Log")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Text").AsString();
        }

        public override void Down()
        {
            Delete.Table("Log");
        }
    }
    public class AddLogTable2 : Migration
    {
        private string Suffix { get; set; }
        public AddLogTable2(string suffix)
        {
            Suffix = suffix;
        }

        public override void Up()
        {
            Create.Table($"Log_{Suffix}")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Text").AsString();
        }

        public override void Down()
        {
        }
    }
}
