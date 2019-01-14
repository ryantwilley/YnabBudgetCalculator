using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using YNAB.Rest;

namespace YnabBudgetBuilder.Models
{
    internal sealed class AppViewModel : BaseNotifyPropertyChanged
    {
        private const string ApiToken = "a88d403f76c8bf76314c385f10a0e9c9506a13f751bfda1f1f1830d4bc84351f";

        private readonly IApiClient m_Client;
        private Budget m_SelectedBudget;
        private IEnumerable<Budget> m_Budgets;
        private bool m_IsInitialized;

        public AppViewModel()
        {
            m_Client = ApiClientFactory.Create(ApiToken);
            SelectedBudget = new Budget
            {
                Name = "Default"
            };

            Budgets = new List<Budget>
            {
                SelectedBudget
            };

            InitializeCommand = new AsyncCommand(Initialize, () => !m_IsInitialized);
        }

        [NotNull]
        public ICommand InitializeCommand { get; }

        public async Task Initialize()
        {
            ApiResponse<BudgetsData> budgetsData = await m_Client.GetBudgets();

            Budgets = budgetsData.Data.Budgets;
            if (budgetsData.Data.Budgets.Count > 0)
            {
                SelectedBudget = budgetsData.Data.Budgets[0];
            }

            m_IsInitialized = true;
        }

        [ItemNotNull]
        [NotNull]
        public IEnumerable<Budget> Budgets
        { 
            get => m_Budgets;
            private set => SetProperty(ref m_Budgets, value);
        }

        [NotNull]
        public Budget SelectedBudget
        {
            get => m_SelectedBudget;

            set => SetProperty(ref m_SelectedBudget, value);
        }
    }
}
