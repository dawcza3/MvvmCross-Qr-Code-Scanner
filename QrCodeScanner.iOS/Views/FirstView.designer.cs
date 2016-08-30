// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace QrCodeScanner.iOS.Views
{
    [Register ("FirstView")]
    partial class FirstView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton historyButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton scannerButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (historyButton != null) {
                historyButton.Dispose ();
                historyButton = null;
            }

            if (scannerButton != null) {
                scannerButton.Dispose ();
                scannerButton = null;
            }
        }
    }
}