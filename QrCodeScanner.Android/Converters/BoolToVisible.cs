using System;
using System.Globalization;
using Android.Views;
using MvvmCross.Platform.Converters;

namespace QrCodeScanner.Android.Converters
{
    public class BoolToVisible : MvxValueConverter<bool, ViewStates>
    {
        protected override ViewStates Convert(bool value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return value ? ViewStates.Visible : ViewStates.Gone;
        }
    }
}