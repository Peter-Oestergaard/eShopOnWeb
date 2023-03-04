using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Specification;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.WishlistServiceTests;
public class DeleteItemFromWishlist
{
    private readonly string _buyerId = "Test buyerId";
    private readonly Mock<IRepository<Wishlist>> _mockWishlistRepo = new();
    private readonly Mock<IAppLogger<WishlistService>> _mockLogger = new();

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task WhenRemovingItemFromWishlistService_ItemIsRemoved(int catalogItemId)
    {
        // Arrange
        //var mockWishlist = new Mock<Wishlist>(_buyerId);
        //var wishlist = mockWishlist.Object;
        var wishlist = new Wishlist(_buyerId);
        wishlist.AddItem(catalogItemId);
        Assert.Single(wishlist.Items);
        _mockWishlistRepo.Setup(x => x.FirstOrDefaultAsync(It.IsAny<WishlistWithItemsSpecification>(), default)).ReturnsAsync(wishlist);
        var wishlistService = new WishlistService(_mockWishlistRepo.Object, _mockLogger.Object);

        // Act
        await wishlistService.RemoveItemFromWishlist(wishlist.BuyerId, catalogItemId);

        // Assert
        Assert.Empty(wishlist.Items);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task WhenRemovingNonExistingItemFromWishlistService_ExceptionIsThrown(int catalogItemId)
    {
        // Arrange
        var mockWishlist = new Mock<Wishlist>(_buyerId);
        var wishlist = mockWishlist.Object;
        Assert.Empty(wishlist.Items);
        _mockWishlistRepo.Setup(x => x.FirstOrDefaultAsync(It.IsAny<WishlistWithItemsSpecification>(), default)).ReturnsAsync(wishlist);
        var wishlistService = new WishlistService(_mockWishlistRepo.Object, _mockLogger.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => wishlistService.RemoveItemFromWishlist(wishlist.BuyerId, catalogItemId));
    }
}
