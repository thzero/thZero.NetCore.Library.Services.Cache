/* ------------------------------------------------------------------------- *
thZero.NetCore.Library.Services.Cache
Copyright (C) 2016-2021 thZero.com

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
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Nito.AsyncEx;

namespace thZero.Services
{
	public abstract class ServiceBaseCacheAsync<TService> : IntermediaryServiceBase<TService>, IServiceCacheAsync
	{
        public ServiceBaseCacheAsync(thZero.Services.IServiceLog log, ILogger<TService> logger) : base(log, logger)
        {
		}

		#region Public Methods
		public virtual void Initialize()
		{
		}

		public async Task<bool> Add<T>(string key, T value)
		{
			return await Add<T>(key, value, null, false);
		}

		public async Task<bool> Add<T>(string key, T value, bool forceCache)
		{
			return await Add(key, value, null, forceCache);
		}

		public async Task<bool> Add<T>(string key, T value, string region)
		{
			return await Add<T>(key, value, region, false);
		}

		public async Task<bool> Add<T>(string key, T value, string region, bool forceCache)
		{
			if (!UseCache(forceCache))
				return false;

			IDisposable lockResult = null;

			try
			{
				if (CacheLockEnabled)
					lockResult = await Lock.WriterLockAsync();

				return await AddCore<T>(key, value, region);
			}
			finally
			{
				if (lockResult != null)
					lockResult.Dispose();
			}
		}

		public async Task<bool> Clear()
		{
			return await Clear(false);
		}

		public async Task<bool> Clear(bool forceCache)
		{
			if (!UseCache(forceCache))
				return false;

			IDisposable lockResult = null;

			try
			{
				if (CacheLockEnabled)
					lockResult = await Lock.WriterLockAsync();

				return await ClearAllCore();
			}
			finally
			{
				if (lockResult != null)
					lockResult.Dispose();
			}
		}

		public async Task<bool> Clear(string region)
		{
			return await Clear(region, false);
		}

		public async Task<bool> Clear(string region, bool forceCache)
		{
			IDisposable lockResult = null;

			try
			{
				if (CacheLockEnabled)
					lockResult = await Lock.WriterLockAsync();

				return await ClearCore(region);
			}
			finally
			{
				if (lockResult != null)
					lockResult.Dispose();
			}
		}

		public async Task<bool> Contains(string key)
		{
			return await Contains(key, null, false);
		}

		public async Task<bool> Contains(string key, bool forceCache)
		{
			return await Contains(key, null, forceCache);
		}

		public async Task<bool> Contains(string key, string region)
		{
			return await Contains(key, region, false);
		}

		public async Task<bool> Contains(string key, string region, bool forceCache)
		{
			if (!UseCache(forceCache))
				return false;

			IDisposable lockResult = null;

			try
			{
				if (CacheLockEnabled)
					lockResult = await Lock.ReaderLockAsync();

				return await ContainsCore(key, region);
			}
			finally
			{
				if (lockResult != null)
					lockResult.Dispose();
			}
		}

		public async Task<T> Get<T>(string key)
		{
			return await Get<T>(key, null, false);
		}

		public async Task<T> Get<T>(string key, bool forceCache)
		{
			return await Get<T>(key, null, forceCache);
		}

		public async Task<T> Get<T>(string key, string region)
		{
			return await Get<T>(key, region, false);
		}

		public async Task<T> Get<T>(string key, string region, bool forceCache)
		{
			if (!UseCache(forceCache))
				return default;

			IDisposable lockResult = null;

			try
			{
				if (CacheLockEnabled)
					lockResult = await Lock.ReaderLockAsync();

				return await GetCore<T>(key, region);
			}
			finally
			{
				if (lockResult != null)
					lockResult.Dispose();
			}
		}

		public async Task<bool> Initialize(ServiceCacheConfig config)
		{
			return await InitializeCore(config);
		}

		public async Task<bool> MaintainCache()
		{
			if (!UseCache(false))
				return false;

			IDisposable lockResult = null;

			try
			{
				if (CacheLockEnabled)
					lockResult = await Lock.WriterLockAsync();

				return await MaintainCacheCore();
			}
			finally
			{
				if (lockResult != null)
					lockResult.Dispose();
			}
		}

		public async Task<bool> Remove(string key)
		{
			return await Remove(key, null, false);
		}

		public async Task<bool> Remove(string key, bool forceCache)
		{
			return await Remove(key, null, forceCache);
		}

		public async Task<bool> Remove(string key, string region)
		{
			return await Remove(key, region, false);
		}

		public async Task<bool> Remove(string key, string region, bool forceCache)
		{
			if (!UseCache(forceCache))
				return false;

			IDisposable lockResult = null;

			try
			{
				if (CacheLockEnabled)
					lockResult = await Lock.WriterLockAsync();

				return await RemoveCore(key, region);
			}
			finally
			{
				if (lockResult != null)
					lockResult.Dispose();
			}
		}

		public async Task<long> Size()
		{
			IDisposable lockResult = null;

			try
			{
				if (CacheLockEnabled)
					lockResult = await Lock.ReaderLockAsync();

				return await SizeCore();
			}
			finally
			{
				if (lockResult != null)
					lockResult.Dispose();
			}
		}

		public async Task<Dictionary<string, long>> SizeRegions()
		{
			IDisposable lockResult = null;

			try
			{
				if (CacheLockEnabled)
					lockResult = await Lock.ReaderLockAsync();

				return await SizeRegionsCore();
			}
			finally
			{
				if (lockResult != null)
					lockResult.Dispose();
			}
		}

		public async Task<ServiceCacheStats> StatsAsync()
		{
			IDisposable lockResult = null;

			try
			{
				if (CacheLockEnabled)
					lockResult = await Lock.ReaderLockAsync();

				return await StatsCore();
			}
			finally
			{
				if (lockResult != null)
					lockResult.Dispose();
			}
		}
		#endregion

		#region Public Properties
		public virtual int? MaxSize { get { return _maxSize; } set { _maxSize = value; } }
		public virtual long? MaxTimeToLive { get { return _maxTimeToLive; } set { _maxTimeToLive = value; } }

		public bool CacheEnabled
		{
			get { return _cacheEnabled; }
			set { _cacheEnabled = value; }
		}
		#endregion

		#region Protected Methods
		protected abstract Task<bool> AddCore<T>(string key, T value, string region);

		protected abstract Task<bool> ClearCore(string region);

		protected abstract Task<bool> ClearAllCore();

		protected abstract Task<bool> ContainsCore(string key, string region);

		protected abstract Task<T> GetCore<T>(string key, string region);

		protected abstract Task<bool> InitializeCore(ServiceCacheConfig config);

		protected abstract Task<bool> MaintainCacheCore();

		protected abstract Task<bool> RemoveCore(string key, string region);

		protected abstract Task<long> SizeCore();

		protected abstract Task<Dictionary<string, long>> SizeRegionsCore();

		protected abstract Task<ServiceCacheStats> StatsCore();

		protected virtual bool UseCache(bool forceCache)
		{
			return forceCache || CacheEnabled;
		}
		#endregion

		#region Protected Properties
		protected abstract bool CacheLockEnabled { get; }

		protected abstract AsyncReaderWriterLock Lock { get; }
		#endregion

		#region Fields
		private static int? _maxSize;
		private static long? _maxTimeToLive;
		private static bool _cacheEnabled = true;
		#endregion
	}
}