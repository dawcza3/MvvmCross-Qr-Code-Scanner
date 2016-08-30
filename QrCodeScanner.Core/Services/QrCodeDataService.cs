using System.Collections.Generic;
using System.Linq;
using MvvmCross.Plugins.Sqlite;
using QrCodeScanner.Core.Models;
using SQLite.Net;

namespace QrCodeScanner.Core.Services
{
    public class QrCodeDataService
    {
        private readonly IMvxSqliteConnectionFactory _sqliteConnectionFactory;
        private readonly SQLiteConnectionWithLock _connection;

        public QrCodeDataService(IMvxSqliteConnectionFactory sqliteConnectionFactory)
        {
            _sqliteConnectionFactory = sqliteConnectionFactory;
            var databaseName = "myExampleDatabase.sqlite";
            _connection = _sqliteConnectionFactory.GetConnectionWithLock(databaseName);
            _connection.CreateTable<QrCodeResult>();
        }

        public string GetTimeDate()
        {
            var DateTime = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            return DateTime;
        }

        public void Insert(string result)
        {
            var example = new QrCodeResult {Result = result, DateTime = GetTimeDate()};
            _connection.Insert(example);
        }

        public List<QrCodeResult> GetAllQrCodeResults()
        {
            var list = _connection.Table<QrCodeResult>().ToList();
            return list;
        }
    }
}