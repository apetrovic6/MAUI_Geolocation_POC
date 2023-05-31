using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationUtils.Services.Interfaces
{
    public interface IBackgroundService
    {
        void Start();
        void Stop();
        bool IsServiceIsRunning();
    }
}
