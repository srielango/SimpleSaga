namespace SimpleSaga
{
    public interface IActivity
    {
        string Name { get; }
        Task<ActivityStatus> ExecuteAsync();
        Task<ActivityStatus> CompensateAsync();
    }
}
