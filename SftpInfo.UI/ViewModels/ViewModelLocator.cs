using SftpInfo.UI.Configuration;
using SimpleIoc;

namespace SftpInfo.UI.ViewModels
{
   public class ViewModelLocator
   {
      private readonly IContainer _iocContainer;

      public ViewModelLocator()
      {
         _iocContainer = BootStrapper.GetIocContainer();
      }

      public MainWindowViewModel MainWindowViewModel => _iocContainer.Resolve<MainWindowViewModel>();
   }
}
