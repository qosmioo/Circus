using Circus.Core.Repositories;
using Circus.Database.Repositories;
using Circus.Dto.Http;
using Circus.Server.Controllers.Converters;
using Microsoft.AspNetCore.Mvc;

namespace Circus.Server.Controllers;

[ApiController]
[Route("api/show")]
public class ShowsController : ControllerBase
{
    private readonly IShowRepository _showRepository;

    private readonly ILogger<ShowRepository> _logger;

    private readonly IActorShowRepository _actorShowRepository;

    private readonly ISessionRepository _sessionRepository;

    private readonly IFeedBackRepository _feedBackRepository;

    public ShowsController(IShowRepository showRepository, ILogger<ShowRepository> logger, IActorShowRepository actorShowRepository, ISessionRepository sessionRepository, IFeedBackRepository feedBackRepository)
    {
        _showRepository = showRepository;
        _logger = logger;
        _actorShowRepository = actorShowRepository;
        _sessionRepository = sessionRepository;
        _feedBackRepository = feedBackRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetShowsAsync()
    {
        try
        {
            var shows = await _showRepository.GetShowsAsync();
            var dtoActors = shows.Select(ShowConverter.ConvertShowToDto).ToList();

            return Ok(dtoActors);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(GetShowsAsync));

            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("{showId:guid}")]
    public async Task<IActionResult> GetShowAsync([FromRoute] Guid showId)
    {
        try
        {
            var show = await _showRepository.FindShowAsync(showId);

            if (show == null)
                return NotFound();

            return Ok(ShowConverter.ConvertShowToDto(show));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(GetShowAsync));

            return StatusCode(500);
        }
    }

    [HttpDelete]
    [Route("{showId:guid}")]
    public async Task<IActionResult> DeleteShowAsync([FromRoute] Guid showId)
    {
        try
        {
            await _showRepository.RemoveShowAsync(showId);

            return Ok();
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}",
                nameof(DeleteShowAsync));

            return NotFound();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}",
                nameof(DeleteShowAsync));

            return StatusCode(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> PutShowAsync([FromBody] Show show)
    {
        try
        {
            if (await _showRepository.ExistAsync(show.Id))
            {
                await _showRepository.UpdateShowAsync(show.Id, show.Name, show.Description,
                    show.Duration, show.PosterId);

                foreach (var feedback in show.Feedbacks)
                {
                    await _feedBackRepository.UpdateFeedbackAsync(feedback.Id,
                        feedback.Text,
                        feedback.ShowId,
                        feedback.UserId,
                        feedback.Rating);
                }

                foreach (var session in show.Sessions)
                {
                    await _sessionRepository.UpdateSessionAsync(session.ShowId,
                        session.ShowId,
                        session.HallId,
                        session.StartsAt);
                }

                foreach (var actorShow in show.ActorShows)
                {
                    await _actorShowRepository.UpdateActorShowAsync(actorShow.Id,
                        actorShow.ShowId,
                        actorShow.ActorId,
                        actorShow.Role);
                }
            }
            else
            {
                await _showRepository.AddShowAsync(show.Id, show.Name, show.Description,
                    show.Duration, show.PosterId);

                foreach (var feedback in show.Feedbacks)
                {
                    await _feedBackRepository.AddFeedbackAsync(feedback.Id,
                        feedback.Text,
                        feedback.ShowId,
                        feedback.UserId,
                        feedback.Rating,
                        feedback.CreatedAt);
                }

                foreach (var session in show.Sessions)
                {
                    await _sessionRepository.AddSessionAsync(session.ShowId,
                        session.ShowId,
                        session.HallId,
                        session.StartsAt);
                }

                foreach (var actorShow in show.ActorShows)
                {
                    await _actorShowRepository.AddActorShowAsync(actorShow.Id,
                        actorShow.ShowId,
                        actorShow.ActorId,
                        actorShow.Role);
                }
            }

            return Ok();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}",
                nameof(PutShowAsync));

            return StatusCode(500);
        }
    }
}