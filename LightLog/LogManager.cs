namespace LightLog
{

    /// <summary>
    /// Provides single instance of <see cref="Logger"/>
    /// </summary>
    public static class LogManager
    {
        static LogManager()
        { }

        /// <summary>
        /// Logger instance
        /// </summary>
        public static Logger Instance { get; } = new Logger();
    }
}
