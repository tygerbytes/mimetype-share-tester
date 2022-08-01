using System.Threading.Tasks;
using Xamarin.Forms;

namespace MimeTypeShareTester
{
    public static class DialogHelper
    {
        public static async Task DisplayAlert(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, null, "OK");
        }
    }
}