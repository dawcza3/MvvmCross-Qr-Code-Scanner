using System.Collections.Specialized;
using System.Drawing;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using QrCodeScanner.Core.ViewModels;
using UIKit;
using ZXing;
using ZXing.Mobile;

namespace QrCodeScanner.iOS.Views
{
    public partial class ScannerView : MvxViewController
    {

        MobileBarcodeScanner _scanner;
        public ScannerView() : base("ScannerView", null)
        {
            _scanner = new MobileBarcodeScanner(NavigationController);
            StartScan();
            _scanner.Cancel();
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

        public override void ViewDidLoad()
        {
            var set = this.CreateBindingSet<ScannerView, Core.ViewModels.ScannerViewModel>();
            
            var textField=new UITextField(new RectangleF(20,20,320,40));
            Add(textField);
            set.Bind(textField).To(vm => vm.ScannerResult);
            //set.Bind().To(vm => vm.ScannerResult);
            set.Apply();

        }
    }
}