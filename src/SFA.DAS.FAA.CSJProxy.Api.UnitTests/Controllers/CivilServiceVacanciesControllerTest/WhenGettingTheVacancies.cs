using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using SFA.DAS.FAA.CSJProxy.Api.Controllers;
using SFA.DAS.FAA.CSJProxy.Application.Queries.CivilServiceJobs;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.FAA.CSJProxy.Api.UnitTests.Controllers.CivilServiceVacanciesControllerTest;
[TestFixture]
internal class WhenGettingTheVacancies
{
    [Test, RecursiveMoqAutoData]
    public async Task Get_ReturnsOk_WhenVacanciesExists(
        GetCivilServiceJobsQuery query,
        GetCivilServiceJobsQueryResult mediatrResult,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CivilServiceVacanciesController controller,
        CancellationToken token)
    {
        // Arrange
        mediator.Setup(p => p.Send(query, token)).ReturnsAsync(mediatrResult);

        // Act
        var result = await controller.Get(token);

        // Assert
        result.Should().BeOfType<Ok<GetCivilServiceJobsQueryResult>>();
        var okResult = result as Ok<GetCivilServiceJobsQueryResult>;
        okResult!.Value.Should().BeEquivalentTo(mediatrResult);
    }
   
    [Test, RecursiveMoqAutoData]
    public async Task Get_ReturnsInternalServerException_WhenException_Thrown(Guid id,
        GetCivilServiceJobsQuery query,
        GetCivilServiceJobsQueryResult mediatrResult,
        [Frozen] Mock<IMediator> mediator,
        [Greedy] CivilServiceVacanciesController controller,
        CancellationToken token)
    {
        // Arrange
        mediator.Setup(p => p.Send(query, token)).ThrowsAsync(new Exception());

        // Act
        var result = await controller.Get(token);

        // Assert
        result.Should().BeOfType<ProblemHttpResult>();
    }
}
