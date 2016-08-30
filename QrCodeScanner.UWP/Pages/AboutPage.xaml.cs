using Windows.UI.Core;

namespace QrCodeScanner.UWP.Pages
{
    public sealed partial class AboutPage : BasePage
    {
        public AboutPage()
        {
            this.InitializeComponent();
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            currentView.BackRequested -= CurrentView_BackRequested;
            currentView.BackRequested += CurrentView_BackRequested;
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
    }
}
