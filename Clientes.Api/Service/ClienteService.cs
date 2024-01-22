using Clientes.Api.Domain.Dto;
using Clientes.Api.Domain.Enum;
using Clientes.Api.Domain.Filters;
using Clientes.Api.Domain.Model;
using Clientes.Api.Infrastructure;
using System.Text.Json;

namespace Clientes.Api.Service
{
    public class ClienteService(AppDbContext appDbContext) : IClienteService
    {
        public readonly AppDbContext _appDbContext = appDbContext;

		public CreateClienteDto BuscarPorId(int id)
		{
            var model = _appDbContext.Clientes.FirstOrDefault(x => x.Id == id);
			return model == null ? throw new Exception("Usuario Não Encontrado") : (CreateClienteDto)model;
		}

		public List<Cliente> ListarClientes(ClienteFiltros clienteFiltros) {
            var query = _appDbContext.Clientes.AsQueryable();
            if(!string.IsNullOrEmpty(clienteFiltros.NomeRazao))
            {
                query = query.Where(c => c.NomeRazao.Contains(clienteFiltros.NomeRazao));
            }

            if (!string.IsNullOrEmpty(clienteFiltros.Email))
            {
                query = query.Where(c => c.Email.Contains(clienteFiltros.Email));
            }

            if (!string.IsNullOrEmpty(clienteFiltros.Telefone))
            {
                query = query.Where(c => c.Telefone.Contains(clienteFiltros.Telefone));
            }

            if (clienteFiltros.Bloqueado != null)
            {
                query = query.Where(c => c.Bloqueado == clienteFiltros.Bloqueado);
            }

            if (clienteFiltros.DataCadastroInicio != null)
            {
                query = query.Where(c => c.DataCadastro >= clienteFiltros.DataCadastroInicio);
            }

            if (clienteFiltros.DataCadastroFim != null)
            {
                query = query.Where(c => c.DataCadastro <= clienteFiltros.DataCadastroFim);
            }
            return [.. query];
        }

        public bool ExisteEmail(string? email, int? id)
        {
			if (email == null)
            {
                return false;
            }

			return _appDbContext.Clientes
                .FirstOrDefault(c => c.Email.ToLower() == email.ToLower() 
                    && c.Id != id) != null;
        }

        public bool ExisteCpfCnpj(string? cpfCnpj, int? id)
        {
            if (cpfCnpj == null)
            {
                return false;
            }

			return _appDbContext.Clientes
                .FirstOrDefault(c => c.CpfCnpj == cpfCnpj 
                    && c.Id != id) != null;
        }

        public bool ExisteInscricaoEstadual(string? inscricaoEstadual, int? id)
        {
            if (inscricaoEstadual == null)
            {
                return false;
            }

			return _appDbContext.Clientes
                .FirstOrDefault(c => c.InscricaoEstadual != null 
                    && c.InscricaoEstadual == inscricaoEstadual 
                    && c.Id != id) != null;
        }

		private void ValidadeDadosDuplicados(Cliente clienteModel, string etapa, int? id = null)
		{
			var erros = new List<string>();
			if (ExisteEmail(clienteModel.Email, id))
			{
				erros.Add("Este e-mail já está cadastrado para outro Cliente");
			}

			if (ExisteCpfCnpj(clienteModel.CpfCnpj, id))
			{
				erros.Add("Este CPF/CNPJ já está cadastrado para outro Cliente");
			}

			if (!clienteModel.Isento &&
				(clienteModel.InscricaoEstadualPessoaFisica || clienteModel.TipoPessoa == TipoPessoaEnum.Juridica)
				&& ExisteInscricaoEstadual(clienteModel.InscricaoEstadual, id))
			{
				erros.Add("Esta Inscrição Estadual já está cadastrada para outro Cliente");
			}

			if (erros.Count > 0)
			{
				var stringErro = $"Erro{etapa};" + JsonSerializer.Serialize(erros);
				throw new Exception(stringErro);
			}
		}

		public void CadastraCliente(CreateClienteDto clienteDto)
		{
			var clienteModel = (Cliente)clienteDto;
			ValidadeDadosDuplicados(clienteModel, "Cadastro");

			_appDbContext.Clientes.Add(clienteModel);
			_appDbContext.SaveChanges();
		}

		public void EditarCliente(CreateClienteDto Cliente, int ClienteId)
		{
            var clienteNew = (Cliente)Cliente;
			ValidadeDadosDuplicados(clienteNew, "Edicao", ClienteId);

            var clienteOld = _appDbContext.Clientes.FirstOrDefault(x => x.Id == ClienteId) 
                ?? throw new Exception("Usuario Não Encontrado");

			clienteOld.AtualizarDadosCliente(clienteNew);
			_appDbContext.Update(clienteOld);
            _appDbContext.SaveChanges();
		}
	}
}
