namespace BaseSource.Core.Application.IntegrationTests;

public class SampleTest
{
    [Fact]
    public void Test1()
    {
        //  Arrange
        int a = 10, b = 20, c = 0;
        //  Act
        c = a + b;
        //  Assert
        Assert.Equal(30, c);
    }
}
