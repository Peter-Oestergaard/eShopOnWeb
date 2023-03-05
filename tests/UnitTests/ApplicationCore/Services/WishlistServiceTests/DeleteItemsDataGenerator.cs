using System.Collections;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.WishlistServiceTests;

public class DeleteItemsDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> data = new List<object[]>
    {
        new object[] {
            new List<int> { 1, 2, 3 },
            new List<int> { 1 },
            new List<int> { 2, 3 } 
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
