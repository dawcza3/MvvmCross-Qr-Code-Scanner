using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Sqlite;
using QrCodeScanner.Core.Services;

namespace QrCodeScanner.Core.ViewModels
{
    public class FirstViewModel
        : MvxViewModel
    {
       // private QrCodeDataService _dataService = new QrCodeDataService(Mvx.Resolve<IMvxSqliteConnectionFactory>());

        #region Commands

        private MvxCommand _goScannerCommand;
        public MvxCommand GoScannerCommand
        {
            get
            {
                return _goScannerCommand ??
                       (_goScannerCommand = new MvxCommand(() => { ShowViewModel<ScannerViewModel>(); }));
            }
        }

        private MvxCommand _goAboutCommand;

        public MvxCommand GoAboutCommand
        {
            get
            {
                if(_goAboutCommand==null) _goAboutCommand=new MvxCommand(() =>
                {
                    ShowViewModel<AboutViewModel>();
                });
                return _goAboutCommand;
            }
        }

        private MvxCommand _goHistoryCommand;
        public MvxCommand GoHistoryCommand
        {
            get
            {
                return _goHistoryCommand ??
                       (_goHistoryCommand = new MvxCommand(() => { ShowViewModel<HistoryViewModel>(); }));
            }
        }

        #endregion
    }
}
