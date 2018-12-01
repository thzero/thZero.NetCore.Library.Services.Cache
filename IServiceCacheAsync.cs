/* ------------------------------------------------------------------------- *
thZero.NetCore.Library
Copyright (C) 2016-2017 thZero.com

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

namespace thZero.Services
{
	public interface IServiceCacheAsync : IServiceCache
	{
		Task<bool> Add<T>(string key, T value);
		Task<bool> Add<T>(string key, T value, bool forceCache);
		Task<bool> Add<T>(string key, T value, string region);
		Task<bool> Add<T>(string key, T value, string region, bool forceCache);
		Task<bool> Clear();
		Task<bool> Clear(string region);
		Task<bool> Contains(string key);
		Task<bool> Contains(string key, bool forceCache);
		Task<bool> Contains(string key, string region);
		Task<bool> Contains(string key, string region, bool forceCache);
		Task<T> Get<T>(string key); //where T : class;
		Task<T> Get<T>(string key, bool forceCache); //where T : class;
		Task<T> Get<T>(string key, string region); //where T : class;
		Task<T> Get<T>(string key, string region, bool forceCache);//where T : class;
		Task<bool> Initialize(ServiceCacheConfig config);
		Task<bool> MaintainCache();
		Task<bool> Remove(string key);
		Task<bool> Remove(string key, bool forceCache);
		Task<bool> Remove(string key, string region);
		Task<bool> Remove(string key, string region, bool forceCache);
		Task<long> Size();
		Task<Dictionary<string, long>> SizeRegions();
		Task<ServiceCacheStats> StatsAsync();
	}
}