using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.FAA.CSJProxy.Application.Queries.CivilServiceJobs;
using SFA.DAS.FAA.CSJProxy.Domain.Models.Response;
using System.Net;

namespace SFA.DAS.FAA.CSJProxy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CivilServiceVacanciesController(IMediator mediator,
    ILogger<CivilServiceVacanciesController> logger) : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(List<Job>), StatusCodes.Status200OK)]
    public async Task<IResult> Get(CancellationToken cancellationToken)
    {
        
        logger.LogInformation("Get Civil Service Vacancies invoked");

        try
        {
            var result = await mediator.Send(new GetCivilServiceJobsQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred getting civil service vacancies");
            return Results.Problem(statusCode: (int)HttpStatusCode.InternalServerError);
        }
    }
}