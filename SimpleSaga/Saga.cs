namespace SimpleSaga
{
    public class Saga
    {
        public Guid SagaId { get; set; }
        public IList<IActivity> Activities { get; set; }
        public SagaStatus Status { get; private set; }
        public int CurrentActivity { get; set; }
        public int LastActivity { get; set; }

        //protected SagaContext Context { get; set; }

        public Saga()
        {
            SagaId = new Guid();
            Status = SagaStatus.NotStarted;
            Activities = new List<IActivity>();
        }

        public IList<IActivity> GetActivities()
        {
            return Activities;
        }

        private async Task<ActivityStatus> ExecuteActivities(CancellationToken cancellationToken)
        {
            ActivityStatus activityStatus = ActivityStatus.Failed;
            IActivity activity;

            for (CurrentActivity = 0; CurrentActivity < Activities.Count; ++CurrentActivity)
            {
                LastActivity =  CurrentActivity;

                activity = Activities[CurrentActivity];

                try
                {
                    activityStatus = await activity.ExecuteAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error executing activity: {activity.Name}");
                    activityStatus = ActivityStatus.Failed;
                }

                if (cancellationToken.IsCancellationRequested || activityStatus == ActivityStatus.Failed)
                {
                    break;
                }
            }

            return activityStatus;
        }

        private async Task<ActivityStatus> CompensatingActivities()
        {
            ActivityStatus activityStatus = ActivityStatus.Succeeded;
            IActivity activity;

            CurrentActivity--;

            Status = SagaStatus.Failed;

            for (; CurrentActivity >= 0; --CurrentActivity)
            {
                activity = Activities[CurrentActivity];
                try
                {
                    if(await activity.CompensateAsync() != ActivityStatus.Succeeded)
                    {
                        activityStatus = ActivityStatus.Failed;
                    }
                }
                catch
                {
                    activityStatus = ActivityStatus.Failed;
                }
            }

            return activityStatus;
        }

        public async Task<SagaStatus> Run(CancellationToken cancellationToken)
        {
            if(Activities.Count == 0)
            {
                return Status;
            }

            Status = SagaStatus.Running;
            ActivityStatus activityStatus;

            activityStatus = await ExecuteActivities(cancellationToken);

            if(activityStatus == ActivityStatus.Succeeded)
            {
                Status = SagaStatus.Succeeded;
                return Status;
            }

            if(CurrentActivity == 0)
            {
                Status = SagaStatus.Failed;
                return Status;
            }

            activityStatus = await CompensatingActivities();

            if(activityStatus == ActivityStatus.Failed)
            {
                Status = SagaStatus.UnexpectedError;
            }

            return Status;
        }
    }
}
