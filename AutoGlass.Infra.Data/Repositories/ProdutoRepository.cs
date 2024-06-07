using AutoGlass.Core.Domain.Entities;
using AutoGlass.Core.Domain.Interfaces;
using AutoGlass.Infra.Data.DataContext;
using Microsoft.EntityFrameworkCore;

namespace AutoGlass.Infra.Data.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ApplicationDbContext _context;
    public ProdutoRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(Produto product)
    {

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync().ConfigureAwait(false);

        }
        catch (DbUpdateException)
        {

            await transaction.RollbackAsync();
        }
    }

    public async Task<Produto> DeleteAsync(int productId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var inactivate = await _context.Produtos
                .AsTracking()
                .FirstOrDefaultAsync(n => n.Id.Equals(productId));

            if (inactivate is null)
                return Produto.NULL;

            inactivate.InactivateProduct();
            await _context.SaveChangesAsync();

            await transaction.CommitAsync().ConfigureAwait(false);

            return inactivate;
        }
        catch (DbUpdateException)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async IAsyncEnumerable<Produto> GetAllAsync(int skip = 0, int take = 25)
    {

        var products = _context.Produtos
            .Where(n => !n.Inativo)
            .Skip(skip)
            .Take(take)
            .Select(item => new Produto
            {
                Id = item.Id,
                Descricao = item.Descricao,
                Ativo = item.Ativo,
                FabricadoEm = item.FabricadoEm,
                Validade = item.Validade,
                IdFornecedor = item.IdFornecedor,
                DescricaoFornecedor = item.DescricaoFornecedor,
                CNPJFornecedor = item.CNPJFornecedor

            }).AsAsyncEnumerable();

        await foreach (var item in products)
        {
            yield return item;
        }
    }

    public async Task<Produto> GetId(int productId)
    {
        var existing = await _context.Produtos
            .AsTracking()
            .SingleOrDefaultAsync(n => n.Id.Equals(productId));

        if (existing is null)
            return Produto.NULL;

        return existing;
    }

    public async Task<Produto> UpdateAsync(int productId, Produto product)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var existing = await _context.Produtos
            .AsTracking()
            .SingleOrDefaultAsync(n => n.Id.Equals(productId));

            if (existing is null)
                return Produto.NULL;

            existing.SetProduct(productId,
                                  product.Descricao,
                                  product.FabricadoEm,
                                  product.Validade,
                                  product.IdFornecedor,
                                  product.DescricaoFornecedor,
                                  product.CNPJFornecedor);

            _context.Update(existing);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync().ConfigureAwait(false);

            return existing;
        }
        catch (DbUpdateException)
        {
            await transaction.RollbackAsync();
            throw;
        }


    }
}
