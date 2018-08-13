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
	public class StatsResults
	{
		public StatsResults()
		{
			Timestamp = DateTime.UtcNow.Millisecond;
		}

		#region Public Methods
		public void Add(string key, string value)
		{
			if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
				return;

			if (_additionals.ContainsKey(key))
				return;

			_additionals.Add(key, value);
		}
		#endregion

		#region Public Properties
		public Dictionary<string, string> Additional { get { return _additionals; } }
		public long? Storage { get; set; }
		public long? StorageMax { get; set; }
		public long Timestamp { get; set; }
		#endregion

		#region Fields
		private readonly Dictionary<string, string> _additionals = new Dictionary<string, string>();
		#endregion
	}
}