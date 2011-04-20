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
	/// <summary>
	/// Delivers a message to a consumer on the specified fiber.
	/// </summary>
	/// <typeparam name="TChannel">The message type</typeparam>
	public class ConsumerNode<TChannel> : 
		ProductionNode<TChannel>,
		Activation<TChannel>
	{
		readonly Consumer<TChannel> _consumer;

		public ConsumerNode(Fiber fiber, Consumer<TChannel> consumer, bool disableOnActivation = true)
			: this(FiberConsumer(fiber, consumer), disableOnActivation)
		{
		}

		public ConsumerNode(Consumer<TChannel> consumer, bool disableOnActivation = true)
			: base(disableOnActivation)
		{
			_consumer = consumer;
		}

		public void Activate(RoutingContext<TChannel> context)
		{
			Accept(context, body => _consumer(body));
		}

		static Consumer<TChannel> FiberConsumer(Fiber fiber, Consumer<TChannel> consumer)
		{
			return message =>
			{
				fiber.Add(() => consumer(message));
			};
		}
	}
}