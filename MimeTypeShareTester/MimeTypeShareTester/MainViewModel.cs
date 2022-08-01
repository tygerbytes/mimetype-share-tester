using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MimeTypeShareTester
{
    public class MainViewModel : ObservableObject
    {
        private FileResult _selectedFileResult;

        public MainViewModel()
        {
            PickFileCommand = new AsyncCommand(PickFileAsync);
            ShareFileCommand = new AsyncCommand(ShareFileAsync);
        }

        public string SelectedFileDisplayName
        {
            get
            {
                if (_selectedFileResult == null)
                {
                    return "(No file selected)";
                }

                return _selectedFileResult.FileName;
            }
        }
        
        private string _mimeType;
        public string MimeType
        {
            get => _mimeType;
            set => SetProperty(ref _mimeType, value);
        }


        private async Task PickFileAsync()
        {
            var fileOptions = new PickOptions
            {
                PickerTitle = $"Select a file",
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    {DevicePlatform.Android, new[] {"*/*"}},
                    {DevicePlatform.iOS, new[] {"*/*"}}
                })
            };
            var result = await FileHelper.PickAsync(fileOptions);
            if (result == null)
            {
                return;
            }

            _selectedFileResult = result;

            OnFilePicked(); 
        }
        
        private void OnFilePicked()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                NotifyPropertyChanged(nameof(SelectedFileDisplayName));
            });
        }
        
        public ICommand PickFileCommand { get; }

        public ICommand ShareFileCommand { get; }
        private async Task ShareFileAsync()
        {
            var path = _selectedFileResult?.FullPath;
            if (path == null)
            {
                await DialogHelper.DisplayAlert("Oops", "Nothing to share");
                return;
            }

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Share file",
                File = new ShareFile(path, MimeType),
            }); 
        }
    }
}