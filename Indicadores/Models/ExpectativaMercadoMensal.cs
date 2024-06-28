using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Indicadores.Models
{
    public class ExpectativaMercadoMensal: INotifyPropertyChanged
    {
        private string? _indicador;
        private string? _data;
        private string? _dataReferencia;
        private double? _media;
        private double? _mediana;
        private double? _desvioPadrao;
        private double? _minimo;
        private double? _maximo;
        private int? _numeroRespondentes;
        private int? _baseCalculo;

        public string? Indicador { get => _indicador; set { _indicador = value; OnPropertyChanged();} }
        public string? Data { get => _data; set { _data = value; OnPropertyChanged(); } }
        public string? DataReferencia { get => _dataReferencia; set { _dataReferencia = value; OnPropertyChanged(); } }
        public double? Media { get => _media; set { _media = value; OnPropertyChanged(); } }
        public double? Mediana { get => _mediana; set { _mediana = value; OnPropertyChanged(); } }
        public double? DesvioPadrao { get => _desvioPadrao; set { _desvioPadrao = value; OnPropertyChanged(); } }
        public double? Minimo { get => _minimo; set { _minimo = value; OnPropertyChanged(); } }
        public double? Maximo { get => _maximo; set { _maximo = value; OnPropertyChanged(); } }
        public int? numeroRespondentes { get => _numeroRespondentes; set { _numeroRespondentes = value; OnPropertyChanged(); } }
        public int? baseCalculo { get => _baseCalculo; set { _baseCalculo = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
