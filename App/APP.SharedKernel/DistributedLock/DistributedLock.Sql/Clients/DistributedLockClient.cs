using APP.SharedKernel.DistributedLock.DistributedLock.Sql.Contracts;
using Medallion.Threading;
using Medallion.Threading.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.SharedKernel.DistributedLock.DistributedLock.Sql.Clients
{
    public class DistributedLockClient : IDistributedLockClient
    {
        private readonly IDistributedLockProvider _distributedLock;

        public DistributedLockClient(DistributedLockConfiguration distributedLockConfiguration)
        {
            _distributedLock = new SqlDistributedSynchronizationProvider(distributedLockConfiguration.ConnectionString);
        }

        public IDisposable AcquireLock(string lockName, TimeSpan? timeSpan = null, CancellationToken cancellationToken = default)
        {
            return _distributedLock.AcquireLock(lockName, timeSpan, cancellationToken);
        }

        public IDisposable TryAcquireLock(string lockName, TimeSpan timeSpan = default, CancellationToken cancellationToken = default)
        {
            return _distributedLock.TryAcquireLock(lockName, timeSpan, cancellationToken);
        }
    }
}
