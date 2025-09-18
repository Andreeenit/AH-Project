public class Order
{
    public int OrderId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Status { get; set; } = "Active";
    public decimal TotalAmount { get; set; }
    public string ShippingAddy { get; set; } = "";
    public string? ItemNumber { get; set; }

}