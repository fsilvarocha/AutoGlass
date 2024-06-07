using AutoGlass.Core.Domain.Entities;

namespace AutoGlass.Core.Application.DTOs;

public readonly record struct ProdutoResponse
{
    public int Id { get; init; }
    public string Descricao { get; init; }
    public bool Ativo { get; init; }
    public DateTime FabricadoEm { get; init; }
    public DateTime Validade { get; init; }
    public int IdFornecedor { get; init; }
    public string DescricaoFornecedor { get; init; }
    public string CNPJFornecedor { get; init; }

    public static implicit operator ProdutoResponse(Produto produto)
    {
        return new()
        {
            Id = produto.Id,
            Descricao = produto.Descricao,
            Ativo = produto.Ativo,
            FabricadoEm = produto.FabricadoEm,
            Validade = produto.Validade,
            IdFornecedor = produto.IdFornecedor,
            DescricaoFornecedor = produto.DescricaoFornecedor,
            CNPJFornecedor = produto.CNPJFornecedor
        };

    }
}
