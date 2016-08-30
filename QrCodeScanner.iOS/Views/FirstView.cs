using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace QrCodeScanner.iOS.Views
{
    public partial class FirstView : MvxViewController
    {
        public FirstView() : base("FirstView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<FirstView, Core.ViewModels.FirstViewModel>();
            scannerButton.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin |
                                             UIViewAutoresizing.FlexibleLeftMargin;
            historyButton.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin |
                                             UIViewAutoresizing.FlexibleRightMargin;


           // scannerButton.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleBottomMargin;
            set.Bind(scannerButton).To(vm => vm.GoScannerCommand);
            set.Bind(historyButton).To(vm => vm.GoHistoryCommand);
            set.Apply();
        }
    }
}
