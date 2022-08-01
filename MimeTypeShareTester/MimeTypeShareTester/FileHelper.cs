using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MimeTypeShareTester
{
    public static class FileHelper
    {
        public static async Task<FileResult> PickAsync(PickOptions options)
        {
            try
            {
                FileResult result = null;
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    result = await FilePicker.PickAsync(options);
                });
                return result;
            }
            catch (Exception)
            {
                // User cancelled or something went wrong
            }

            return null;
        } 
    }
}