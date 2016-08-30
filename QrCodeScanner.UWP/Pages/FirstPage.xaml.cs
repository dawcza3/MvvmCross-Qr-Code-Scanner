
using System.Diagnostics;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace QrCodeScanner.UWP.Pages
{

    public sealed partial class FirstPage : BasePage
    {
        public FirstPage()
        {
            InitializeComponent();
        }


        private void FirstPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            Singleton.Instance.CanScan = true;
        }
    }
}
