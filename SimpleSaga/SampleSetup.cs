namespace SimpleSaga
{
    public class InteractiveActivity
    {
        protected ActivityStatus InteractiveExecute(string activityName)
        {
            ActivityStatus status;

            Console.WriteLine($"\nActivity {activityName} executing...");
            Console.Write("Enter Return status (Y=Succeeded, N=Failed) - ");
            var reply = Console.ReadLine();
            status = GetStatus(reply);

            Console.WriteLine($"Activity {activityName} execution {status.ToString()}");

            return status;
        }

        protected ActivityStatus InteractiveCompensate(string activityName)
        {
            ActivityStatus status;

            Console.WriteLine($"\nActivity {activityName} compensating ...");
            Console.Write("Enter Return Status (Y=Succeeded, N=Failed)? ");

            var reply = Console.ReadLine();
            status = GetStatus(reply);

            return status;
        }

        protected async Task<ActivityStatus> ExecuteAsync(string name)
        {
            var task = Task.Run<ActivityStatus>(() =>
            {
                return InteractiveExecute(name);
            });

            return await task;
        }

        protected async Task<ActivityStatus> CompensateAsync(string name)
        {
            var task = Task.Run<ActivityStatus>(() =>
            {
                return InteractiveCompensate(name);
            });

            return await task;
        }


        private static ActivityStatus GetStatus(string? reply)
        {
            if (reply != null)
            {
                var cleanReply = reply.Trim().ToLower();
                if (cleanReply == "y" || cleanReply == "yes")
                {
                    return ActivityStatus.Succeeded;
                }
            }

            return ActivityStatus.Failed;
        }
    }
    public class Activity1 : InteractiveActivity, IActivity
    {
        public string Name => "One";

        public async Task<ActivityStatus> CompensateAsync()
        {
            return await CompensateAsync(Name);
        }

        public async Task<ActivityStatus> ExecuteAsync()
        {
            return await ExecuteAsync(Name);
        }
    }

    public class Activity2 : InteractiveActivity, IActivity
    {
        public string Name => "Two";

        public async Task<ActivityStatus> CompensateAsync()
        {
            return await CompensateAsync(Name);
        }

        public async Task<ActivityStatus> ExecuteAsync()
        {
            return await ExecuteAsync(Name);
        }

    }

    public class Activity3 : InteractiveActivity, IActivity
    {
        public string Name => "Three";

        public async Task<ActivityStatus> CompensateAsync()
        {
            return await CompensateAsync(Name);
        }

        public async Task<ActivityStatus> ExecuteAsync()
        {
            return await ExecuteAsync(Name);
        }

    }
}
