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
namespace Stact.Specs.Actors
{
	using System;
	using Magnum.Extensions;
	using Magnum.TestFramework;


	[Scenario]
	public class When_an_actor_throws_an_exception
	{
		[Then]
		public void Should_receive_a_fault_message()
		{
			var received = new Future<Fault>();

			ActorInstance actor = AnonymousActor.New(inbox =>
			{
				inbox.Receive<Fault>(fault =>
				{
					received.Complete(fault);
				});

				throw new NotImplementedException("A");
			});

			received.WaitUntilCompleted(5.Seconds()).ShouldBeTrue();
			received.Value.Message.ShouldEqual("A");
		}
	}
}