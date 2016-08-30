using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;
using MvvmCross.Plugins.Sqlite;
namespace QrCodeScanner.Core.Services
{
    public class Kitten
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public interface IKittenGenesisService
    {
        Kitten CreateNewKitten(string extra = "");
    }

    public class KittenGenesisService : IKittenGenesisService
    {
        public Kitten CreateNewKitten(string extra = "")
        {
            return new Kitten()
            {
                Name = extra,
                Price=100
            };
        }
    }

    public interface IDataService
    {
        List<Kitten> KittensMatching(string nameFilter);
        void Insert(Kitten kitten);
        void Update(Kitten kitten);
        void Delete(Kitten kitten);
        int Count { get; }
    }

    public class DataService:IDataService
    {
        public List<Kitten> KittensMatching(string nameFilter)
        {
            throw new NotImplementedException();
        }

        public void Insert(Kitten kitten)
        {
            throw new NotImplementedException();
        }

        public void Update(Kitten kitten)
        {
            throw new NotImplementedException();
        }

        public void Delete(Kitten kitten)
        {
            throw new NotImplementedException();
        }

        public int Count { get; }
    }
}