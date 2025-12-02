using AutoFixture.NUnit3;
using Moq;
using SFA.DAS.FAA.CSJProxy.Domain.Interfaces;
using SFA.DAS.FAA.CSJProxy.Domain.Models;
using SFA.DAS.FAA.CSJProxy.Domain.Requests;
using SFA.DAS.FAA.CSJProxy.Domain.Responses;
using SFA.DAS.FAA.CSJProxy.Domain.Services;
using SFA.DAS.Testing.AutoFixture;
using System.Net;
using FluentAssertions;

namespace SFA.DAS.FAA.CSJProxy.Api.UnitTests.Domain.Services.CivilServiceApiServiceTests;
[TestFixture]
internal class WhenGettingApiHealth
{
    [Test, MoqAutoData]
    public async Task Then_The_Health_Is_Returned_As_True_From_The_Api(GetCivilServiceJobsApiRequest request,
        GetCivilServiceJobsApiResponse response,
        [Frozen] Mock<IApiClient> apiClient,
        [Greedy] CivilServiceApiService service,
        CancellationToken token)
    {
        //Arrange
        var apiResponse = new ApiResponse<GetCivilServiceJobsApiResponse>(response, HttpStatusCode.OK, string.Empty);
        apiClient
            .Setup(x => x.GetWithResponseCodeAsync<GetCivilServiceJobsApiResponse>(It.IsAny<GetCivilServiceJobsApiRequest>(), token))!
            .ReturnsAsync(apiResponse);


        //Act
        var actualResponse = await service.IsApiHealthyAsync(token);
        //Assert
        actualResponse.Should().BeTrue();
    }

    [Test]
    [MoqInlineAutoData(HttpStatusCode.BadRequest)]
    [MoqInlineAutoData(HttpStatusCode.BadGateway)]
    [MoqInlineAutoData(HttpStatusCode.NotFound)]
    [MoqInlineAutoData(HttpStatusCode.NotAcceptable)]
    public async Task Then_The_Health_Is_Degraded_Returned_As_False_From_The_Api(
        HttpStatusCode statusCode,
        GetCivilServiceJobsApiRequest request,
        GetCivilServiceJobsApiResponse response,
        [Frozen] Mock<IApiClient> apiClient,
        [Greedy] CivilServiceApiService service,
        CancellationToken token)
    {
        //Arrange
        var apiResponse = new ApiResponse<GetCivilServiceJobsApiResponse>(response, statusCode, string.Empty);
        apiClient
            .Setup(x => x.GetWithResponseCodeAsync<GetCivilServiceJobsApiResponse>(It.IsAny<GetCivilServiceJobsApiRequest>(), token))!
            .ReturnsAsync(apiResponse);

        //Act
        var actualResponse = await service.IsApiHealthyAsync(token);
        //Assert
        actualResponse.Should().BeFalse();
    }
}
