using LightLog;

namespace LogTestApp_Net472
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Logger.LogFileName = "test.log";
            Logger.Debug("Program started");
        }
    }
}
