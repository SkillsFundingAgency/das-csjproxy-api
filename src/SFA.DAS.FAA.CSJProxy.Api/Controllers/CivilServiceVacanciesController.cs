using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using SFA.DAS.FAA.CSJProxy.Application.Queries.CivilServiceJobs;
using SFA.DAS.FAA.CSJProxy.Domain.Models.Response;

namespace SFA.DAS.FAA.CSJProxy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CivilServiceVacanciesController(IMediator mediator,
    ILogger<CivilServiceVacanciesController> logger) : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(List<Job>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        
        logger.LogInformation("Get Civil Service Vacancies invoked");

        try
        {
            var result = await mediator.Send(new GetCivilServiceJobsQuery(), cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred getting civil service vacancies");
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}