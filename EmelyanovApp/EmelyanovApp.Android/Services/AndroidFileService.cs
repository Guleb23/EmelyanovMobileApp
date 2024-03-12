using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using EmelyanovApp.Interfaces;
using EmelyanovApp.Models.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using AndroidX.Core.Content;


[assembly: Dependency(typeof(EmelyanovApp.Droid.Services.AndroidFileService))]
namespace EmelyanovApp.Droid.Services
{

    internal class AndroidFileService : IFileInterface
    {
        public string StorageDirectory => Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath;

        public event EventHandler<DownLoadEventArgs> OnFileDownloaded;

        public void DownloadFile(string url, string folder)
        {
            string pathFolder = Path.Combine(StorageDirectory, folder);
            Directory.CreateDirectory(pathFolder);
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompleted);
                string pathToNewFile = Path.Combine(pathFolder, Path.GetFileName(url + ".pdf"));
                webClient.DownloadFileAsync(new Uri(url), pathToNewFile);
            }
            catch (Exception ex)
            {

                if (OnFileDownloaded != null)
                {
                    OnFileDownloaded.Invoke(this, new DownLoadEventArgs(false));
                }
            }
        }

        public void openFile(string path)
        {
            string extensions = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(new Java.IO.File(path)).ToString());
            string mineType = Android.Webkit.MimeTypeMap.Singleton.GetExtensionFromMimeType(extensions);
            Context context = Android.App.Application.Context;
            Android.Net.Uri uri = FileProvider.GetUriForFile(context, context.PackageName + ".provider", new Java.IO.File(path + ".pdf"));
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(uri, mineType);
            intent.AddFlags(ActivityFlags.NewTask);
            intent.AddFlags(ActivityFlags.GrantReadUriPermission);
            Intent choserIntent = Intent.CreateChooser(intent, "Открыть с помощью:");
            choserIntent.AddFlags(ActivityFlags.NewTask);

            try
            {
                context.StartActivity(intent);
            }
            catch (Exception ex)
            {

                Toast.MakeText(context, "Нету подходящих приложений", ToastLength.Short).Show();
            }


        }

        private void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (OnFileDownloaded != null)
                {
                    OnFileDownloaded.Invoke(this, new DownLoadEventArgs(false));
                }
            }
            else
            {
                if (OnFileDownloaded != null)
                {
                    OnFileDownloaded.Invoke(this, new DownLoadEventArgs(true));
                }
            }
        }
    }
}