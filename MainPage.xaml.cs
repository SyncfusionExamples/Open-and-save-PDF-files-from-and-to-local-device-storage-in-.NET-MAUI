using CommunityToolkit.Maui.Storage;

namespace OpenAndSaveLocalPDF
{
    public partial class MainPage : ContentPage
    {
        string currentFileName;
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Triggers when open button is clicked
        /// </summary>
        private void openButton_Clicked(object sender, EventArgs e)
        {
            FilePickerFileType pdfFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>{
                        { DevicePlatform.iOS, new[] { "com.adobe.pdf" } },
                        { DevicePlatform.Android, new[] { "application/pdf" } },
                        { DevicePlatform.WinUI, new[] { "pdf" } },
                        { DevicePlatform.MacCatalyst, new[] { "pdf" } },
                    });
            PickOptions options = new()
            {
                PickerTitle = "Choose a PDF file",
                FileTypes = pdfFileType,
            };
            PickAndLoadPDF(options);
        }

        /// <summary>
        /// Shows a file picker and let the user choose the required PDF file to view. 
        /// </summary>
        private async void PickAndLoadPDF(PickOptions options)
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    if (result.FileName != null)
                    {
                        if (result.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                        {
                            // Store the name of the picked file. This name will be used when the PDF is saved. 
                            currentFileName = result.FileName;
                            fileNameLabel.Text = currentFileName;
                            Stream pdfStream = await result.OpenReadAsync();

                            // Hide the initial message label if it is showing. 
                            if (initialMessage.IsVisible)
                                initialMessage.IsVisible = false;
                            pdfViewer.LoadDocument(pdfStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message;
                if (ex != null && string.IsNullOrEmpty(ex.Message) == false)
                    message = ex.Message;
                else
                    message = "File open failed.";
                Microsoft.Maui.Controls.Application.Current?.MainPage?.DisplayAlert("Error", message, "OK");
            }
        }

        /// <summary>
        /// Triggers when save as button is clicked
        /// </summary>
        private async void saveAsButton_Clicked(object sender, EventArgs e)
        {
            Stream saveDocumentStream = new MemoryStream();
            await pdfViewer.SaveDocumentAsync(saveDocumentStream);
            var fileSaverResult = await FileSaver.Default.SaveAsync(currentFileName, saveDocumentStream);
            if (fileSaverResult.IsSuccessful)
            {
                ShowToast($"The file is saved to {fileSaverResult.FilePath}");
            }
            else
            {
                ShowToast($"The file is not saved. {fileSaverResult.Exception.Message}");
            }
        }

        /// <summary>
        /// Show the toast message to indicate the result or errors of the saving operation.
        /// </summary>
        /// <param name="message">The message indicating the success or error of the saving operation.</param>
        private async void ShowToast(string message)
        {
#if ANDROID
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, (Android.Widget.ToastLength)3000).Show();
#else
            toastLabel.Text = message;
            await toast.FadeTo(1, 500, Easing.SinIn);
            await toast.FadeTo(0, 4000, Easing.SinIn);
#endif
        }
    }
}
