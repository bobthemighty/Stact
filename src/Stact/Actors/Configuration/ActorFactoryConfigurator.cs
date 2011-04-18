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
namespace Stact.Configuration
{
	using System;


	public interface ActorFactoryConfigurator<TActor>
		where TActor : Actor
	{

		ActorFactoryConfigurator<TActor> ConstructedBy(Func<TActor> actorFactory);
		ActorFactoryConfigurator<TActor> ConstructedBy(Func<Inbox, TActor> actorFactory);
		ActorFactoryConfigurator<TActor> ConstructedBy(Func<Fiber, TActor> actorFactory);
		ActorFactoryConfigurator<TActor> ConstructedBy(Func<Fiber, Inbox, TActor> actorFactory);
		ActorFactoryConfigurator<TActor> ConstructedBy(Func<Fiber, Scheduler, Inbox, TActor> actorFactory);

		ActorFactoryConfigurator<TActor> HandleOnCallingThread();
		ActorFactoryConfigurator<TActor> HandleOnPoolFiber();
		ActorFactoryConfigurator<TActor> HandleOnThreadFiber();
		ActorFactoryConfigurator<TActor> UseFiberFactory(FiberFactoryEx fiberFactory);
		ActorFactoryConfigurator<TActor> UseShutdownTimeout(TimeSpan timeout);

		ActorFactoryConfigurator<TActor> UseSharedScheduler();
		ActorFactoryConfigurator<TActor> UseScheduler(Scheduler scheduler);
		ActorFactoryConfigurator<TActor> UseSchedulerFactory(SchedulerFactory schedulerFactory);

		/// <summary>
		/// Add conventions to apply to actor instances as they are created
		/// </summary>
		/// <param name="convention">The convention to apply to the actor instance</param>
		/// <returns>The configurator</returns>
		ActorFactoryConfigurator<TActor> AddConvention(ActorConvention<TActor> convention);
	}
}