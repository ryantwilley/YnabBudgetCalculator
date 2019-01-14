using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;

namespace YnabBudgetBuilder.Models
{
    public interface IAsyncCommand : ICommand
    {
        [NotNull]
        Task ExecuteAsync();

        bool CanExecute();
    }
}