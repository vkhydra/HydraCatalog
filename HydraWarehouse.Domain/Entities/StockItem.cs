namespace HydraWarehouse.Domain.Entities;

public class StockItem
{
    public Guid Id { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    
    public StockItem(int productId, int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentException("Initial stock quantity cannot be negative.");
        }

        Id = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
    }
    
    private void ValidatePositiveQuantity(int quantity, string paramName )
    {
        if (quantity <= 0)
        {
            throw new ArgumentException($"{paramName} must be greater than zero.");
        }
    }
    
    public void AddQuantity(int quantityToAdd)
    {
        ValidatePositiveQuantity(quantityToAdd, nameof(quantityToAdd));
        Quantity += quantityToAdd;
    }
    
    public void RemoveQuantity(int quantityToRemove)
    {
        ValidatePositiveQuantity(quantityToRemove,  nameof(quantityToRemove));
        
        if (quantityToRemove > Quantity)
        {
            throw new InvalidOperationException("Not enough stock available to remove.");
        }
        Quantity -= quantityToRemove;
    }
}