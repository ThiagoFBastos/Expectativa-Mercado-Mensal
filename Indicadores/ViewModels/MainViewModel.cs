using Indicadores.Commands;
using Indicadores.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Indicadores.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Indicadores.Utils;

namespace Indicadores.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<ExpectativaMercadoMensal> _expectativas;

        private string? _indicador = "IPCA";

        private DateTime? _dataInicial = DateTime.MinValue;

        private DateTime? _dataFinal = DateTime.Now;
        public ObservableCollection<ExpectativaMercadoMensal> Expectativas { get => _expectativas; set { _expectativas = value; OnPropertyChanged(); } }
        public ICommand ClickFilterExpectativasCommand { get; }
        public ICommand PreviousFilterExpectativasCommand { get; }
        public ICommand NextFilterExpectativasCommand { get; }
        public ICommand ExportCSVCommand { get; }
        public string? Indicador { get => _indicador; set { _indicador = value; OnPropertyChanged(); } }
        public DateTime? DataInicial { get => _dataInicial; set { _dataInicial = value; OnPropertyChanged(); } }

        public DateTime? DataFinal { get => _dataFinal; set { _dataFinal = value; OnPropertyChanged(); } }

        public string PageIndex { get => _currentPageIndex.ToString(); set { _currentPageIndex = int.Parse(value); OnPropertyChanged(); } }

        private int _currentPageIndex = 1;

        public event PropertyChangedEventHandler PropertyChanged;

        private ExpectativaMercadoMensalService _service;

        public MainViewModel(ExpectativaMercadoMensalService service)
        {
            _service = service;
            ClickFilterExpectativasCommand = new AsyncCommand(ClickFilterExpectativas);
            PreviousFilterExpectativasCommand = new AsyncCommand(PreviousFilterExpectativas);
            NextFilterExpectativasCommand = new AsyncCommand(NextFilterExpectativas);
            ExportCSVCommand = new SyncCommand(ExportToCsv);
            Expectativas = new ObservableCollection<ExpectativaMercadoMensal>();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task FilterExpectativas(object? parameter)
        {
            try
            {
                const int LIMITE_POR_QUERY = 100;
                string? indicador = Indicador;
                string? dataInicial = DataInicial?.ToString("yyyy-MM-dd");
                string? dataFinal = DataFinal?.ToString("yyyy-MM-dd");
                int pageIndex = int.Parse(PageIndex);

                if (indicador == null) throw new Exception("Selecione um indicador");
                if (dataInicial == null) throw new Exception("Insira uma data inicial");
                if (dataFinal == null) throw new Exception("Insira uma data final");

                List<ExpectativaMercadoMensal> expectativas = await _service.FilterExpectativas(indicador, dataInicial, dataFinal, LIMITE_POR_QUERY, LIMITE_POR_QUERY * (pageIndex - 1));

                Expectativas.Clear();
                foreach (var expectativa in expectativas)
                    Expectativas.Add(expectativa);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ClickFilterExpectativas(object? parameter)
        {
            PageIndex = "1";
            await FilterExpectativas(parameter);
        }

        private async Task NextFilterExpectativas(object? parameter)
        {
            var expectativas = Expectativas;

            if (expectativas.Count > 0)
            {
                ++_currentPageIndex;
                PageIndex = _currentPageIndex.ToString();
                await FilterExpectativas(parameter);
            }
            else
            {
                MessageBox.Show("Nenhum resultado encontrado. Você chegou ao limite.", "Resultados", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task PreviousFilterExpectativas(object? parameter)
        {
            if (_currentPageIndex > 1)
            {
                --_currentPageIndex;
                PageIndex = _currentPageIndex.ToString();
                await FilterExpectativas(parameter);
            }
        }

        private void ExportToCsv(object? parameter)
        {
            try
            {
                string? indicador = Indicador;
                string? dataInicial = DataInicial?.ToString("ddMMyyyy");
                string? dataFinal = DataFinal?.ToString("ddMMyyyy");
                string pagina = PageIndex;
                var dialogo = new Microsoft.Win32.SaveFileDialog();

                if (indicador == null) throw new Exception("Selecione um indicador");
                if (dataInicial == null) throw new Exception("Insira uma data inicial");
                if (dataFinal == null) throw new Exception("Insira uma data final");

                dialogo.FileName = String.Format("{0}-{1}-{2}-{3}", indicador, dataInicial, dataFinal, pagina);
                dialogo.DefaultExt = ".csv";

                bool? resultado = dialogo.ShowDialog();

                if (resultado == true)
                    ExpectativaMensalCSV.ToCSV(dialogo.FileName, Expectativas);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
