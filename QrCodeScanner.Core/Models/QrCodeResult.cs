using SQLite.Net.Attributes;

namespace QrCodeScanner.Core.Models
{
    public class QrCodeResult
    { 
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Result { get; set; }
        public string DateTime { get; set; }
    }
}