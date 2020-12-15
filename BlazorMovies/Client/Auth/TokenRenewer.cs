using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace BlazorMovies.Client.Auth
{
    public class TokenRenewer : IDisposable
    {
        public TokenRenewer(ILoginService loginService)
        {
            _loginService = loginService;
        }

        Timer timer;
        private readonly ILoginService _loginService;

        public void Initiate()
        {
            timer = new Timer();
            timer.Interval = 1000 * 60 * 4; // 4 minutes
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _loginService.TryRenewToken();
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
