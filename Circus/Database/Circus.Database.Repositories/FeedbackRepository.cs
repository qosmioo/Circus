using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Circus.Core.Repositories;
using Circus.Database.Context;
using Circus.Database.Models;
using Circus.Database.Repositories.Converters;
using Microsoft.EntityFrameworkCore;
using CoreFeedback = Circus.Core.Models.Feedback;

namespace Circus.Database.Repositories;

public class FeedbackRepository : IFeedBackRepository
{
    private readonly CircusContext _dbContext;

    public FeedbackRepository(CircusContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddFeedbackAsync(Guid id, 
        string text, 
        Guid showId, 
        Guid userId, 
        int rating, 
        DateTimeOffset createdAt)
    {
        await _dbContext.Feedbacks.AddAsync(new Feedback(id, text, showId, userId, createdAt, rating));

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateFeedbackAsync(Guid id, string text, Guid showId, Guid userId, int rating)
    {
        var feedback = await _dbContext.Feedbacks.FindAsync(id);

        if (feedback == null)
            throw new InvalidOperationException($"Feedback with id: {id} not found");
        
        feedback.Text = text;
        feedback.ShowId = showId;
        feedback.UserId = userId;
        feedback.Rating = rating;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<CoreFeedback>> GetFeedbacksAsync(Guid showId)
    {
        var feedbacks = await _dbContext.Feedbacks
            .AsNoTracking()
            .Where(f => f.ShowId == showId)
            .ToListAsync();

        return feedbacks.Select(FeedbackConverter.ConvertFeedbackToCore).ToList()!;
    }

    public async Task<CoreFeedback> RemoveFeedbackAsync(Guid feedBackId)
    {
        var feedback = await _dbContext.Feedbacks.FirstOrDefaultAsync(f => f.Id == feedBackId);

        if (feedback == null)
            throw new InvalidOperationException($"Feedback with id: {feedBackId} not found");

        _dbContext.Feedbacks.Remove(feedback);
        await _dbContext.SaveChangesAsync();

        return FeedbackConverter.ConvertFeedbackToCore(feedback)!;
    }

    public Task<bool> ExistsAsync(Guid feedbackId)
    {
        return _dbContext.Feedbacks.AnyAsync(f => f.Id == feedbackId);
    }
}