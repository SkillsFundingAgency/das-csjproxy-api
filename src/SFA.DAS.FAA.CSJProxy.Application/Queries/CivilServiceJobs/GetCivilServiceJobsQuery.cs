using MediatR;

namespace SFA.DAS.FAA.CSJProxy.Application.Queries.CivilServiceJobs;

public sealed record GetCivilServiceJobsQuery : IRequest<GetCivilServiceJobsQueryResult>;