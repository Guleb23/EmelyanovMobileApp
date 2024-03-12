using System;
using System.Collections.Generic;
using System.Text;

namespace EmelyanovApp.Models.Events
{
   
    public class DownLoadEventArgs : EventArgs
    {
        public bool FileSaved = false;
        public DownLoadEventArgs( bool fileSaved)
        {
            FileSaved = fileSaved;
        }
    }
}
