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
namespace Stact.Internal
{
	using System;
	using System.Linq.Expressions;
	using System.Reflection;


	/// <summary>
	///   Provides a channel from an actor, based on the property information which is used
	///   to create a dynamic method that returns the actual channel
	/// </summary>
	/// <typeparam name = "TActor">The actor type</typeparam>
	/// <typeparam name = "TChannel">The channel type</typeparam>
	public class ActorChannelProvider<TActor, TChannel> :
		ChannelProvider<TChannel>
		where TActor : class, Actor
	{
		private readonly ActorFactory<TActor> _actorFactory;
		private readonly ChannelAccessor<TActor, TChannel> _channelAccessor;

		public ActorChannelProvider(ActorFactory<TActor> actorFactory, PropertyInfo property)
		{
			Magnum.Guard.AgainstNull(actorFactory, "actorInstanceProvider");
			Magnum.Guard.AgainstNull(property, "property");

			_actorFactory = actorFactory;

			_channelAccessor = CreateChannelAccessor(property);
		}

		public Channel<TChannel> GetChannel(TChannel message)
		{
			throw new NotImplementedException("This is changing to inbox usage");
		}

		private static ChannelAccessor<TActor, TChannel> CreateChannelAccessor(PropertyInfo property)
		{
			ParameterExpression actor = Expression.Parameter(typeof (TActor), "actor");
			MethodCallExpression getter = Expression.Call(actor, property.GetGetMethod(true));

			return Expression.Lambda<ChannelAccessor<TActor, TChannel>>(getter, new[] {actor})
				.Compile();
		}
	}
}