using AutoGlass.Core.Application.Abstractions;
using AutoGlass.Core.Application.DTOs;
using AutoGlass.Core.Application.Results;
using AutoGlass.Core.Domain.Entities;
using AutoGlass.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoGlass.Core.Application.Services;

public sealed class ProductService : IProdutoService
{
    private readonly IProdutoRepository _repository;
    public ProductService(IProdutoRepository repository) => _repository = repository;

    public async Task<bool> Add(ProdutoRequest productRequest)
    {
        Result result;
        var entity = (Produto)productRequest;
        if (entity.IsValid())
        {
            try
            {
                entity.Ativo = true;
                await _repository.AddAsync(entity);
                result = new Result(201, $"{entity.Descricao} criado com sucesso", true, entity);
                return result.Success;
            }
            catch (DbUpdateException)
            {
                _ = new Result(400, $"Erro ao cadastrar produto", false, productRequest);
            }

        }
        result = new Result(400, $"Error Servidor", false, entity);
        result.SetNotifications(result.Notifications.ToList());
        return result.Success;


    }

    public async Task<ProdutoResponse> Delete(int productId)
    {
        var existing = await _repository.DeleteAsync(productId);

        return existing;

    }

    public async IAsyncEnumerable<ProdutoResponse> GetAll()
    {

        var getAll = _repository.GetAllAsync();

        await foreach (var item in getAll)
        {
            yield return item;
        }
    }

    public async Task<ProdutoResponse> GetId(int productId)
    {
        var getId = await _repository.GetId(productId);
        if (getId is null)
            return null;

        return (ProdutoResponse)getId;
    }


    public async Task<bool> Update(int productId, ProdutoRequest model)
    {
        Result result;
        var entity = (Produto)model;
        await _repository.GetId(productId);
        if (entity.IsValid())
        {
            try
            {
                entity.SetProduct(productId,
                                  model.Descricao,
                                  model.FabricadoEm,
                                  model.Validade,
                                  model.IdFornecedor,
                                  model.DescricaoFornecedor,
                                  model.CNPJFornecedor);

                await _repository.UpdateAsync(productId, entity);
                result = new Result(200, $"{entity.Id} atualizado com sucesso", true, entity);
                return result.Success;

            }
            catch (Exception)
            {
                _ = new Result(400, $"Erro ao atualizar produto", false, model);
            }
            result = new Result(400, $"Error Servidor", false, entity);
            result.SetNotifications(result.Notifications.ToList());
            return result.Success;
        }
        return false;

    }
}
