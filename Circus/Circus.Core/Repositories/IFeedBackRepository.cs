using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Circus.Core.Models;

namespace Circus.Core.Repositories;

public interface IFeedBackRepository
{
    Task AddFeedbackAsync(Guid id, 
        string text, 
        Guid showId, 
        Guid userId, 
        int rating, 
        DateTimeOffset createdAt);

    Task UpdateFeedbackAsync(Guid id,
        string text,
        Guid showId,
        Guid userId,
        int rating);

    Task<List<Feedback>> GetFeedbacksAsync(Guid showId);

    Task<Feedback> RemoveFeedbackAsync(Guid feedBackId);
    
    Task<bool> ExistsAsync(Guid feedbackId);
}