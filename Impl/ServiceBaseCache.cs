/* ------------------------------------------------------------------------- *
thZero.NetCore.Library.Services.Cache
Copyright (C) 2016-2019 thZero.com

<development [at] thzero [dot] com>

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 * ------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Threading;

using Microsoft.Extensions.Logging;

namespace thZero.Services
{
	public abstract class ServiceCacheBase<TService> : IntermediaryServiceBase<TService>, IServiceCacheBase
    {
        public ServiceCacheBase(thZero.Services.IServiceLog log, ILogger<TService> logger) : base(log, logger)
        {
        }

        #region Public Methods
        public virtual void Initialize()
		{
		}

		public void Add(string key, object value)
		{
			Add(key, value, null, false);
		}

		public void Add(string key, object value, bool forceCache)
		{
			Add(key, value, null, forceCache);
		}

		public void Add(string key, object value, string region)
		{
			Add(key, value, region, false);
		}

		public void Add(string key, object value, string region, bool forceCache)
		{
			if (!forceCache && !CacheEnabled)
				return;

			try
			{
				if (CacheLockEnabled)
					LockCache.EnterWriteLock();

				AddCore(key, value, null);
			}
			finally
			{
				if (CacheLockEnabled)
					LockCache.ExitWriteLock();
			}
		}

		public void AddNonExpiring(string key, object value)
		{
			AddNonExpiring(key, value, null, false);
		}

		public void AddNonExpiring(string key, object value, string region)
		{
			AddNonExpiring(key, value, region, false);
		}

		public void AddNonExpiring(string key, object value, string region, bool forceCache)
		{
			if (!forceCache && !CacheEnabled)
				return;

			try
			{
				if (CacheLockEnabled)
					LockCache.EnterWriteLock();

				AddNonExpiringCore(key, value, null);
			}
			finally
			{
				if (CacheLockEnabled)
					LockCache.ExitWriteLock();
			}
		}

		public void Clear()
		{
			Clear(false);
		}

		public void Clear(bool forceCache)
		{
			if (!forceCache && !CacheEnabled)
				return;

			try
			{
				if (CacheLockEnabled)
					LockCache.EnterWriteLock();

				ClearCore(null);
			}
			finally
			{
				if (CacheLockEnabled)
					LockCache.ExitWriteLock();
			}
		}

		public void Clear(string region)
		{
			Clear(region, false);
		}

		public void Clear(string region, bool forceCache)
		{
			if (!forceCache && !CacheEnabled)
				return;

			try
			{
				if (CacheLockEnabled)
					LockCache.EnterWriteLock();

				ClearCore(null);
			}
			finally
			{
				if (CacheLockEnabled)
					LockCache.ExitWriteLock();
			}
		}

		public bool Contains(string key)
		{
			return Contains(key, null, false);
		}

		public bool Contains(string key, bool forceCache)
		{
			if (!forceCache && !CacheEnabled)
				return false;

			return Contains(key, null, forceCache);
		}

		public bool Contains(string key, string region)
		{
			return Contains(key, region, false);
		}

		public bool Contains(string key, string region, bool forceCache)
		{
			if (!forceCache && !CacheEnabled)
				return false;

			try
			{
				if (CacheLockEnabled)
					LockCache.EnterReadLock();

				return ContainsCore(key, region);
			}
			finally
			{
				if (CacheLockEnabled)
					LockCache.ExitReadLock();
			}
		}

		public T Get<T>(string key)
		{
			return Get<T>(key, null, false);
		}

		public T Get<T>(string key, bool forceCache)
		{
			return Get<T>(key, null, forceCache);
		}

		public T Get<T>(string key, string region)
		{
			return Get<T>(key, region, false);
		}

		public T Get<T>(string key, string region, bool forceCache)
		{
			if (!forceCache && !CacheEnabled)
				return default(T);

			try
			{
				if (CacheLockEnabled)
					LockCache.EnterReadLock();

				return GetCore<T>(key, region);
			}
			finally
			{
				if (CacheLockEnabled)
					LockCache.ExitReadLock();
			}
		}

		public void Remove(string key)
		{
			Remove(key, null, false);
		}

		public void Remove(string key, bool forceCache)
		{
			if (!forceCache && !CacheEnabled)
				return;

			Remove(key, null, forceCache);
		}

		public void Remove(string key, string region)
		{
			Remove(key, region, false);
		}

		public void Remove(string key, string region, bool forceCache)
		{
			if (!forceCache && !CacheEnabled)
				return;

			try
			{
				if (CacheLockEnabled)
					LockCache.EnterWriteLock();

				RemoveCore(key, region);
			}
			finally
			{
				if (CacheLockEnabled)
					LockCache.ExitWriteLock();
			}
		}

		public long Size()
		{
			return SizeCore();
		}

		public Dictionary<string, long> SizeRegions()
		{
			try
			{
				if (CacheLockEnabled)
					LockCache.EnterWriteLock();

				return SizeRegionsCore();
			}
			finally
			{
				if (CacheLockEnabled)
					LockCache.ExitWriteLock();
			}
		}
		#endregion

		#region Public Properties
		public virtual int? MaxSize { get; set; }
		public virtual long? MaxTimeToLive { get; set; }

		public bool CacheEnabled
		{
			get { return _useCache; }
			set { _useCache = value; }
		}
		#endregion

		#region Protected Properties
		protected abstract bool CacheLockEnabled { get; }
		#endregion

		#region Protected Methods
		protected abstract void AddCore(string key, object value, string region);

		protected abstract void AddNonExpiringCore(string key, object value, string region);

		protected abstract void ClearCore(string region);

		protected abstract bool ContainsCore(string key, string region);

		protected abstract T GetCore<T>(string key, string region);

		protected abstract void RemoveCore(string key, string region);

		protected abstract long SizeCore();

		protected abstract Dictionary<string, long> SizeRegionsCore();
		#endregion

		#region Fields
		private static readonly ReaderWriterLockSlim LockCache = new ReaderWriterLockSlim();
		private static bool _useCache = true;
		#endregion
	}
}