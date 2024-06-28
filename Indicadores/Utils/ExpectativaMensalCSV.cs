using System.IO;
using Indicadores.Models;

namespace Indicadores.Utils
{
    public class ExpectativaMensalCSV
    {
        public static void ToCSV(string filename, IEnumerable<ExpectativaMercadoMensal> expectativas)
        {
            var ObjectRepr = (object? param, string separator) => param == null ? "\"\"" + separator : "\"" + param.ToString() + "\"" + separator;

            using (StreamWriter sr = new StreamWriter(filename))
            {
                sr.WriteLine("Indicador,Data,DataReferencia,Media,Mediana,DesvioPadrao,Minimo,Maximo,numeroRespondentes,baseCalculo");
                
                foreach(var expectativa in expectativas)
                {
                    sr.Write(ObjectRepr(expectativa.Indicador, ","));
                    sr.Write(ObjectRepr(expectativa.Data, ","));
                    sr.Write(ObjectRepr(expectativa.DataReferencia, ","));
                    sr.Write(ObjectRepr(expectativa.Media, ","));
                    sr.Write(ObjectRepr(expectativa.Mediana, ","));
                    sr.Write(ObjectRepr(expectativa.DesvioPadrao, ","));
                    sr.Write(ObjectRepr(expectativa.Minimo, ","));
                    sr.Write(ObjectRepr(expectativa.Maximo, ","));
                    sr.Write(ObjectRepr(expectativa.numeroRespondentes, ","));
                    sr.Write(ObjectRepr(expectativa.baseCalculo, sr.NewLine));
                }
            }
        }
    }
}
