using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace openhab.winrt.winhab.ErrorManager
{
    /// <summary>
    /// This Class is static.
    /// It is used to print Errors to a Logfile.
    /// </summary>
    public static class ErrorLogger
    {
        /// <summary>
        /// Writes StrackTrace and ErrorMessage to the App-Folder
        /// </summary>
        /// <param name="StrackTrace"></param>
        /// <param name="ErrorMessage"></param>
        public static async void printErrorToLog(String stackTrace, String errorMessage)
        {
            StorageFolder storageFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("ErrorLogs", CreationCollisionOption.OpenIfExists);
            Random rnd = new Random();
            String fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month + "." + DateTime.Now.Ticks + ".log";
            StorageFile storageFile = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(storageFile, "DATUM: " + DateTime.Now.ToString() + "\n" + errorMessage + "\n\n" + "DATUM: " + DateTime.Now.ToString() + "\n " + stackTrace);
        }
    }
}
