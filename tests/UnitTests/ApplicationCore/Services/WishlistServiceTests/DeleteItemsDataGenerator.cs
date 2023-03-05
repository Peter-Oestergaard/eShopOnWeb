using System.Collections;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.WishlistServiceTests;

public class DeleteItemsDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> data = new List<object[]>
    {
        new object[] {
            new List<int> { 1, 2, 3 }, // Initial items in list
            new List<int> { 1 },       // Items to remove
            new List<int> { 2, 3 }     // Remaining items
        },
        new object[] {
            new List<int> { 2, 3 },
            new List<int> { 3 },
            new List<int> { 2 }
        },
        new object[] {
            new List<int> { 1, 3, 5, 7, 9 },
            new List<int> { 3, 7 },
            new List<int> { 1, 5, 9 }
        }
    };

    public IEnumerator<object[]> GetEnumerator()
    {
        return data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
