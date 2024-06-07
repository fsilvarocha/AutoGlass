using AutoGlass.Core.Domain.Entities;

namespace AutoGlass.Core.Domain.Interfaces;

public interface IProdutoRepository
{
    Task AddAsync(Produto produto);
    Task<Produto> UpdateAsync(int produtoId, Produto produto);
    Task<Produto> DeleteAsync(int produtoId);
    IAsyncEnumerable<Produto> GetAllAsync(int pular = 0, int obter = 25);
    Task<Produto> GetId(int produtoId);
}
