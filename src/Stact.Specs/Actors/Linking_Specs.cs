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
	using Magnum.TestFramework;



	[Scenario]
	public class Linking_Specs
	{
		ActorInstance _a;
		ActorInstance _b;

		[When]
		public void When_two_actors_are_linked()
		{
			_a = AnonymousActor.New(inbox =>
			{
				inbox.Receive<Die>(x =>
				{
					inbox.Exit();
				});
			});

			_b = AnonymousActor.New(inbox =>
			{
				_a.Link(inbox);

				inbox.Receive<Die>(x =>
				{
				});
			});
		}


		class Die
		{
		}

	}
}