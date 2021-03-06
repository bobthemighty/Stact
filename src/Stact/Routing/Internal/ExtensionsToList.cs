﻿// Copyright 2010 Chris Patterson
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Stact.Routing.Internal
{
	using System.Collections.Generic;
	using System.Linq;
	using Magnum.Extensions;


	public static class ExtensionsToList
	{
		public static Continuation<T> GetListContinuation<T>(this IList<T> list)
		{
			if (list.Count == 0)
				return _ => { };

			T[] messages = list.ToArray();

			return x => messages.Each(x);
		}

		public static Continuation<T> GetMatchesContinuation<T>(this IList<T> list, T match)
		{
			if (list.Any(x => x.Equals(match)))
				return x => x(match);

			return _ => { };
		}
	}
}