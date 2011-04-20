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
	using System;
	using System.Collections.Generic;


	public class AlphaMemory<T>
	{
		readonly SuccessorList<T> _successors;
		readonly ActivatedMessageList<T> _messages;

		public AlphaMemory()
		{
			_messages = new ActivatedMessageList<T>();
			_successors = new SuccessorList<T>();
		}

		public IEnumerable<Activation<T>> Successors
		{
			get { return _successors; }
		}

		protected void Add(RoutingContext<T> message)
		{
			_messages.Add(message);
		}

		protected void All(Func<RoutingContext<T>, bool> callback)
		{
			_messages.All(callback);
		}

		protected void Any(RoutingContext<T> match, Action<RoutingContext<T>> callback)
		{
			_messages.Any(match, callback);
		}

		public void AddActivation(Activation<T> activation)
		{
			_successors.Add(activation);

			_messages.All(context =>
				{
					if (!activation.IsAlive)
						return false;

					if(context.IsAlive)
						activation.Activate(context);

					return true;
				});
		}

		public void RemoveActivation(Activation<T> activation)
		{
			_successors.Remove(activation);
		}
	}
}