using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities;
public class Wishlist : IAggregateRoot
{
    public string BuyerId { get; }

    public Wishlist(string buyerId) { }

    public void AddItem(int catalogItemId) { }
}
