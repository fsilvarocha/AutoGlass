using AutoGlass.Core.Application.DTOs;

namespace AutoGlass.Core.Application.Abstractions;

public interface IProdutoService
{
    Task<bool> Add(ProdutoRequest productRequest);
    Task<bool> Update(int productId, ProdutoRequest request);
    Task<ProdutoResponse> Delete(int productId);
    IAsyncEnumerable<ProdutoResponse> GetAll();
    Task<ProdutoResponse> GetId(int productId);
}
