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

namespace thZero.Services
{
	public interface IServiceCacheSync : IServiceCache
	{
		void Add(string key, object value);
		void Add(string key, object value, bool forceCache);
		void Add(string key, object value, string region);
		void Add(string key, object value, string region, bool forceCache);
		void AddNonExpiring(string key, object value);
		void AddNonExpiring(string key, object value, string region);
		void AddNonExpiring(string key, object value, string region, bool forceCache);
		void Clear();
		void Clear(string region);
		bool Contains(string key);
		bool Contains(string key, bool forceCache);
		bool Contains(string key, string region);
		bool Contains(string key, string region, bool forceCache);
		T Get<T>(string key); //where T : class;
		T Get<T>(string key, bool forceCache); //where T : class;
		T Get<T>(string key, string region); //where T : class;
		T Get<T>(string key, string region, bool forceCache);//where T : class;
		void Remove(string key);
		void Remove(string key, bool forceCache);
		void Remove(string key, string region);
		void Remove(string key, string region, bool forceCache);
		long Size();
		Dictionary<string, long> SizeRegions();
	}
}