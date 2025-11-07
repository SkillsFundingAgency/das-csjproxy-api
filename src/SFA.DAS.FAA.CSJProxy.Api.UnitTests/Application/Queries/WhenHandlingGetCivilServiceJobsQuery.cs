using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SFA.DAS.FAA.CSJProxy.Application.Queries.CivilServiceJobs;
using SFA.DAS.FAA.CSJProxy.Domain.Interfaces;
using SFA.DAS.FAA.CSJProxy.Domain.Models;
using SFA.DAS.FAA.CSJProxy.Domain.Requests;
using SFA.DAS.FAA.CSJProxy.Domain.Responses;
using SFA.DAS.Testing.AutoFixture;
using System.Net;

namespace SFA.DAS.FAA.CSJProxy.Api.UnitTests.Application.Queries;

[TestFixture]
internal class WhenHandlingGetCivilServiceJobsQuery
{
    [Test, MoqAutoData]
    public async Task Then_The_Request_Is_Handled_Correctly(
        GetCivilServiceJobsQuery query,
        GetCivilServiceJobsApiResponse response,
        [Frozen] Mock<ICivilServiceApiService> apiClient,
        [Frozen] ILogger<GetCivilServiceJobsQueryHandler> logger,
        [Greedy] GetCivilServiceJobsQueryHandler handler,
        CancellationToken token)
    {
        var apiResponse = new ApiResponse<GetCivilServiceJobsApiResponse>(response, HttpStatusCode.OK, string.Empty);
        apiClient
            .Setup(x => x.GetCivilServiceApiResponse<GetCivilServiceJobsApiResponse>(It.IsAny<GetCivilServiceJobsApiRequest>(), token))
            .ReturnsAsync(apiResponse);

        // act
        var result = await handler.Handle(query, token);

        // assert
        result.Should().BeEquivalentTo(response, opt => opt.ExcludingMissingMembers());
    }

    [Test]
    [MoqInlineAutoData(HttpStatusCode.BadRequest)]
    [MoqInlineAutoData(HttpStatusCode.BadGateway)]
    [MoqInlineAutoData(HttpStatusCode.NotFound)]
    [MoqInlineAutoData(HttpStatusCode.NotAcceptable)]
    public async Task Handles_Invalid_Response_Code(
        HttpStatusCode statusCode,
        GetCivilServiceJobsQuery query,
        [Frozen] Mock<ICivilServiceApiService> apiClient,
        [Frozen] ILogger<GetCivilServiceJobsQueryHandler> logger,
        [Greedy] GetCivilServiceJobsQueryHandler handler,
        CancellationToken token)
    {
        // arrange
        var apiResponse = new ApiResponse<GetCivilServiceJobsApiResponse>(new GetCivilServiceJobsApiResponse(), statusCode, string.Empty);
        apiClient
           .Setup(x => x.GetCivilServiceApiResponse<GetCivilServiceJobsApiResponse>(It.IsAny<GetCivilServiceJobsApiRequest>(), token))
           .ReturnsAsync(apiResponse);

        // act
        var result = await handler.Handle(query, CancellationToken.None);

        // assert
        result.Jobs.Should().BeEmpty();
    }
}