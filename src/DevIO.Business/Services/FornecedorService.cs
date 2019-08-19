using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace DevIO.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        public async Task Adicionar(Fornecedor fornecedor)
        {
            //// Validar o estado da entidade;
            //var validator = new FornecedorValidation();
            //var result = validator.Validate(fornecedor);

            //if (!result.IsValid)
            //{
            //    // result.Errors;
            //}
            // validar se não existe fornecedor com o mesmo documento;


            // Validar o estado da entidade;
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)
                && !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return;
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;
        }

        public async Task Remover(Fornecedor fornecedor)
        {
            throw new NotImplementedException();
        }
    }
}
