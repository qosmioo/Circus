using Circus.Core.Repositories;
using Circus.Database.Repositories;
using Circus.Dto.Http;
using Circus.Server.Controllers.Converters;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Circus.Server.Controllers;

[ApiController]
[Route("api/row")]
public class RowsController : ControllerBase
{
    private readonly IRowRepository _rowRepository;

    private readonly ILogger<RowRepository> _logger;

    private readonly ISeatRepository _seatRepository;

    public RowsController(IRowRepository rowRepository, ILogger<RowRepository> logger, ISeatRepository seatRepository)
    {
        _rowRepository = rowRepository;
        _logger = logger;
        _seatRepository = seatRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetRowsAsync()
    {
        try
        {
            var rows = await _rowRepository.GetRowsAsync();
            var dtoRows = rows.Select(RowConverter.ConvertRowToDto).ToList();

            return Ok(dtoRows);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(GetRowsAsync));

            return StatusCode(500);
        }
    }
    
    [HttpGet]
    [Route("{rowId:guid}")]
    public async Task<IActionResult> GetRowAsync([FromRoute] Guid rowId)
    {
        try
        {
            var row = await _rowRepository.FindRowAsync(rowId);

            if (row == null)
                return NotFound();

            return Ok(RowConverter.ConvertRowToDto(row));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(GetRowAsync));

            return StatusCode(500);
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> PutRowAsync([FromBody] Row row)
    {
        try
        {
            if (await _rowRepository.ExistAsync(row.Id))
            {
                await _rowRepository.UpdateRowAsync(row.Id, row.SectorId, row.RowNumber);
                
                foreach (var seat in row.Seats)
                {
                    await _seatRepository.UpdateSeatAsync(row.Id, seat.RowId, seat.SeatNumber);
                }
            }
            else
            {
                await _rowRepository.AddRowAsync(row.Id, row.SectorId, row.RowNumber);
                
                foreach (var seat in row.Seats)
                {
                    await _seatRepository.AddSeatAsync(row.Id, seat.RowId, seat.SeatNumber );
                }
            }
            
            return Ok();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(PutRowAsync));

            return StatusCode(500);
        }
    }
    
    [HttpDelete]
    [Route("{rowId:guid}")]
    public async Task<IActionResult> DeleteRowAsync([FromRoute] Guid rowId)
    {
        try
        {
            await _rowRepository.RemoveRowAsync(rowId);

            return Ok();
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(DeleteRowAsync));

            return NotFound();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(DeleteRowAsync));

            return StatusCode(500);
        }
    }
}