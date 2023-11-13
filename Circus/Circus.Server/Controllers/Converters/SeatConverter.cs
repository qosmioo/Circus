using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class SeatConverter
{
    public static Seat? ConvertSeatToDto(Core.Models.Seat? seat)
    {
        if (seat == null)
            return null;

        var dtoTickets = seat.Tickets.Select(TicketConverter.ConvertTicketToDto).ToList();

        return new Seat(seat.Id, seat.RowId, seat.SeatNumber, dtoTickets);
    }
}