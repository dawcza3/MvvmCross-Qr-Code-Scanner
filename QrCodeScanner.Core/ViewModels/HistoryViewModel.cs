using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Sqlite;
using QrCodeScanner.Core.Models;
using QrCodeScanner.Core.Services;

namespace QrCodeScanner.Core.ViewModels
{
    public class HistoryViewModel:MvxViewModel
    {
        private QrCodeDataService _dataService = new QrCodeDataService(Mvx.Resolve<IMvxSqliteConnectionFactory>());

        private List<QrCodeResult> _historyItems;
        public List<QrCodeResult> HistoryItems
        {
            get
            {
                return _historyItems;
            }
            set { _historyItems = value; RaisePropertyChanged(); }
        }

        public HistoryViewModel()
        {
            IsProgresBarShow = true;
            WorkWithDataBase();
        }
        
        private bool _isProgresBarShow;
        public bool IsProgresBarShow
        {
            get { return _isProgresBarShow; }
            set { _isProgresBarShow = value; RaisePropertyChanged(); }
        }

        private async void WorkWithDataBase()
        {
            List<QrCodeResult> table = await Task.Run(()=>_dataService.GetAllQrCodeResults());
            HistoryItems = table;
            IsProgresBarShow = false;
        }
    }
}