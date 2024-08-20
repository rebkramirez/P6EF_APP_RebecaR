using P6EF_APP_RebecaR.Views;

namespace P6EF_APP_RebecaR
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new BienvenidaPage());
        }
    }
}
