using AutoMapper;
using DevIO.App.ViewModels;
using DevIO.Business.Models;

namespace DevIO.App.AutoMapper
{
    // A classe Profile (do AutoMapper) vai dizer que essa classe é uma classe de configuração de um perfil de mapeamento;
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // O código abaixo transforma Fornecedor em FornecedorViewModel, mas não transformar FornecedorViewModel em Fornecedor,
            // é um caminho de mão única; Antigamente precisava criar dois perfis, "um indo e um voltando", mas não é mais necessário;
            // uma vez que transforme Fornecedor em FornecedorViewModel, desde que o processo de transformação seja o mesmo, ou seja, não haja
            // um construtor parametrizado em alguma das classes, é possível utilizar o .ReverseMap();
            // O ReverseMap significa que da mesma forma que ele vai fazer um, ele vai fazer o outro, porque não tem diferença;
            // CreateMap<Fornecedor, FornecedorViewModel>();

            // ReverseMap é para fazer o mapeamento nos dois sentidos;
            // Através deste mapeamento da para fazer agora de Fornecedor para FornecedorViewModel e de FornecedorViewModel para Fornecedor (fazendo um mapeamento reverso);
            // Se por exemplo na classe Fornecedor tivesse um construtor com parâmetro, o AutoMapper não sabe construir instâncias que tem construtor parametrizado,
            // teria então que fazer um mapeamento exclusivo para um sentido e um outro mapeamento para o outro sentido;
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }
    }
}
