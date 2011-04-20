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
	using Internal;


	/// <summary>
	///   A synchronous fiber will execute an action immediately on the calling thread
	///   without any protection from an exception
	/// </summary>
	public class SynchronousFiber :
		Fiber
	{
		readonly OperationExecutor _executor;
		bool _shuttingDown;

		public SynchronousFiber()
			: this(new BasicOperationExecutor())
		{
		}

		public SynchronousFiber(OperationExecutor executor)
		{
			_executor = executor;
		}

		public void Add(Action operation)
		{
			if (_shuttingDown)
				return;
				// seems to be causing more problems that it solves
				// throw new FiberException("The fiber is no longer accepting actions");

			_executor.Execute(operation);
		}

		public void Stop()
		{
			_executor.Stop();
		}

		public void Shutdown(TimeSpan timeout)
		{
			_shuttingDown = true;
		}
	}
}