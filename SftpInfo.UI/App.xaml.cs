using System.Windows;
using SftpInfo.UI.Configuration;

namespace SftpInfo.UI
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {
      protected override void OnStartup(StartupEventArgs e)
      {
         BootStrapper.InitializeIocContainer();
      }
   }
}
