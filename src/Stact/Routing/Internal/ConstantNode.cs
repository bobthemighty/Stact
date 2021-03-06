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
namespace Stact.Routing.Internal
{
	using System;


	/// <summary>
	/// Always invokes on right activation for joining single alpha nodes
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ConstantNode<T> :
		RightActivation<T>
	{
		public void RightActivate(Func<RoutingContext<T>, bool> callback)
		{
			// ConstantNodes are not activated, so they would never push joins back to the calling node
		}

		public void RightActivate(RoutingContext<T> context, Action<RoutingContext<T>> callback)
		{
			callback(context);
		}
	}
}