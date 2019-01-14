using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;

namespace YnabBudgetBuilder.Models
{
    public class AsyncCommand : IAsyncCommand
    {
        public event EventHandler CanExecuteChanged;

        private bool m_IsExecuting;
        private readonly Func<Task> m_Execute;
        private readonly Func<bool> m_CanExecute;

        public AsyncCommand(
            [NotNull] Func<Task> execute,
            [CanBeNull] Func<bool> canExecute = null)
        {
            m_Execute = execute;
            m_CanExecute = canExecute;
        }

        public bool CanExecute()
        {
            return !m_IsExecuting && (m_CanExecute?.Invoke() ?? true);
        }

        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    m_IsExecuting = true;
                    await m_Execute();
                }
                finally
                {
                    m_IsExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit implementations
        bool ICommand.CanExecute([NotNull] object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute([NotNull] object parameter)
        {
            ExecuteAsync();
        }
        #endregion
    }
}
