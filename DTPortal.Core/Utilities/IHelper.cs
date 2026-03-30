namespace DTPortal.Core.Utilities
{
    public interface IHelper
    {
        public string GetErrorMsg(uint code, string message = null);

        public string GetRedisErrorMsg(int code, uint default_code);
    }
}