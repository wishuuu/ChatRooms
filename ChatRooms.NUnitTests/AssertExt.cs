using Newtonsoft.Json;

namespace TestProject1;

public static class AssertExt
{
    public static void AreEqualByJson(object expected, object actual)
    {
        var expectedJson = JsonConvert.SerializeObject(expected);
        var actualJson = JsonConvert.SerializeObject(actual);
        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }
}