using System;
using System.Windows.Input;

namespace SftpInfo.UI.Commands
{
   public class RelayCommand : ICommand
   {

      private readonly Predicate<object> _canExecute;

      private readonly Action<object> _execute;

      public RelayCommand(Action<object> execute) : this(execute, null)
      {
      }

      public RelayCommand(Action<object> execute, Predicate<object> canExecute)
      {
         if (execute == null)
         {
            throw new ArgumentNullException(nameof(execute));
         }

         _execute = execute;
         _canExecute = canExecute;
      }

      public bool CanExecute(object parameter)
      {
         return _canExecute?.Invoke(parameter) ?? true;
      }

      public void Execute(object parameter)
      {
         _execute(parameter);
      }

      public event EventHandler CanExecuteChanged;
   }
}
