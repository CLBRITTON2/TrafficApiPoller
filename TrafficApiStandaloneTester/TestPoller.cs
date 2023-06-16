using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WashingtonTrafficApi;

namespace TrafficApiStandaloneTester
{
    public class TestPoller
    {
        private bool _isRunning;
        private CancellationTokenSource _cts;
        private Task _poll;
        private TestTrafficApiLog _log;
        private int _pollingInterval;
        private TestTrafficApi _trafficApi;
        private DateTime _lastPollUpdateTime;

        public TestPoller()
        {
            _log = new TestTrafficApiLog();
            _pollingInterval = 10000;
            _trafficApi = new TestTrafficApi();
            _lastPollUpdateTime = DateTime.Now;
        }

        private async void PollForTrafficEvents()
        {
            _log.LogWrite("WashingtonTrafficApi poller starting");

            List<TestAlert> alerts = new List<TestAlert>();

            while (_isRunning)
            {
                try
                {
                    _cts.Token.ThrowIfCancellationRequested();
                    alerts = _trafficApi.GetTrafficAlerts();

                    _log.LogWrite($"Polling... Current interval: {_pollingInterval}");
                    foreach (var alert in alerts.Where(x => x.LastUpdatedTime > _lastPollUpdateTime))
                    {
                        HandleTrafficAlert(alert);
                    }
                    await Task.Delay(_pollingInterval, _cts.Token);
                }
                catch (TaskCanceledException)
                {
                    _isRunning = false;
                    break;
                }
                catch (Exception exception)
                {
                    _isRunning = false;
                    _log.LogWrite(exception.ToString());
                    break;
                }
            }
        }

        private void HandleTrafficAlert(TestAlert alert)
        {

            _lastPollUpdateTime = alert.LastUpdatedTime;
            Console.WriteLine($"Alert ID: {alert.AlertID}");
            Console.WriteLine($"Priority: {alert.Priority}");
            Console.WriteLine($"Description: {alert.HeadlineDescription}");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        public void StartPoller()
        {
            _isRunning = true;
            _cts = new CancellationTokenSource();
            _poll = Task.Run(() => PollForTrafficEvents(), _cts.Token);
        }

        public void StopPoller()
        {
            _isRunning = false;
            CancelToken();
        }
        private void CancelToken()
        {
            // If token can be cancelled and cancellation has not been requested, request cancel
            if (_cts != null && _cts.Token.CanBeCanceled && !_cts.Token.IsCancellationRequested)
            {
                _log.LogWrite("Cancelling token...");
                _cts.Cancel();
                try
                {
                    _log.LogWrite("Waiting for cancelled task to finish...");
                    _poll.Wait();
                }
                catch (OperationCanceledException)
                {
                    // Do Nothing
                }
                _log.LogWrite("Task done...");
                _cts = null;
                _poll = null;
            }
        }
    }
}
