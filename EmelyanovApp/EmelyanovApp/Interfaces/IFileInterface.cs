using EmelyanovApp.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmelyanovApp.Interfaces
{
    public interface IFileInterface
    {
        string StorageDirectory { get; }
        void DownloadFile(string url, string folder);
        event EventHandler<DownLoadEventArgs> OnFileDownloaded;
        void openFile(string path);

    }
}
