# Open and save PDF files from and to local device storage in .NET-MAUI

This repository contains an example that demonstrates how to open and save PDF files from and to local device storage using the Syncfusion&reg; .NET-MAUI PDF Viewer.

## Prerequisites

1. A .NET MAUI project set up.
2. The [Syncfusion.Maui.PdfViewer](https://www.nuget.org/packages/Syncfusion.Maui.PdfViewer) package.
3. The [CommunityToolkit.Maui](https://www.nuget.org/packages/CommunityToolkit.Maui) package.

## How to open and save PDF files using local device storage.

### 1. Install Required NuGet Package

Create a new [MAUI App](https://dotnet.microsoft.com/en-us/learn/maui/first-app-tutorial/create), install the [Syncfusion.Maui.PdfViewer](https://www.nuget.org/packages/Syncfusion.Maui.PdfViewer) and [CommunityToolkit.Maui](https://www.nuget.org/packages/CommunityToolkit.Maui) packages using either.

* NuGet Package Manager
* NuGet CLI

### 2. Namespace required

**C#:**

```csharp
    using CommunityToolkit.Maui.Storage;
```

### 3. Initialize and Configure the PDF Viewer

Start by adding the Syncfusion PDF Viewer control to your XAML file.

#### a. Add the Syncfusion namespace in `MainPage.xaml`

This namespace enables access to the PDF Viewer control.

**XAML:**

```xaml
    xmlns:pv="clr-namespace:Syncfusion.Maui.PdfViewer;assembly=Syncfusion.Maui.PdfViewer"
```

#### b. Add the PDF Viewer to your layout

**XAML:**

```xaml
     <Grid>
        <pv:SfPdfViewer x:Name="pdfViewer"/>
     </Grid>
 ```

### 4. Create open and save button.

Create open and save button to open and save in the local storage using .NET MAUI PdfViewer.

**XAML:**

```xaml
    <HorizontalStackLayout HorizontalOptions="End" Spacing="5" Margin="0,0,10,0">
        <Button x:Name="openButton" ToolTipProperties.Text="Open PDF" VerticalOptions="Center" Text="&#xe712;" FontFamily="Maui Material Assets" />
        <Button x:Name="saveAsButton" ToolTipProperties.Text="Save as" VerticalOptions="Center" Text="&#xe75f;" FontFamily="Maui Material Assets" />
    </HorizontalStackLayout>
```

### 5. Create event handler for open button.

In open button event handler, platform-specific file type filters are defined to ensure the file picker displays only compatible PDF files across different operating systems. The file picker is then configured with a custom title and the appropriate file type settings. Once launched, it waits for the user to select a PDF file. After selection, the application opens a read stream from the chosen file, allowing access to its contents. Finally, get the stream and load the pdf in the PdfViewer control using the [LoadDocument](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.PdfViewer.SfPdfViewer.html#Syncfusion_Maui_PdfViewer_SfPdfViewer_LoadDocument_System_IO_Stream_System_String_System_Nullable_Syncfusion_Maui_PdfViewer_FlattenOptions__) method.

**C#:**

```csharp
    private void openButton_Clicked(object sender, EventArgs e)
    {
        // Define platform-specific file types for the file picker.
        FilePickerFileType pdfFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>{
                    { DevicePlatform.iOS, new[] { "com.adobe.pdf" } },
                    { DevicePlatform.Android, new[] { "application/pdf" } },
                    { DevicePlatform.WinUI, new[] { "pdf" } },
                    { DevicePlatform.MacCatalyst, new[] { "pdf" } },
                });

        // Configure the file picker options.
        PickOptions options = new()
        {
            PickerTitle = "Choose a PDF file",
            FileTypes = pdfFileType,
        };

        // Launch the file picker and wait for user selection.
        var result = await FilePicker.Default.PickAsync(options);

        // Check if a file was selected.
        if (result != null)
        {
            // Ensure the file has a name.
            if (result.FileName != null)
            {
                // Validate the file extension (case-insensitive).
                if (result.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    // Get the stream.
                    Stream pdfStream = await result.OpenReadAsync();

                    // Load the Pdf in the PdfViewer control using LoadDocument method
                    pdfViewer.LoadDocument(pdfStream);
                }
            }
        }
    }
```

### 6. Create event handler for save button.

In save button event handler, create a memory stream and save the current document content into the memory stream, then save the pdf in the local storage using the `SaveAsync` method in the CommunityToolkit.Maui library.

**C#:**

```csharp
    private async void saveAsButton_Clicked(object sender, EventArgs e)
    {
        // Create a new memory stream to hold the saved PDF document
        Stream saveDocumentStream = new MemoryStream();

        // Asynchronously save the current document content into the memory stream
        await pdfViewer.SaveDocumentAsync(saveDocumentStream);

        // Save the pdf in the local storage using SaveAsync method in the `FileSaver` class present in the CommunityToolkit.Maui source.
        var fileSaverResult = await FileSaver.Default.SaveAsync("your file name", saveDocumentStream);
    }
```

### 7. Wire the event handlers for open and save button.

**XAML:**

```xaml
    <HorizontalStackLayout HorizontalOptions="End" Spacing="5" Margin="0,0,10,0">
        <Button x:Name="openButton" ToolTipProperties.Text="Open PDF" VerticalOptions="Center" Text="&#xe712;" FontFamily="Maui Material Assets" Clicked="openButton_Clicked" />
        <Button x:Name="saveAsButton" ToolTipProperties.Text="Save as" VerticalOptions="Center" Text="&#xe75f;" FontFamily="Maui Material Assets" Clicked="saveAsButton_Clicked" />
    </HorizontalStackLayout>
```

### Conclusion

We hope you enjoyed learning how to open and save PDF files from and to local device storage using .NET-MAUI PDF Viewer.

Refer to our [.NET MAUI PDF Viewer's feature tour](https://www.syncfusion.com/maui-controls/maui-pdf-viewer) page to learn about its other groundbreaking feature representations. You can also explore our [.NET MAUI PDF Viewer Documentation](https://help.syncfusion.com/maui/pdf-viewer/getting-started) to understand how to present and manipulate data.

For current customers, check out our .NET MAUI components on the [License and Downloads](https://www.syncfusion.com/sales/teamlicense) page. If you are new to Syncfusion, try our 30-day [free trial](https://www.syncfusion.com/downloads/maui) to explore our .NET MAUI PDF Viewer and other .NET MAUI components.

Please let us know in the following comments if you have any queries or require clarifications. You can also contact us through our [support forums](https://www.syncfusion.com/downloads/maui), [support ticket](https://support.syncfusion.com/create) or [feedback portal](https://www.syncfusion.com/feedback/maui). We are always happy to assist you!
