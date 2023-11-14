using Circus.Core.Repositories;
using Circus.Dto.Http;
using Circus.Server.Controllers.Converters;
using Microsoft.AspNetCore.Mvc;

namespace Circus.Server.Controllers;

[ApiController]
[Route("api/seat")]
public class SeatsController : ControllerBase
{
    private readonly ISeatRepository _seatRepository;

    private readonly ILogger<SeatsController> _logger;

    private readonly ITicketRepository _ticketRepository;

    public SeatsController(ISeatRepository seatRepository, ILogger<SeatsController> logger, 
        ITicketRepository ticketRepository)
    {
        _seatRepository = seatRepository;
        _logger = logger;
        _ticketRepository = ticketRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetSeatsAsync()
    {
        try
        {
            var seats = await _seatRepository.GetSeatsAsync();
            var dtoSeats = seats.Select(SeatConverter.ConvertSeatToDto).ToList();

            return Ok(dtoSeats);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(GetSeatsAsync));

            return StatusCode(500);
        }
    }
    
    [HttpGet]
    [Route("{seatId:guid}")]
    public async Task<IActionResult> GetSeatAsync([FromRoute] Guid seatId)
    {
        try
        {
            var seat = await _seatRepository.FindSeatAsync(seatId);

            if (seat == null)
                return NotFound();

            return Ok(SeatConverter.ConvertSeatToDto(seat));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(GetSeatAsync));

            return StatusCode(500);
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> PutSeatAsync([FromBody] Seat seat)
    {
        try
        {
            if (await _seatRepository.ExistAsync(seat.Id))
            {
                await _seatRepository.UpdateSeatAsync(seat.Id, seat.RowId, seat.SeatNumber);
                
                foreach (var ticket in seat.Tickets)
                {
                    await _ticketRepository.UpdateTicketsAsync(seat.Id, 
                        ticket.SeatId,
                        ticket.SessionId,
                        ticket.UserId, 
                        ticket.Price,
                        ticket.IsAvailable);
                }
            }
            else
            {
                await _seatRepository.AddSeatAsync(seat.Id, seat.RowId, seat.SeatNumber);
                
                foreach (var ticket in seat.Tickets)
                {
                    await _ticketRepository.AddTicketsAsync(seat.Id, 
                        ticket.SeatId,
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
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(PutSeatAsync));

            return StatusCode(500);
        }
    }
    
    [HttpDelete]
    [Route("{seatId:guid}")]
    public async Task<IActionResult> DeleteSeatAsync([FromRoute] Guid seatId)
    {
        try
        {
            await _seatRepository.RemoveSeatAsync(seatId);

            return Ok();
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(DeleteSeatAsync));

            return NotFound();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(DeleteSeatAsync));

            return StatusCode(500);
        }
    }
}