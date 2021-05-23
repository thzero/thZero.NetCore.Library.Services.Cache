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

using thZero.Responses;

namespace thZero.Services
{
	public interface IServiceCacheExecuteResponse : IDurationResponse
	{
		[Newtonsoft.Json.JsonIgnore]
		bool Cacheable { get; }
		bool CacheEnabled { get; set; }
        IEnumerable<Guid> Ids { get; }
		bool WasCached { get; set; }
		bool WasCachedSecondary { get; set; }
	}
}