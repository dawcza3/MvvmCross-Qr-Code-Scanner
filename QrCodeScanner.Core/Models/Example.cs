using SQLite.Net.Attributes;

namespace QrCodeScanner.Core.Models
{
    public class Example
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Result { get; set; }
    }
}