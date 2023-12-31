﻿using Circus.Core.Repositories;
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

    private readonly IActorShowRepository _actorShowRepository;
    
    private readonly ILogger<ActorsController> _logger;

    public ActorsController(IActorRepository actorRepository, ILogger<ActorsController> logger, IActorShowRepository actorShowRepository)
    {
        _actorRepository = actorRepository;
        _logger = logger;
        _actorShowRepository = actorShowRepository;
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
    
    [HttpGet]
    [Route("{actorId:guid}")]
    public async Task<IActionResult> GetActorAsync([FromRoute] Guid actorId)
    {
        try
        {
            var actor = await _actorRepository.FindActorAsync(actorId);

            if (actor == null)
                return NotFound();

            return Ok(ActorConverter.ConvertActorToDto(actor));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(GetActorAsync));

            return StatusCode(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> PutActorAsync([FromBody] Actor actor)
    {
        try
        {
            if (await _actorRepository.ExistAsync(actor.Id))
            {
                await _actorRepository.UpdateActorAsync(actor.Id, actor.Name, 
                    actor.Description, actor.AvatarId);
                
                foreach (var actorShow in actor.ActorShows)
                {
                    await _actorShowRepository.UpdateActorShowAsync(actor.Id, 
                        actorShow.ShowId,
                        actorShow.ActorId,
                        actorShow.Role);
                }
            }
            else
            {
                await _actorRepository.AddActorAsync(actor.Id, actor.Name, 
                    actor.Description, actor.AvatarId);
                
                foreach (var actorShow in actor.ActorShows)
                {
                    await _actorShowRepository.AddActorShowAsync(Guid.NewGuid(), 
                        actorShow.ShowId,
                        actorShow.ActorId,
                        actorShow.Role);
                }
            }
            
            return Ok();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(PutActorAsync));

            return StatusCode(500);
        }
    }

    [HttpDelete]
    [Route("{actorId:guid}")]
    public async Task<IActionResult> DeleteActorAsync([FromRoute] Guid actorId)
    {
        try
        {
            await _actorRepository.RemoveActorAsync(actorId);

            return Ok();
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(DeleteActorAsync));

            return NotFound();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(DeleteActorAsync));

            return StatusCode(500);
        }
    }
}