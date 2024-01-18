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

        public bool ExisteEmail(string? email)
        {
            if (email == null)
            {
                return false;
            }
            var cliente = _appDbContext.Clientes.FirstOrDefault(c => c.Email.ToLower() == email.ToLower());
            return cliente != null;
        }

        public bool ExisteCpfCnpj(string? cpfCnpj)
        {
            if (cpfCnpj == null)
            {
                return false;
            }
            var cliente = _appDbContext.Clientes.FirstOrDefault(c => c.CpfCnpj == cpfCnpj);
            return cliente != null;
        }

        public bool ExisteInscricaoEstadual(string? inscricaoEstadual)
        {
            if (inscricaoEstadual == null)
            {
                return false;
            }
            var cliente = _appDbContext.Clientes.FirstOrDefault(c => c.InscricaoEstadual != null 
                && c.InscricaoEstadual == inscricaoEstadual);
            return cliente != null;
        }

        public void CadastraCliente(CreateClienteDto clienteDto)
        {
            var erros = new List<string>();
            var clienteModel = (Cliente)clienteDto;

            if (ExisteEmail(clienteModel.Email))
            {
                erros.Add("Este e-mail já está cadastrado para outro Cliente");
            }

            if (ExisteCpfCnpj(clienteModel.CpfCnpj))
            {
                erros.Add("Este CPF/CNPJ já está cadastrado para outro Cliente");
            }

            if (!clienteModel.Isento && 
                (clienteModel.InscricaoEstadualPessoaFisica || clienteModel.TipoPessoa == TipoPessoaEnum.Juridica) 
                && ExisteInscricaoEstadual(clienteModel.InscricaoEstadual))
            {
                erros.Add("Esta Inscrição Estadual já está cadastrada para outro Cliente");
            }

            if (erros.Count > 0)
            {
                var stringErro = "ErroCadastro;" + JsonSerializer.Serialize(erros);
                throw new Exception(stringErro);
            }

            _appDbContext.Clientes.Add(clienteModel);
            _appDbContext.SaveChanges();
        }
    }
}
