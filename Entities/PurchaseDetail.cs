using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnigmaShopApi.Entities;

[Table(name: "t_purchase_detail")]
public class PurchaseDetail
{
    [Key, Column(name: "id")] public Guid Id { get; set; }
    [Column(name: "purchase_id")] public Guid PurchaseId { get; set; }
    [Column(name: "product_id")] public Guid ProductId { get; set; }
    [Column(name: "qty")] public int Qty { get; set; }

    public virtual Purchase Purchase { get; set; }
    public virtual Product Product { get; set; }
}