using Circus.Core.Repositories;
using Circus.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Circus.Server.Controllers;

public class ShowsController : ControllerBase
{
    private readonly IShowRepository _showRepository;

    private readonly ILogger<ShowRepository> _logger;

    private readonly IActorShowRepository _actorShowRepository;

    private readonly ISessionRepository _sessionRepository;

    public ShowsController(IShowRepository showRepository, ILogger<ShowRepository> logger, IActorShowRepository actorShowRepository, ISessionRepository sessionRepository)
    {
        _showRepository = showRepository;
        _logger = logger;
        _actorShowRepository = actorShowRepository;
        _sessionRepository = sessionRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetShowsAsync()
    {
        try
        {
            var shows = _showRepository.GetShowsAsync();
            
        }
    }
}