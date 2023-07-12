using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnigmaShopApi.Entities;

[Table(name: "t_purchase")]
public class Purchase
{
    [Key, Column(name: "id")] public Guid Id { get; set; }

    [Column(name: "trans_date")] public DateTime TransDate { get; set; }

    [Column(name: "customer_id")] public Guid CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }// -> penanda relasi
    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }// -> penanda relasi
}