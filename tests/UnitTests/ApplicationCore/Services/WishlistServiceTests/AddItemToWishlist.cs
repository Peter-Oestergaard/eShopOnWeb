using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Specification;
using Moq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.WishlistServiceTests;

public class AddItemToWishlist
{
    private readonly string _buyerId = "Test buyerId";
    private readonly Mock<IRepository<Wishlist>> _mockWishlistRepo = new();
    private readonly Mock<IAppLogger<WishlistService>> _mockLogger = new();

    [Fact]
    public async Task InvokesWishlistRepositoryGetBySpecAsyncOnce()
    {
        // Arrange
        var wishlist = new Wishlist(_buyerId);
        _mockWishlistRepo.Setup(x => x.FirstOrDefaultAsync(It.IsAny<WishlistWithItemsSpecification>(), default)).ReturnsAsync(wishlist);
        var wishlistService = new WishlistService(_mockWishlistRepo.Object, _mockLogger.Object);

        // Act
        await wishlistService.AddItemToWishlist(wishlist.BuyerId, 1);

        // Assert
        _mockWishlistRepo.Verify(x => x.FirstOrDefaultAsync(It.IsAny<WishlistWithItemsSpecification>(), default), Times.Once);
    }

}
