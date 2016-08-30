using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using QrCodeScanner.Core.ViewModels;
using UIKit;

namespace QrCodeScanner.iOS.Views
{
    public partial class HistoryView : MvxTableViewController
    {
        public HistoryView() : base("HistoryView", null)
        {
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var source=new MvxStandardTableViewSource(TableView,"TitleText Result");
            TableView.Source = source;

            var set = this.CreateBindingSet<HistoryView, HistoryViewModel>();
            set.Bind(source).To(vm => vm.HistoryItems);
            set.Apply();

            TableView.ReloadData();

            //var set = this.CreateBindingSet<HistoryView, Core.ViewModels.HistoryViewModel>();
            //set.Bind(label1).To(vm => vm.MyText);
            //set.Apply();
        }
    }
}