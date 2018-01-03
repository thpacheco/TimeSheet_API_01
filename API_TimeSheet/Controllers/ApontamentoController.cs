using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_TimeSheet.DAO.RepositoryDAO;
using API_TimeSheet.Models;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

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
        [Route("apontamentos/consultar-apontamento/{idUsuario}/{dataMarcacao}")]
        public HttpResponseMessage BuscarApontamentoDoDia(string idUsuario, DateTime dataMarcacao)
        {
            try
            {

                idUsuario = "5a4bcf4f7a0052364c68617f";
                var apontamento = apontamentoRepository
                    .Consultar(c => c.IdUsuario == idUsuario);

                string DataMarcacao = "03/01/2018";

                var apontamentoComData = apontamentoRepository
                    .Consultar(c => c.DataMarcacao == DataMarcacao);

                if (apontamentoComData == null) throw new Exception("Cliente não encontrado");
                return Request.CreateResponse(HttpStatusCode.OK, apontamento);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
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
