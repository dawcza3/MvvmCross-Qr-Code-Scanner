using Android.App;
using Android.OS;
using Android.Widget;
using MvvmCross.Droid.Views;
using QrCodeScanner.Core.ViewModels;
using ZXing.Mobile;
using Result = ZXing.Result;

namespace QrCodeScanner.Android.Views
{
    [Activity(Label = "View for ScannerViewModel")]
    public class ScannerView : MvxActivity
    {
        private MobileBarcodeScanner _scanner;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ScannerView);
            MobileBarcodeScanner.Initialize(Application);
            _scanner = new MobileBarcodeScanner();
            StartScan();
        }

        private async void StartScan()
        {
            var result = await _scanner.Scan();
            if (result != null)
                HandleScanResult(result);
        }

        private void HandleScanResult(Result result)
        {
            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                var scannerViewModel = ViewModel as ScannerViewModel;
                scannerViewModel?.SaveToDatabase(result.Text);
                _scanner.Cancel();
            }
            else
            {
                StartScan();
            }
        }
    }
}