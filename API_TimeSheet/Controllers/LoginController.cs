using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_TimeSheet.DAO.RepositoryDAO;
using API_TimeSheet.Models;
using MongoDB.Bson;

namespace API_TimeSheet.Controllers
{
    [RoutePrefix("api")]
    public class LoginController : ApiController
    {

        private readonly Repository<Usuario> _repositoryUsuario = new Repository<Usuario>("Usuario");

        [HttpGet]
        [Route("usuarios")]
        public HttpResponseMessage ListarUsuarios()
        {
            try
            {
                var usuarios = _repositoryUsuario.Listar();

                if (usuarios == null) throw new Exception("Usuário não encontrado");

                return Request.CreateResponse(HttpStatusCode.OK, usuarios);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("usuario/{login}/{senha}")]
        public HttpResponseMessage BuscarApontamentoPorIdUsuario(string login, string senha)
        {
            try
            {
                var usuario = _repositoryUsuario.Consultar(c => c.Login == login && c.Senha == senha);

                if (usuario == null) throw new Exception("Usuario não encontrado");

                return Request.CreateResponse(HttpStatusCode.OK, usuario);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("usuario/cadastrar")]
        public HttpResponseMessage SalvarUsuario(Usuario usuario)
        {
            try
            {
                var ID = new ObjectId();
                usuario.Id = ID;

                _repositoryUsuario.Inserir(usuario);
                return Request.CreateResponse(HttpStatusCode.OK, "Usuário cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("usuario/atualizar")]
        public HttpResponseMessage AtualizarUsuario(Usuario usuario)
        {
            try
            {
                _repositoryUsuario.Atualizar(usuario);

                return Request.CreateResponse(HttpStatusCode.OK, "Usuário alterado com sucesso.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
