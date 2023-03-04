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

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task WhenAddingItemToWishlistService_ItemIsAdded(int catalogItemId)
    {
        // Arrange
        var mockWishlist = new Mock<Wishlist>(_buyerId);
        var wishlist = mockWishlist.Object;
        _mockWishlistRepo.Setup(x => x.FirstOrDefaultAsync(It.IsAny<WishlistWithItemsSpecification>(), default)).ReturnsAsync(wishlist);
        var wishlistService = new WishlistService(_mockWishlistRepo.Object, _mockLogger.Object);

        // Act
        await wishlistService.AddItemToWishlist(wishlist.BuyerId, catalogItemId);

        // Assert
        mockWishlist.Verify(x => x.AddItem(catalogItemId), Times.Once);
    }

    public static IEnumerable<object[]> GetCatalogItemIds()
    {
        yield return new object[] { new List<int> { 1 } };
        yield return new object[] { new List<int> { 2, 3 } };
        yield return new object[] { new List<int> { 1, 3, 4 } };
    }

    [Theory]
    [MemberData(nameof(GetCatalogItemIds))]
    public async Task WhenAddingItemsToWishlistService_CorrectNumberOfItemsAreAdded(List<int> catalogItemIds)
    {
        // Arrange
        var wishlist = new Wishlist(_buyerId);
        _mockWishlistRepo.Setup(x => x.FirstOrDefaultAsync(It.IsAny<WishlistWithItemsSpecification>(), default)).ReturnsAsync(wishlist);
        var wishlistService = new WishlistService(_mockWishlistRepo.Object, _mockLogger.Object);

        // Act
        foreach (var item in catalogItemIds)
        {
            await wishlistService.AddItemToWishlist(wishlist.BuyerId, item);
        }

        // Assert
        Assert.Equal(catalogItemIds.Count, wishlist.Items.Count);
    }

}
