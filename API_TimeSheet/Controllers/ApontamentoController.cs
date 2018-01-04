using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using API_TimeSheet.DAO.RepositoryDAO;
using API_TimeSheet.Models;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using Extension;

namespace API_TimeSheet.Controllers
{
    [RoutePrefix("api")]
    public class ApontamentoController : ApiController
    {
        readonly Repository<Apontamento> apontamentoRepository = new Repository<Apontamento>("Apontamento");

        [HttpPost]
        [Route("cadastrar")]
        public HttpResponseMessage SalvarMarcacao(Apontamento apontamento)
        {
            try
            {
                ObjectId ID = new ObjectId();
                apontamento.Id = ID;

                apontamentoRepository.Inserir(apontamento);
                return Request.CreateResponse(HttpStatusCode.OK, "Marcação Efetuada com sucesso.");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("Atualizar")]
        public HttpResponseMessage AtualizarMarcacao(Apontamento apontamento)
        {
            try
            {
                var apontamentoConsulta = apontamentoRepository
                    .Consultar(c => c.IdUsuario == apontamento.IdUsuario && c.DataMarcacao == apontamento.DataMarcacao);

                if (apontamentoConsulta == null) throw new Exception("Erro ao atualizar Marcação");

                apontamento.Id = apontamentoConsulta.Id;

                apontamentoRepository.Atualizar(apontamento);

                return Request.CreateResponse(HttpStatusCode.OK, "Marcação alterada com sucesso.");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("apontamentos")]
        public HttpResponseMessage BuscarApontamentoPorIdUsuario()
        {
            try
            {
                var apontamento = apontamentoRepository.Listar();

                if (apontamento == null) throw new Exception("Cliente não encontrado");

                return Request.CreateResponse(HttpStatusCode.OK, apontamento);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("apontamento/{idUsuario}")]
        public HttpResponseMessage BuscarApontamentoPorIdUsuario(string IdUsuario)
        {
            try
            {

                var apontamento = apontamentoRepository.Listar(c => c.IdUsuario == IdUsuario);

                if (apontamento == null) throw new Exception("Cliente não encontrado");

                return Request.CreateResponse(HttpStatusCode.OK, apontamento);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("apontamento/consultar-apontamento/{idUsuario}/{dataMarcacao}")]
        public HttpResponseMessage BuscarApontamentoDoDia(string idUsuario, [FromUri]string dataMarcacao)
        {
            try
            {
                var apontamento = apontamentoRepository
                    .Consultar(c => c.IdUsuario == idUsuario && c.DataMarcacao == dataMarcacao.FormatarData());

                if (apontamento == null) throw new Exception("Cliente não encontrado");
                return Request.CreateResponse(HttpStatusCode.OK, apontamento);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Convert a UTC Date String of format yyyyMMddThhmmZ into a Local Date
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        private string BuildDateTimeFromYAFormat(string dateString)
        {
            Regex r = new Regex(@"^\d{4}\d{2}\d{2}T\d{2}\d{2}Z$");
            if (!r.IsMatch(dateString))
            {
                throw new FormatException(
                    string.Format("{0} is not the correct format. Should be yyyyMMddThhmmZ", dateString));
            }

            DateTime dt = DateTime.ParseExact(dateString, "yyyyMMddThhmmZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

            return dt.ToShortDateString();
        }

        [HttpDelete]
        [Route("Excluir/{id}")]
        public HttpResponseMessage DeletarMarcacao(ObjectId id)
        {
            try
            {
                var query = Query<Apontamento>.EQ(c => c.Id, id);

                apontamentoRepository.Excluir(query);

                return Request.CreateResponse(HttpStatusCode.OK, "Erro ao excluir registro.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
