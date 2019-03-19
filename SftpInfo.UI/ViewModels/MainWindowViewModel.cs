using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Win32;
using SftpInfo.UI.Commands;
using SftpInfo.Result;
using System.IO;
using SftpInfo.UI.Properties;

namespace SftpInfo.UI.ViewModels
{
   public class MainWindowViewModel : INotifyPropertyChanged
   {
      private SftpClientAdapter _sftpClientAdapter;

      private string _status;

      private string _uploadFilePath;

      public event PropertyChangedEventHandler PropertyChanged;

      public MainWindowViewModel()
      {
         Directories = new ObservableCollection<string>();
         OpenFileCommand = new RelayCommand(param => OpenFileDialog(param));
         SftpConnectCommand = new RelayCommand(param => SftpConnect(param));
         SftpDisconnectCommand = new RelayCommand(param => SftpDisconnect(param));
         SftpCreateDirectoryCommand = new RelayCommand(param => SftpCreateDirectory(param));
         SftpUploadFileCommand = new RelayCommand(param => SftpUploadFile(param));
         SftpListDirectoryCommand = new RelayCommand(param => SftpListDirectory(param));
         SftpDeleteDirectoryCommand = new RelayCommand(param => SftpDeleteDirectory(param));
      }

      public string Status
      {
         get
         {
            return _status;
         }
         set
         {
            _status = value;
            OnPropertyChanged(nameof(Status));
         }
      }

      public string UploadFilePath
      {
         get
         {
            return _uploadFilePath;
         }
         set
         {
            _uploadFilePath = value;
            OnPropertyChanged(nameof(UploadFilePath));
         }
      }

      public ObservableCollection<string> Directories { get; set; }

      public ICommand OpenFileCommand { get; }

      public ICommand SftpConnectCommand { get; }

      public ICommand SftpDisconnectCommand { get; }

      public ICommand SftpCreateDirectoryCommand { get; }

      public ICommand SftpUploadFileCommand { get; }

      public ICommand SftpListDirectoryCommand { get; }

      public ICommand SftpDeleteDirectoryCommand { get; }

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      private void OpenFileDialog(object parameter)
      {
         var openFileDialog = new OpenFileDialog
         {
            Filter = "All files (*.*)|*.*",
            Title = "Select file for SFTP upload"
         };

         if (openFileDialog.ShowDialog() != null)
         {
            UploadFilePath = openFileDialog.FileName;
         }
      }

      private void SftpConnect(object parameter)
      {
         var connectParameters = (object[])parameter;
         string ipAddress = connectParameters[0] as string;
         string userName = connectParameters[1] as string;
         string password = connectParameters[2] as string;

         if (string.IsNullOrEmpty(ipAddress)
             || string.IsNullOrEmpty(userName)
             || string.IsNullOrEmpty(password))
         {
            Status = "Fields Ip address, User name and Password must be set!";
            return;
         }

         _sftpClientAdapter = new SftpClientAdapter(ipAddress, userName, password);
         ReturnResult result = _sftpClientAdapter.Connect();

         Status = result.IsSuccess
            ? "Connect success!"
            : result.ErrorMessage;
      }

      private void SftpDisconnect(object parameter)
      {
         ReturnResult result = _sftpClientAdapter.Disconnect();

         Status = result.IsSuccess
            ? "Disconnect success!"
            : result.ErrorMessage;
      }

      private void SftpCreateDirectory(object parameter)
      {
         string directoryPath = parameter as string;

         if (string.IsNullOrEmpty(directoryPath))
         {
            Status = "Field Upload directory name must be set!";
            return;
         }

         ReturnResult result = _sftpClientAdapter.CreateDirectory(directoryPath);

         Status = result.IsSuccess
            ? "Create directory success!"
            : result.ErrorMessage;
      }

      private void SftpUploadFile(object parameter)
      {
         var uploadFileParameters = (object[])parameter;
         string fileName = uploadFileParameters[0] as string;
         string uploadFilePath = uploadFileParameters[1] as string;

         if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(uploadFilePath))
         {
            Status = "Fields File name and Upload path must be set!";
            return;
         }

         using (Stream uploadFileStream = File.OpenRead(fileName))
         {
            if (uploadFileStream.Length == 0)
            {
               Status = $"Unable to open file {fileName}!";
            }

            ReturnResult result = _sftpClientAdapter.UploadFile(uploadFileStream, uploadFilePath);

            Status = result.IsSuccess
               ? "Upload file success!"
               : result.ErrorMessage;
         }
      }

      private void SftpListDirectory(object parameter)
      {
         string directoryPath = parameter as string;

         if (string.IsNullOrEmpty(directoryPath))
         {
            Status = "Field List directory path must be set!";
            return;
         }

         ListDirectoryResult result = _sftpClientAdapter.ListDirectory(directoryPath);

         Directories.Clear();
         foreach (var sftpFile in result.SftpFiles)
         {
            Directories.Add(sftpFile.FullName);
         }

         Status = result.IsSuccess
            ? "List directory success!"
            : result.ErrorMessage;
      }

      private void SftpDeleteDirectory(object parameter)
      {
         string directoryPath = parameter as string;

         if (string.IsNullOrEmpty(directoryPath))
         {
            Status = "Field Upload directory name must be set!";
            return;
         }

         ReturnResult result = _sftpClientAdapter.DeleteDirectory(directoryPath);

         Status = result.IsSuccess
            ? "Delete directory success!"
            : result.ErrorMessage;
      }
   }
}
