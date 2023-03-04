using System;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Services;
public class WishlistService
{
    public WishlistService(IRepository<Wishlist> wishlistRepository,
        IAppLogger<WishlistService> logger)
    {

    }

    public Task AddItemToWishlist(string username, int catalogItemId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveItemFromWishlist(string username, int catalogItemId)
    {
        throw new NotImplementedException();
    }
}
