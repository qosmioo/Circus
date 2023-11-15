using Circus.Core.Repositories;
using Circus.Dto.Http;
using Circus.Server.Controllers.Converters;
using Microsoft.AspNetCore.Mvc;

namespace Circus.Server.Controllers;

[ApiController]
[Route("api/session")]
public class SessionsController : ControllerBase
{
    private readonly ISessionRepository _sessionRepository;

    private readonly ILogger<SessionsController> _logger;

    private readonly ITicketRepository _ticketRepository;

    public SessionsController(ISessionRepository sessionRepository, ILogger<SessionsController> logger, ITicketRepository ticketRepository)
    {
        _sessionRepository = sessionRepository;
        _logger = logger;
        _ticketRepository = ticketRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetSessionsAsync()
    {
        try
        {
            var sessions = await _sessionRepository.GetSessionsAsync();
            var dtoSessions = sessions.Select(SessionConverter.ConvertSessionToDto).ToList();

            return Ok(dtoSessions);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(GetSessionsAsync));

            return StatusCode(500);
        }
    }
    
    [HttpGet]
    [Route("{sessionId:guid}")]
    public async Task<IActionResult> GetSessionAsync([FromRoute] Guid sessionId)
    {
        try
        {
            var session = await _sessionRepository.FindSessionAsync(sessionId);

            if (session == null)
                return NotFound();

            return Ok(SessionConverter.ConvertSessionToDto(session));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(GetSessionAsync));

            return StatusCode(500);
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> PutSessionAsync([FromBody] Session session)
    {
        try
        {
            if (await _sessionRepository.ExistAsync(session.Id))
            {
                await _sessionRepository.UpdateSessionAsync(session.Id, 
                    session.ShowId, 
                    session.HallId, 
                    session.StartsAt);
                
                foreach (var ticket in session.Tickets)
                {
                    await _ticketRepository.UpdateTicketsAsync(session.Id, 
                        ticket.SessionId,
                        ticket.SessionId,
                        ticket.UserId, 
                        ticket.Price,
                        ticket.IsAvailable);
                }
            }
            else
            {
                await _sessionRepository.AddSessionAsync(session.Id, 
                    session.ShowId, 
                    session.HallId, 
                    session.StartsAt);
                
                foreach (var ticket in session.Tickets)
                {
                    await _ticketRepository.AddTicketsAsync(session.Id, 
                        ticket.SessionId,
                        ticket.SessionId,
                        ticket.UserId, 
                        ticket.Price,
                        ticket.IsAvailable);
                }
            }
            
            return Ok();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(PutSessionAsync));

            return StatusCode(500);
        }
    }
    
    [HttpDelete]
    [Route("{sessionId:guid}")]
    public async Task<IActionResult> DeleteSessionAsync([FromRoute] Guid sessionId)
    {
        try
        {
            await _sessionRepository.RemoveSessionAsync(sessionId);

            return Ok();
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(DeleteSessionAsync));

            return NotFound();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(DeleteSessionAsync));

            return StatusCode(500);
        }
    }
}