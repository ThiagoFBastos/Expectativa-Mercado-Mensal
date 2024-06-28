using Indicadores.Models;
using System.Net.Http;
using System.Text.Json;
using Indicadores.Utils;

namespace Indicadores.Services
{
    public class ExpectativaMercadoMensalService
    {
        private const string urlEndpoint = "https://olinda.bcb.gov.br/olinda/servico/Expectativas/versao/v1/odata/ExpectativaMercadoMensais";
        public async Task<List<ExpectativaMercadoMensal>> FilterExpectativas(string indicador, string dataInicial, string dataFinal, int top = -1, int skip = -1)
        {
            using (HttpClient client = new HttpClient())
            {
                string filter = String.Format("Indicador eq '{0}' and Data ge '{1}' and Data le '{2}'", indicador, dataInicial, dataFinal);
                string uri = String.Format("{0}?$format=json&$filter={1}", urlEndpoint, filter);

                if (top != -1)
                    uri += String.Format("&$top={0}", top);

                if (skip != -1)
                    uri += String.Format("&$skip={0}", skip);

                string result = await client.GetStringAsync(uri);

                JsonExpectativaMercadoMensal? json = JsonSerializer.Deserialize<JsonExpectativaMercadoMensal>(result);

                if (json == null || json?.value == null)
                    throw new FormatException("Não foi recebido um objeto json válido");

                return json?.value;
            }
        }
    }
}
