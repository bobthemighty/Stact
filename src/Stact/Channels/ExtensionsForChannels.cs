// Copyright 2010 Chris Patterson
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
namespace Stact
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using Configuration;
	using Configuration.Internal;
	using Magnum;
	using Visitors;


	public static class ExtensionsForChannels
	{
		public static ChannelConnection Connect<T>(this Channel<T> channel,
		                                           Action<ConnectionConfigurator<T>> subscriberActions)
		{
			Guard.AgainstNull(channel, "channel");

			var subscriber = new TypedConnectionConfigurator<T>(channel);

			subscriberActions(subscriber);

			return subscriber.Complete();
		}

		public static ChannelConnection Connect(this UntypedChannel channel, Action<ConnectionConfigurator> subscriberActions)
		{
			Guard.AgainstNull(channel, "channel");

			var subscriber = new UntypedConnectionConfigurator(channel);

			subscriberActions(subscriber);

			return subscriber.CreateConnection();
		}

		public static IEnumerable<Channel> Flatten<T>(this Channel<T> channel)
		{
			return new FlattenChannelVisitor().Flatten(channel);
		}

		public static IEnumerable<Channel> Flatten(this UntypedChannel channel)
		{
			return new FlattenChannelVisitor().Flatten(channel);
		}

		public static bool SendRequestWaitForResponse<TRequest>(this UntypedChannel channel, TRequest request, TimeSpan timeout)
		{
			using (var reset = new ManualResetEvent(false))
			{
				var responseChannel = new ChannelAdapter();
				using (responseChannel.Connect(x =>
				{
					x.AddConsumerOf<Response<TRequest>>()
						.UsingConsumer(m => reset.Set())
						.HandleOnCallingThread();
				}))
				{
					channel.Request(request, responseChannel);

					return reset.WaitOne(timeout, true);
				}
			}
		}

		public static bool SendRequestWaitForResponse<TRequest>(this UntypedChannel channel, TimeSpan timeout)
		{
			using (var reset = new ManualResetEvent(false))
			{
				var responseChannel = new ChannelAdapter();
				using (responseChannel.Connect(x =>
				{
					x.AddConsumerOf<Response<TRequest>>()
						.UsingConsumer(m => reset.Set())
						.HandleOnCallingThread();
				}))
				{
					channel.Request<TRequest>(responseChannel);

					return reset.WaitOne(timeout, true);
				}
			}
		}
	}
}