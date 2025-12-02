using FluentAssertions;
using SFA.DAS.FAA.CSJProxy.Domain.Requests;

namespace SFA.DAS.FAA.CSJProxy.Api.UnitTests.Domain.Requests;
[TestFixture]
internal class WhenBuildingGetCivilServiceJobsApiRequest
{
    [Test]
    public void Then_The_Request_Is_Built_Correctly()
    {
        //Act
        var request = new GetCivilServiceJobsApiRequest();
        //Assert
        request.GetUrl.Should().Be("/csj/v1/jobs?contractType=Apprenticeship");
    }
}