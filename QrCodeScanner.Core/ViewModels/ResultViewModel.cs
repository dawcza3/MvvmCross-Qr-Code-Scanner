using MvvmCross.Core.ViewModels;

namespace QrCodeScanner.Core.ViewModels
{
    public class ResultViewModel : MvxViewModel
    {
        private string _result="Nie udało się zeskanować QR-kodu";

        public string Result
        {
            get { return _result; }
            set { _result = value; RaisePropertyChanged(); }
        }


    }
}