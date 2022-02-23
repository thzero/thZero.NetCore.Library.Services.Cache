/* ------------------------------------------------------------------------- *
thZero.NetCore.Library.Services.Cache
Copyright (C) 2016-2022 thZero.com

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
using System.Threading.Tasks;

namespace thZero.Services
{
	public interface IServiceCacheExecute : IServiceCacheAsync
	{
		Task<bool> Add<T>(IServiceCacheExecutableItemWithKey<T> executable, T value)
			where T : IServiceCacheExecuteResponse;
		Task<bool> Add<T>(IServiceCacheExecutableItemWithKey<T> executable, T value, string region)
			where T : IServiceCacheExecuteResponse;
		Task<bool> Add<T>(IServiceCacheExecutableItemWithKey<T> executable, T value, string region, bool forceCache)
			where T : IServiceCacheExecuteResponse;

		Task<T> Check<T>(IServiceCacheExecutableItemWithKey<T> executable, string region)
			where T : IServiceCacheExecuteResponse;

		Task<T> Check<T>(IServiceCacheExecutableItemWithKey<T> executable, string region, bool forceCache)
			where T : IServiceCacheExecuteResponse;

		Task<T> Check<T>(IServiceCacheExecutableItemWithKey<T> executable, string region, bool execute, bool forceCache)
			where T : IServiceCacheExecuteResponse;

		Task<T> Check<T>(IServiceCacheExecutableItemWithKey<T> executable, IServiceCacheExecute secondary, string region)
			where T : IServiceCacheExecuteResponse;
		Task<T> Check<T>(IServiceCacheExecutableItemWithKey<T> executable, IServiceCacheExecute secondary, string region, bool forceCache)
			where T : IServiceCacheExecuteResponse;
		Task<T> Check<T>(IServiceCacheExecutableItemWithKey<T> executable, IServiceCacheExecute secondary, string region, bool execute, bool forceCache)
			where T : IServiceCacheExecuteResponse;
	}
}