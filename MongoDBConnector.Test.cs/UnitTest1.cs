using Xunit;   
using MongoDBConnector.cs; 
using System.Threading.Tasks; 
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Builders;


namespace MongoDBConnector.Test.cs;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var mongoDbContainer = new TestcontainersBuilder<TestcontainersContainer>()
            .WithImage("mongo:6.0") // official MongoDB image
                .WithCleanUp(true)
                .WithPortBinding(27017, true) // random free port
                .Build();

        await mongoDbContainer.StartAsync();

        var connectionString = $"mongodb://localhost:{mongoDbContainer.GetMappedPublicPort(27017)}";
        var connector = new MongoDBConnector(connectionString);

        // Act
        var result = connector.Ping();

        // Assert
        Assert.True(result);

        // Clean up
        await mongoDbContainer.StopAsync();
    }
}
