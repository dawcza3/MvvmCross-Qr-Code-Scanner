using Windows.Phone.UI.Input;
using MvvmCross.WindowsCommon.Views;
namespace QrCodeScanner.WindowsPhone.Views
{

    public sealed partial class ResultView : MvxWindowsPage
    {
        public ResultView()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if(Frame.CanGoBack)
                Frame.GoBack();
        }
    }
}
