using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace YnabBudgetBuilder.Models.NUnit
{
    [TestFixture]
    internal sealed class AppViewModelFixture
    {
        private AppViewModel m_Model;

        [SetUp]
        public async Task SetUp()
        {
            m_Model = new AppViewModel();
            await m_Model.Initialize();
        }
        
        [Test]
        public void TestInitialize()
        {
            Assert.That(m_Model.Budgets, Is.Not.Null);
            Assert.That(m_Model.SelectedBudget, Is.EqualTo(m_Model.Budgets.First()));
            Assert.That(m_Model.InitializeCommand.CanExecute(null), Is.False);
        }
    }
}
