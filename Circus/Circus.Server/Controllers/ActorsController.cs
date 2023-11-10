using Circus.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Circus.Core.Models;
using Circus.Server.Controllers.Converters;
using Microsoft.AspNetCore.Http.HttpResults;
using Actor = Circus.Dto.Http.Actor;
using ActorShow = Circus.Dto.Http.ActorShow;

namespace Circus.Server.Controllers;

[ApiController]
[Route("api/actor")]
public class ActorsController : ControllerBase
{
    private readonly IActorRepository _actorRepository;
    private readonly ILogger<ActorsController> _logger;

    public ActorsController(IActorRepository actorRepository, ILogger<ActorsController> logger, IActorShowRepository actorShowRepository)
    {
        _actorRepository = actorRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetActorsAsync()
    {
        try
        {
            var actors = await _actorRepository.GetActorsAsync();
            var dtoActors = actors.Select(ActorConverter.ConvertActorToDto).ToList();

            return Ok(dtoActors);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(GetActorsAsync));

            return StatusCode(500);
        }
    }
}