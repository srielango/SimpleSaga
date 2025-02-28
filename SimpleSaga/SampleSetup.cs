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
            if (reply != null)
            {
                reply = reply.Trim().ToLower();
                if (reply == "y" || reply == "yes")
                {
                    status = ActivityStatus.Succeeded;
                }
                else
                {
                    status = ActivityStatus.Failed;
                }
            }
            else
            {
                status = ActivityStatus.Failed;
            }

            if (status == ActivityStatus.Succeeded)
            {
                Console.WriteLine($"Activity {activityName} execution succeeded");
            }
            else
            {
                Console.WriteLine($"Activity {activityName} execution succeeded");
            }
            return status;
        }
        protected ActivityStatus InteractiveCompensate(string activityName)
        {
            ActivityStatus status;

            Console.WriteLine($"\nActivity {activityName} compensating ...");
            Console.Write("Enter Return Status (Y=Succeeded, N=Failed)? ");

            var reply = Console.ReadLine();
            if (reply != null)
            {
                reply = reply.Trim().ToLower();
                if (reply == "y" || reply == "yes")
                    status = ActivityStatus.Succeeded;
                else
                    status = ActivityStatus.Failed;
            }
            else
                status = ActivityStatus.Failed;

            if (status == ActivityStatus.Succeeded)
                Console.WriteLine($"Activity {activityName} Compensation Succeeded.");
            else
                Console.WriteLine($"Activity {activityName} Compensation Failed!");

            return status;
        }
    }
    public class Activity1 : InteractiveActivity, IActivity
    {
        public string Name => "One";

        public async Task<ActivityStatus> CompensateAsync()
        {
            var task = Task.Run<ActivityStatus>(() =>
            {
                return InteractiveCompensate(Name);
            });

            return await task;
        }

        public async Task<ActivityStatus> ExecuteAsync()
        {
            var task = Task.Run<ActivityStatus>(() =>
            {
                return InteractiveExecute(Name);
            });

            return await task;
        }
    }

    public class Activity2 : InteractiveActivity, IActivity
    {
        public string Name => "Two";

        public async Task<ActivityStatus> CompensateAsync()
        {
            var task = Task.Run<ActivityStatus>(() =>
            {
                return InteractiveCompensate(Name);
            });

            return await task;
        }

        public async Task<ActivityStatus> ExecuteAsync()
        {
            var task = Task.Run<ActivityStatus>(() =>
            {
                return InteractiveExecute(Name);
            });

            return await task;
        }
    }

    public class Activity3 : InteractiveActivity, IActivity
    {
        public string Name => "Three";

        public async Task<ActivityStatus> CompensateAsync()
        {
            var task = Task.Run<ActivityStatus>(() =>
            {
                return InteractiveCompensate(Name);
            });

            return await task;
        }

        public async Task<ActivityStatus> ExecuteAsync()
        {
            var task = Task.Run<ActivityStatus>(() =>
            {
                return InteractiveExecute(Name);
            });

            return await task;
        }
    }
}
