using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.SharedKernel.DistributedLock.DistributedLock.Sql.Contracts
{
    public interface IDistributedLockClient
    {
        IDisposable AcquireLock(string lockName, TimeSpan? timeSpan = null, CancellationToken cancellationToken = default);
        IDisposable TryAcquireLock(string lockName, TimeSpan timeSpan = default, CancellationToken cancellationToken = default);
    }
}
