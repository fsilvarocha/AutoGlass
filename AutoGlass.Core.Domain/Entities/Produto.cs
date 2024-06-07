using AutoGlass.Core.Domain.Interfaces;
using AutoGlass.Core.Domain.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoGlass.Core.Domain.Entities;

public class Produto : IValidation, IInterface
{
    private List<Notification.Notification> _notifications;

    public int Id { get; set; }
    public string? Descricao { get; set; }
    public bool Ativo { get; set; } = true;
    public bool Inativo { get; set; }
    public DateTime FabricadoEm { get; set; }
    public DateTime Validade { get; set; }
    public int IdFornecedor { get; set; }
    public string? DescricaoFornecedor { get; set; }
    public string? CNPJFornecedor { get; set; }

    [NotMapped]
    public IReadOnlyCollection<Notification.Notification> Notifications => _notifications;


    protected void SetNotifications(List<Notification.Notification> notifications)
    {
        _notifications = notifications;
    }
    public void InactivateProduct() => Inativo = true;
    public void SetProduct(int id, string? description,
                             DateTime manufacturing,
                             DateTime validate,
                             int supplierCode,
                             string? supplierDescription,
                             string? supplierCNPJ)
    {
        Descricao = description;
        FabricadoEm = manufacturing;
        Validade = validate;
        IdFornecedor = supplierCode;
        DescricaoFornecedor = supplierDescription;
        CNPJFornecedor = supplierCNPJ;
    }

    public bool IsValid()
    {
        var contracts = new ContractValidations<Produto>()
            .ValidManufactureIsOk(FabricadoEm, Validade, "Invalid manufacturing date", nameof(FabricadoEm));

        return contracts.IsValid();
    }

    public static readonly Produto NULL = new NullProduct();
    private class NullProduct : Produto { }
}
