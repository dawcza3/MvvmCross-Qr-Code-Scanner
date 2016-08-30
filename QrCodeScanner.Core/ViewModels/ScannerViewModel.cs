using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Sqlite;
using QrCodeScanner.Core.Services;

namespace QrCodeScanner.Core.ViewModels
{
    public class ScannerViewModel : MvxViewModel
    {
        private QrCodeDataService _dataService = new QrCodeDataService(Mvx.Resolve<IMvxSqliteConnectionFactory>());
        private string _scannerResult;

        public ScannerViewModel()
        {
            ScannerResult = "Skanowanie nie powiodło się, naciśnij przycisk powrotu aby powrócić do menu głównego";
        }

        public void SaveToDatabase(string result)
        {
            _dataService.Insert(result);
            ScannerResult = result;
        }

        public string ScannerResult
        {
            get { return _scannerResult; }
            set
            {
                _scannerResult = value;
                RaisePropertyChanged();
            }
        }

    }
}