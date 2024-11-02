using System.Numerics;

namespace FredrikHr.DotnetGeneralPurpose.Extlib.TUnitTest;

public class MyTestClass
{
    [Test]
    public async Task MyTest()
    {
        var result = Add(1, 2);

        await Assert.That(result).IsEqualTo(3);

        static T Add<T>(T a, T b) where T : IAdditionOperators<T, T, T>
        {
            return a + b;
        }
    }
}
