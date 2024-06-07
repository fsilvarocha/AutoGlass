using AutoGlass.Core.Domain.Entities;

namespace AutoGlass.Core.Application.DTOs;

public readonly record struct ProdutoRequest
{
    public string Descricao { get; init; }
    public bool Ativo { get; init; }
    public DateTime FabricadoEm { get; init; }
    public DateTime Validade { get; init; }
    public int IdFornecedor { get; init; }
    public string DescricaoFornecedor { get; init; }
    public string CNPJFornecedor { get; init; }

    public static implicit operator Produto(ProdutoRequest request)
    {
        return new()
        {
            Descricao = request.Descricao,
            Ativo = request.Ativo,
            FabricadoEm = request.FabricadoEm,
            Validade = request.Validade,
            IdFornecedor = request.IdFornecedor,
            DescricaoFornecedor = request.DescricaoFornecedor,
            CNPJFornecedor = request.CNPJFornecedor
        };

    }
}
