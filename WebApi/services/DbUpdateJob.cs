using Quartz;
using WebApi.Controllers.DataController;

/// <summary>
/// Represents the job updating the database.
/// </summary>
public class DbUpdateJob : IJob
{
    private readonly DataController _dataController;

    public DbUpdateJob(DataController dataController)
    {
        _dataController = dataController;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _dataController.UpdateAllAsync();
    }
}