using System.Threading;
using System.Threading.Tasks;
using LightLog;

namespace LogTestApp_Net472
{
    public class MainViewModel
    {
        public Logger Logger { get; } = LogManager.Instance;

        public MainViewModel()
        {
            Logger.LogFileName = "test.log";
            Logger.NewRecordsFirst = true;

            Logger.Debug("Program started");

            PrintInfo(0);
            Task.Run(PrintTask);
            var th1 = new Thread(PrintInfo);
            var th2 = new Thread(PrintInfo);
            th1.Start(1); 
            th2.Start(2);
        }

        private void PrintInfo(object threadNumber)
        {
            for (int i = 0; i < 1000; i++)
            {
                Logger.Info($"Thread {(int)threadNumber} - Information message {i}");
            }
        }

        private Task PrintTask()
        {
            for (int i = 0; i < 1000; i++)
            {
                Logger.Info($"Task - Information message {i}");
            }

            return Task.CompletedTask;
        }
    }
}
