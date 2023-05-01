using Microsoft.Maui.Platform;

namespace MauiWindowsWebViewScreenshotSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            string saveImagePath = "image.png";

#if WINDOWS
            Stream stream = new FileStream(saveImagePath, FileMode.Create);

            await ((MauiWebView)webView.Handler.PlatformView).CoreWebView2
                .CapturePreviewAsync(Microsoft.Web.WebView2.Core.CoreWebView2CapturePreviewImageFormat.Png,
                stream.AsRandomAccessStream());
#else
            var stream = await webView.CaptureAsync();

            using (var fileStream = new FileStream(saveImagePath, FileMode.Create))
            {
                await stream.CopyToAsync(fileStream, ScreenshotFormat.Png, 100);
            }
#endif
        }
    }
}