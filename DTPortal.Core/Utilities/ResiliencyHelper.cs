using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly;

namespace DTPortal.Core.Utilities
{
    public class ResiliencyHelper<T>
    {
        private ILogger<T> _logger;
        private const int _retryCount = 5;

        public ResiliencyHelper(ILogger<T> logger)
        {
            _logger = logger;
        }

        //public async Task ExecuteResilient(Func<Task> action, Action fallbackResult)
        //{
        //    var retryPolicy = Policy
        //        .Handle<Exception>((ex) =>
        //        {
        //            _logger.LogWarning($"Error occured during request-execution. Polly will retry. Exception: {ex.Message}");
        //            return true;
        //        })
        //        .RetryAsync(3);

        //    await retryPolicy.ExecuteAsync(action);
        //}

        //public void ExecuteResilient(Action action, Action fallbackResult)
        //{
        //    var retryPolicy = Policy
        //        .Handle<Exception>((ex) =>
        //        {
        //            _logger.LogWarning($"Error occured during request-execution. Polly will retry. Exception: {ex.Message}");
        //            return true;
        //        })
        //        .Retry(5);

        //    var fallbackPolicy = Policy<Action>
        //        .Handle<Exception>()
        //        .Fallback(
        //        () => fallbackResult(),
        //        (e, c) => Task.Run(() => _logger.LogError($"Error occured during request-execution. Polly will fallback. Exception: {e.Exception.ToString()}")));

        //    fallbackPolicy
        //        .Wrap(retryPolicy)
        //        .Execute(() => action);
        //}

        //public void ExecuteResilient(Func<Task<int>> action, Action fallbackResult)
        //{
        //    var retryPolicy = Policy
        //        .Handle<Exception>((ex) =>
        //        {
        //            _logger.LogWarning($"Error occured during request-execution. Polly will retry. Exception: {ex.Message}");
        //            return true;
        //        })
        //        .Retry(5);

        //    var fallbackPolicy = Policy<Action>
        //        .Handle<Exception>()
        //        .Fallback(
        //        () => fallbackResult(),
        //        (e, c) => Task.Run(() => _logger.LogError($"Error occured during request-execution. Polly will fallback. Exception: {e.Exception.ToString()}")));

        //    fallbackPolicy
        //        .Wrap(retryPolicy)
        //        .Execute(action);
        //}

        //public async Task ExecuteResilient(Func<Task> action, Task fallbackResult)
        //{
        //    var retryPolicy = Policy
        //        .Handle<Exception>((ex) =>
        //        {
        //            _logger.LogWarning($"Error occured during request-execution. Polly will retry. Exception: {ex.Message}");
        //            return true;
        //        })
        //        .RetryAsync(5);

        //    var fallbackPolicy = Policy
        //        .Handle<Exception>()
        //        .FallbackAsync(
        //        Task.Run(() => fallbackResult),
        //        (e, c) => Task.Run(() => _logger.LogError($"Error occured during request-execution. Polly will fallback. Exception: {e.Exception.ToString()}")));

        //   return fallbackPolicy
        //        .WrapAsync(retryPolicy)
        //        .ExecuteAsync(() => action)
        //        .ConfigureAwait(false);
        //}

        public void ExecuteResilient(Action action, Action fallbackResult)
        {
            Policy retryPolicy = Policy
                .Handle<Exception>((ex) =>
                {
                    _logger.LogWarning($"Error occured during request-execution. Polly will retry. Exception: {ex.Message}");
                    return true;
                })
                .Retry(5);

            Policy fallbackPolicy = Policy
                .Handle<Exception>()
                .Fallback(fallbackResult);

            fallbackPolicy
                 .Wrap(retryPolicy)
                 .Execute(action);
        }
    }
}
