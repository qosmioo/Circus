using Circus.Dto.Http;

namespace Circus.Server.Controllers.Converters;

public static class SeatConverter
{
    public static Seat? ConvertSeatToDto(Core.Models.Seat? coreSeat)
    {
        if (coreSeat == null)
            return null;

        var dtoTickets = coreSeat.Tickets.Select(TicketConverter.ConvertTicketToDto).ToList();

        return new Seat(coreSeat.Id, coreSeat.RowId, coreSeat.SeatNumber, dtoTickets);
    }

    public static Core.Models.Seat? ConvertSeatToCore(Seat? dtoSeat)
    {
        if (dtoSeat == null)
            return null;
        
        var coreTickets = dtoSeat.Tickets.Select(TicketConverter.ConvertTicketToCore).ToList();

        return new Core.Models.Seat(dtoSeat.Id, dtoSeat.RowId, dtoSeat.SeatNumber, coreTickets!);
    }
}