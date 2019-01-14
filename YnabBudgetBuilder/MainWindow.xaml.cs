using System.Threading;
using System.Windows;
using YnabBudgetBuilder.Models;

namespace YnabBudgetBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppViewModel m_Model;

        public MainWindow()
        {
            m_Model = new AppViewModel();
            DataContext = m_Model;

            InitializeComponent();
        }
    }
}
