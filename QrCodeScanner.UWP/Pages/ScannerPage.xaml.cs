using Windows.UI.Core;
using Windows.UI.Xaml;
using QrCodeScanner.Core.ViewModels;
using ZXing;
using ZXing.Mobile;

namespace QrCodeScanner.UWP.Pages
{
    public sealed partial class ScannerPage : BasePage
    {
        MobileBarcodeScanner _scanner;

        public ScannerPage()
        {
            InitializeComponent();
        }

        private void CurrentView_BackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            if (Frame.CanGoBack)
            {
                if (Singleton.Instance.CanScan) Singleton.Instance.CanScan = false;
                Frame.GoBack();
            }
        }

        private void StartScan()
        {
            _scanner.Scan().ContinueWith(t =>
            {
                if (t.Result != null)
                    HandleScanResult(t.Result);
            });
        }

        private void HandleScanResult(Result result)
        {
            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                var scannerViewModel = ViewModel as ScannerViewModel;
                scannerViewModel?.SaveToDatabase(result.Text);
                Singleton.Instance.CanScan = false;
                _scanner = null;
                //_scanner.Cancel();
            }
            else
            {
                StartScan();
            }
        }

        private void ScannerPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            currentView.BackRequested -= CurrentView_BackRequested;
            currentView.BackRequested += CurrentView_BackRequested;

            if (Singleton.Instance.CanScan)
            {
                _scanner = null;
                _scanner = new MobileBarcodeScanner(this.Dispatcher);
                _scanner.Dispatcher = Dispatcher;
                StartScan();
            }
        }
    }
}
