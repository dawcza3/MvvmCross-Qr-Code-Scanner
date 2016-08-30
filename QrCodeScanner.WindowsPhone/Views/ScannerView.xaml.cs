using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Graphics.Display;
using Windows.Media.Devices;
using Windows.Phone.UI.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Lumia.Imaging;
using MvvmCross.WindowsCommon.Views;
using QrCodeScanner.Core.ViewModels;
using ZXing;
using ZXing.Common;
using Panel = Windows.Devices.Enumeration.Panel;

namespace QrCodeScanner.WindowsPhone.Views
{
    public sealed partial class ScannerView : MvxWindowsPage
    {
        private readonly BarcodeReader _reader;
        private CameraPreviewImageSource _cameraPreviewImageSource;
        private bool _decoding;
        private bool _focusing;
        private double _height;

        private bool _initialized;
        private bool _isRendering;

        private DateTime _lastFrame = DateTime.Now;
        private bool _scanStopped;
        private DispatcherTimer _timer;
        private VideoDeviceController _videoDevice;

        private double _width;
        private WriteableBitmap _writeableBitmap;
        private WriteableBitmapRenderer _writeableBitmapRenderer;


        public ScannerView()
        {
            InitializeComponent();
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            _reader = new BarcodeReader
            {
                AutoRotate = true,
                Options = new DecodingOptions
                {
                    TryHarder = true
                }
            };
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            StatusBar.GetForCurrentView().HideAsync();
            Init();
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;

            if (rootFrame != null && rootFrame.CanGoBack)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        private async void Init()
        {
            //Get back camera
            var devices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
            var backCameraId =
                devices.FirstOrDefault(x => x.EnclosureLocation != null && x.EnclosureLocation.Panel == Panel.Back).Id;

            //Start preview
            _cameraPreviewImageSource = new CameraPreviewImageSource();
            await _cameraPreviewImageSource.InitializeAsync(backCameraId);
            var properties = await _cameraPreviewImageSource.StartPreviewAsync();

            //Setup preview
            _width = 640.0;
            _height = _width/properties.Width*properties.Height;
            var bitmap = new WriteableBitmap((int) _width, (int) _height);

            _writeableBitmap = bitmap;

            PreviewImage.Source = _writeableBitmap;

            _writeableBitmapRenderer = new WriteableBitmapRenderer(_cameraPreviewImageSource, _writeableBitmap);

            _cameraPreviewImageSource.PreviewFrameAvailable += OnPreviewFrameAvailable;

            _videoDevice = (VideoDeviceController) _cameraPreviewImageSource.VideoDeviceController;

            //Set timer for auto focus
            if (_videoDevice.FocusControl.Supported)
            {
                var focusSettings = new FocusSettings
                {
                    AutoFocusRange = AutoFocusRange.Macro,
                    Mode = FocusMode.Auto,
                    WaitForFocus = false,
                    DisableDriverFallback = false
                };

                _videoDevice.FocusControl.Configure(focusSettings);

                _timer = new DispatcherTimer
                {
                    Interval = new TimeSpan(0, 0, 0, 2, 0)
                };
                _timer.Tick += TimerOnTick;
                _timer.Start();
            }

            await _videoDevice.ExposureControl.SetAutoAsync(true);

            _initialized = true;
        }

        private async void Clean()
        {
            if (_cameraPreviewImageSource != null)
            {
                await _cameraPreviewImageSource.StopPreviewAsync();
                _cameraPreviewImageSource.PreviewFrameAvailable -= OnPreviewFrameAvailable;
            }

            if (_timer != null)
            {
                _timer.Stop();
            }
        }

        private async void OnPreviewFrameAvailable(IImageSize args)
        {
            if (!_initialized || _isRendering)
                return;

            _isRendering = true;

            await Task.Run(() => { _writeableBitmap.Invalidate(); });

            await _writeableBitmapRenderer.RenderAsync();

            await Task.Run(() => { Deocode(_writeableBitmap.PixelBuffer.ToArray(), BitmapFormat.Unknown); });

            _isRendering = false;
        }

        private async void Deocode(byte[] rawRgb, BitmapFormat bitmapFormat)
        {
            await Task.Run(() =>
            {
                if (_decoding)
                    return;

                _decoding = true;

                var decoded = _reader.Decode(rawRgb, (int) _width, (int) _height, bitmapFormat);

                if (decoded != null)
                {
                    _scanStopped = true;
                    if (_scanStopped)
                    {
                        var scannerViewModel = DataContext as ScannerViewModel;
                        if (scannerViewModel != null)
                            scannerViewModel.GoResultCommand.Execute(null);
                    }
                }
                _decoding = false;
            });
        }

        private async void Focus()
        {
            if (_focusing || !_initialized || _videoDevice == null || !_videoDevice.FocusControl.Supported)
                return;

            _focusing = true;
            await _videoDevice.FocusControl.LockAsync();
            await _videoDevice.FocusControl.FocusAsync();
            await _videoDevice.FocusControl.UnlockAsync();
            _focusing = false;
        }

        private void TimerOnTick(object sender, object o)
        {
            Focus();
        }

        private void MainView_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Focus();
        }

        private async Task Stop(string text)
        {
            _cameraPreviewImageSource.PreviewFrameAvailable -= OnPreviewFrameAvailable;
            await _cameraPreviewImageSource.StopPreviewAsync();
            //resultText.Visibility = Visibility.Visible;
            //FpsCounter.Visibility = Visibility.Collapsed;
            //borderImage.Visibility = Visibility.Collapsed;
            //PreviewImage.Visibility = Visibility.Collapsed;
            //resultText.Text = text;
        }
    }
}