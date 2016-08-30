using System.Collections.Generic;
using System.Linq;
using MvvmCross.Plugins.Sqlite;
using QrCodeScanner.Core.Models;
using SQLite.Net;

namespace QrCodeScanner.Core.Services
{
    public class ExampleDataService
    {
        private readonly IMvxSqliteConnectionFactory _sqliteConnectionFactory;
        private SQLiteConnectionWithLock _connection;

        public ExampleDataService(IMvxSqliteConnectionFactory sqliteConnectionFactory)
        {
            _sqliteConnectionFactory = sqliteConnectionFactory;
            var databaseName = "myExampleDatabase.sqlite";
            _connection = _sqliteConnectionFactory.GetConnectionWithLock(databaseName);
            _connection.CreateTable<Example>();
        }

        public void Insert(string result)
        {
            var example = new Example() {Result = result};
            _connection.Insert(example);
        }

        public List<Example> GetAllExamples()
        {
            var list = _connection.Table<Example>().ToList();
            return list;
        }
    }
}