﻿using CommunityToolkit.Maui;
using Syncfusion.Maui.Core.Hosting;

namespace OpenAndSaveLocalPDF
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.UseMauiCommunityToolkit();
            builder.ConfigureSyncfusionCore();
            return builder.Build();
        }
    }
}
