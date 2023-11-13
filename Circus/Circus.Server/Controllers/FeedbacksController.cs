using Circus.Core.Repositories;
using Circus.Dto.Http;
using Circus.Server.Controllers.Converters;
using Microsoft.AspNetCore.Mvc;

namespace Circus.Server.Controllers;

[ApiController]
[Route("api/show/{showId:guid}")]
public class FeedbacksController : ControllerBase
{
    private readonly IFeedBackRepository _feedBackRepository;

    private readonly Logger<FeedbacksController> _logger;

    public FeedbacksController(IFeedBackRepository feedBackRepository, Logger<FeedbacksController> logger)
    {
        _feedBackRepository = feedBackRepository;
        _logger = logger;
    }

    [HttpGet]
    [Route("feedbacks")]
    public async Task<IActionResult> GetFeedbacksAsync([FromRoute] Guid showId)
    {
        try
        {
            var feedbacks = await _feedBackRepository.GetFeedbacksAsync(showId);
            var dtoFeedbacks = feedbacks.Select(FeedbackConverter.ConvertFeedbackToDto).ToList();

            return Ok(dtoFeedbacks);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}", nameof(GetFeedbacksAsync));

            return StatusCode(500);
        }
    }

    [HttpDelete]
    [Route("feedback/{feedbackId:guid}")]
    public async Task<IActionResult> DeleteFeedbackAsync([FromRoute] Guid feedbackId, Guid showId)
    {
        try
        {
            await _feedBackRepository.RemoveFeedbackAsync(feedbackId);

            return Ok();
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}",
                nameof(DeleteFeedbackAsync));

            return NotFound();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}",
                nameof(DeleteFeedbackAsync));

            return StatusCode(500);
        }
    }

    [HttpPut]
    [Route("feedbacks")]
    public async Task<IActionResult> PutFeedbackAsync([FromRoute] Guid showId, 
        [FromBody] Feedback feedback)
    {
        try
        {
            if (await _feedBackRepository.ExistsAsync(feedback.Id))
            {
                await _feedBackRepository.UpdateFeedbackAsync(feedback.Id, 
                    feedback.Text, 
                    showId,
                    feedback.UserId, 
                    feedback.Rating);
            }
            else
            {
                await _feedBackRepository.AddFeedbackAsync(feedback.Id, 
                    feedback.Text, 
                    showId,
                    feedback.UserId, 
                    feedback.Rating, 
                    feedback.CreatedAt);
            }

            return Ok();
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogError(exception, "Error in method: {MethodName}",
                nameof(PutFeedbackAsync));

            return StatusCode(500);
        }
    }
}