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


	public static class ExtensionsToFiber
	{
		/// <summary>
		///   Creates a disposable object that calls Shutdown on the fiber when it is
		///   disposed
		/// </summary>
		/// <param name = "fiber">The fiber to shutdown</param>
		/// <param name = "timeout">The timeout to wait for the shutdown to complete</param>
		/// <returns>An IDisposable object</returns>
		public static IDisposable ShutdownOnDispose(this Fiber fiber, TimeSpan timeout)
		{
			return new ShutdownFiberOnDispose(fiber, timeout);
		}

		/// <summary>
		///   Creates a disposable object that calls Stop on the fiber when it is
		///   disposed
		/// </summary>
		/// <param name = "fiber">The fiber to stop</param>
		/// <returns>An IDisposable object</returns>
		public static IDisposable StopOnDispose(this Fiber fiber)
		{
			return new StopFiberOnDispose(fiber);
		}

		/// <summary>
		/// Signals the fiber to shut down, but does not wait for the remaining operations
		/// to be executed
		/// </summary>
		/// <param name="fiber"></param>
		public static void Shutdown(this Fiber fiber)
		{
			fiber.Shutdown(TimeSpan.Zero);
		}
	}
}