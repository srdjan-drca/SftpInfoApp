using SftpInfo.UI.ViewModels;
using SimpleIoc;

namespace SftpInfo.UI.Configuration
{
   public static class BootStrapper
   {
      private static SimpleIocContainer _iocContainer;

      public static void InitializeIocContainer()
      {
         _iocContainer = new SimpleIocContainer();

         _iocContainer.Register<MainWindowViewModel, MainWindowViewModel>();
      }

      public static IContainer GetIocContainer()
      {
         return _iocContainer;
      }
   }
}
