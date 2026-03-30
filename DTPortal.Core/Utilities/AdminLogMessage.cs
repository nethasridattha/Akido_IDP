namespace DTPortal.Core.Utilities
{
    public class AdminLogMessage
    {
        public readonly string ModuleName;

        public readonly string ServiceName;

        public readonly string ActivityName;

        public readonly string Timestamp;

        public readonly string LogMessage;

        public readonly string LogMessageType;

        public readonly string UserName;

        public readonly string DataTransformation;

        public AdminLogMessage(string moduleName,
            string serviceName,
            string activityName,
            string logMessage,
            string logMessageType,
            string userName,
            string dataTransformation)
        {
            ModuleName = moduleName;
            ServiceName = serviceName;
            ActivityName = activityName;
            LogMessage = logMessage;
            LogMessageType = logMessageType;
            UserName = userName;
            DataTransformation = dataTransformation;
        }
    }
}
