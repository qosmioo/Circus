using Circus.Core.Repositories;
using Circus.Dto.Http;
using Circus.Server.Controllers.Converters;
using Microsoft.AspNetCore.Mvc;

namespace Circus.Server.Controllers;

[ApiController]
[Route("api/hall")]
public class HallsController : ControllerBase
{
    private readonly IHallRepository _hallRepository;

    private readonly ILogger<HallsController> _logger;

    private readonly ISectorRepository _sectorRepository;

    private readonly ISessionRepository _sessionRepository;

    public HallsController(IHallRepository hallRepository, ILogger<HallsController> logger, ISectorRepository sectorRepository, ISessionRepository sessionRepository)
    {
        _hallRepository = hallRepository;
        _logger = logger;
        _sectorRepository = sectorRepository;
        _sessionRepository = sessionRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetHallsAsync()
    {
        try
        {
            var halls = await _hallRepository.GetHallsAsync();
            var dtoHalls = halls.Select(HallConverter.ConvertHallToDto).ToList();

            return Ok(dtoHalls);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(GetHallsAsync));

            return StatusCode(500);
        }
    }
    
    [HttpGet]
    [Route("{hallId:guid}")]
    public async Task<IActionResult> GetHallAsync([FromRoute] Guid hallId)
    {
        try
        {
            var hall = await _hallRepository.FindHallAsync(hallId);

            if (hall == null)
                return NotFound();

            return Ok(HallConverter.ConvertHallToDto(hall));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(GetHallAsync));

            return StatusCode(500);
        }
    }

    [HttpDelete]
    [Route("{hallId:guid}")]
    public async Task<IActionResult> DeleteHallAsync([FromRoute] Guid hallId)
    {
        try
        {
            await _hallRepository.RemoveHallAsync(hallId);

            return Ok();
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(DeleteHallAsync));

            return NotFound();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(DeleteHallAsync));

            return StatusCode(500);
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> PutHallAsync([FromBody] Hall hall)
    {
        try
        {
            if (await _hallRepository.ExistAsync(hall.Id))
            {
                await _hallRepository.UpdateHallAsync(hall.Id, hall.Name);
                
                foreach (var session in hall.Sessions)
                {
                    await _sessionRepository.UpdateSessionAsync(session.Id, 
                        session.ShowId, 
                        session.HallId, 
                        session.StartsAt);
                }

                foreach (var sector in hall.Sectors)
                {
                    await _sectorRepository.UpdateSectorAsync(sector.Id, 
                        sector.HallId, 
                        sector.Name);
                }
            }
            else
            {
                await _hallRepository.AddHallAsync(hall.Id, hall.Name);
                
                foreach (var session in hall.Sessions)
                {
                    await _sessionRepository.AddSessionAsync(session.Id, 
                        session.ShowId, 
                        session.HallId, 
                        session.StartsAt);
                }
                
                foreach (var sector in hall.Sectors)
                {
                    await _sectorRepository.AddSectorAsync(sector.Id, 
                        sector.HallId, 
                        sector.Name);
                }
            }
            
            return Ok();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(PutHallAsync));

            return StatusCode(500);
        }
    }
}