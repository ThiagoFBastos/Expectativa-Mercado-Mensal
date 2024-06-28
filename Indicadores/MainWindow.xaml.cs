using System.Windows;
using Indicadores.ViewModels;
using Indicadores.Services;

namespace Indicadores
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel(new ExpectativaMercadoMensalService());
        }
    }
}