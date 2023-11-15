using Circus.Core.Repositories;
using Circus.Database.Repositories;
using Circus.Dto.Http;
using Circus.Server.Controllers.Converters;
using Microsoft.AspNetCore.Mvc;

namespace Circus.Server.Controllers;

[ApiController]
[Route("api/ticket")]
public class TicketsController : ControllerBase
{
    private readonly ITicketRepository _ticketRepository;

    private readonly ILogger<TicketRepository> _logger;

    public TicketsController(ITicketRepository ticketRepository, ILogger<TicketRepository> logger)
    {
        _ticketRepository = ticketRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetTicketsAsync()
    {
        try
        {
            var tickets = await _ticketRepository.GetTicketsAsync();
            var dtoTickets = tickets.Select(TicketConverter.ConvertTicketToDto).ToList();

            return Ok(dtoTickets);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(GetTicketsAsync));

            return StatusCode(500);
        }
    }
    
    [HttpGet]
    [Route("{ticketId:guid}")]
    public async Task<IActionResult> GetTicketAsync([FromRoute] Guid ticketId)
    {
        try
        {
            var ticket = await _ticketRepository.FindTicketAsync(ticketId);

            if (ticket == null)
                return NotFound();

            return Ok(TicketConverter.ConvertTicketToDto(ticket));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(GetTicketAsync));

            return StatusCode(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> PutTicketAsync([FromBody] Ticket ticket)
    {
        if (await _ticketRepository.ExistAsync(ticket.Id))
        {
            await _ticketRepository.UpdateTicketsAsync(ticket.Id, 
                ticket.SeatId, 
                ticket.SessionId, 
                ticket.UserId,
                ticket.Price, 
                ticket.IsAvailable);
        }
        else
        {
            await _ticketRepository.AddTicketsAsync(ticket.Id, 
                ticket.SeatId, 
                ticket.SessionId, 
                ticket.UserId,
                ticket.Price, 
                ticket.IsAvailable);
        }

        return Ok(ticket);
    }
    
    [HttpDelete]
    [Route("{ticketId:guid}")]
    public async Task<IActionResult> DeleteTicketAsync([FromRoute] Guid ticketId)
    {
        try
        {
            await _ticketRepository.RemoveTicketAsync(ticketId);

            return Ok();
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(DeleteTicketAsync));

            return NotFound();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(DeleteTicketAsync));

            return StatusCode(500);
        }
    }
}