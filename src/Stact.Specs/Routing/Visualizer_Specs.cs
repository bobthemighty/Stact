namespace Stact.Specs
{
	using System.Diagnostics;
	using Internal;
	using Magnum.Extensions;
	using Magnum.TestFramework;
	using Routing;
	using Routing.Visualizers;


	[Scenario]
	public class Show_me_visualization
	{
		[Then]
		public void Should_display_the_empty_network()
		{
			var received = new Future<A>();

			var engine = new DynamicRoutingEngine(new PoolFiber());

			engine.Send(new A());

			Trace.WriteLine("Before Receive");
			var visualizer = new TraceRoutingEngineVisualizer();
			visualizer.Show(engine);

			engine.Configure(x => x.Receive<A>(received.Complete));

			Trace.WriteLine("After Receive");
			visualizer.Show(engine);

			received.WaitUntilCompleted(2.Seconds()).ShouldBeTrue();
		}


		[Then]
		public void Should_have_the_bits_without_the_message_first()
		{
			var engine = new DynamicRoutingEngine(new PoolFiber());
			var visualizer = new TraceRoutingEngineVisualizer();

			var received = new Future<A>();
			engine.Configure(x => x.Receive<A>(received.Complete));

			var block = new Future<int>();
			engine.Add(() =>
				{
					visualizer.Show(engine);
					block.Complete(0);
				});
			block.WaitUntilCompleted(2.Seconds());

			engine.Send(new A());
			received.WaitUntilCompleted(2.Seconds());

			engine.Send(new B());

			var receivedB = new Future<B>();
			engine.Configure(x => x.Receive<B>(receivedB.Complete));

			received.WaitUntilCompleted(200.Seconds()).ShouldBeTrue();
			receivedB.WaitUntilCompleted(200.Seconds()).ShouldBeTrue();

			//engine.Receive<A, B>(x => { });

			visualizer.Show(engine);
		}

		class A
		{
		}

		class B
		{
		}
	}
}