namespace HydraWarehouse.Api.Models;

public class StockItem
{
    public Guid Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}