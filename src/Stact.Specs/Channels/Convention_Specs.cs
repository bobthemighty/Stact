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
namespace Stact.Specs.Channels
{
	using Magnum.Extensions;
	using Magnum.TestFramework;


	[Scenario]
	public class When_connecting_an_inbox_at_startup
	{
		[Then]
		public void Should_bind_channels_for_each_method()
		{
			var subject = new Subject();

			ActorFactory<Subject> factory = ActorFactory.Create<Subject>(x =>
				{
					x.ConstructedBy(() => subject);

					x.ConnectPublicMethods();
					x.ConnectPropertyChannels();
				});

			ActorInstance actor = factory.GetActor();

			subject.ShouldNotBeNull();

			actor.Send<Message<A>>();
			actor.Send<Request<B>>();
			actor.Send<Response<C>>();
			actor.Send<Message<D>>();

			subject.FutureA.WaitUntilCompleted(5.Seconds()).ShouldBeTrue();
			subject.FutureB.WaitUntilCompleted(5.Seconds()).ShouldBeTrue();
			subject.FutureC.WaitUntilCompleted(5.Seconds()).ShouldBeTrue();
			subject.FutureD.WaitUntilCompleted(5.Seconds()).ShouldBeTrue();
		}


		interface A
		{
		}


		interface B
		{
		}


		interface C
		{
		}


		interface D
		{
		}


		class Subject :
			Actor
		{
			public readonly Future<Message<A>> FutureA;
			public readonly Future<Message<B>> FutureB;
			public readonly Future<Message<C>> FutureC;
			public readonly FutureChannel<Message<D>> FutureD;

			public Subject()
			{
				FutureA = new Future<Message<A>>();
				FutureB = new Future<Message<B>>();
				FutureC = new Future<Message<C>>();
				FutureD = new FutureChannel<Message<D>>();
			}

			public Channel<Message<D>> DChannel
			{
				get { return FutureD; }
			}

			public void Handle(Message<A> message)
			{
				FutureA.Complete(message);
			}

			public void Handle(Request<B> message)
			{
				FutureB.Complete(message);
			}

			public void Handle(Response<C> message)
			{
				FutureC.Complete(message);
			}
		}
	}
}