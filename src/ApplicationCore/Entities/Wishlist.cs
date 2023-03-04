using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities;

public class Wishlist : IAggregateRoot
{
    public List<WishlistItem> Items { get; }

    public string BuyerId { get; }

    public Wishlist(string buyerId) { }

    public void AddItem(int catalogItemId) { }
}
