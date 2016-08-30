using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace QrCodeScanner.Android.Views
{
    [Activity(Label = "View for HistoryViewModel")]
    public class HistoryView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.HistoryView);
        }
    }
}