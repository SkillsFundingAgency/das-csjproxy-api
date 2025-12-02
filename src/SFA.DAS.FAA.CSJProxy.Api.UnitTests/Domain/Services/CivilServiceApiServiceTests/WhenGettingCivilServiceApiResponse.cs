using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using SFA.DAS.FAA.CSJProxy.Domain.Interfaces;
using SFA.DAS.FAA.CSJProxy.Domain.Models;
using SFA.DAS.FAA.CSJProxy.Domain.Requests;
using SFA.DAS.FAA.CSJProxy.Domain.Responses;
using SFA.DAS.FAA.CSJProxy.Domain.Services;
using SFA.DAS.Testing.AutoFixture;
using System.Net;

namespace SFA.DAS.FAA.CSJProxy.Api.UnitTests.Domain.Services.CivilServiceApiServiceTests;
[TestFixture]
internal class WhenGettingCivilServiceApiResponse
{
    [Test, MoqAutoData]
    public async Task Then_The_Response_Is_Returned_From_The_Civil_Service_Api_Client(
        GetCivilServiceJobsApiRequest request,
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
        var actualResponse = await service.GetCivilServiceApiResponse<GetCivilServiceJobsApiResponse>(request, token);
        //Assert
        actualResponse.Should().BeEquivalentTo(response, options => options.ExcludingMissingMembers());
    }
}