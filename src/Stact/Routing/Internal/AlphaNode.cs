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
	public class AlphaNode<TChannel> :
		AlphaMemory<TChannel>,
		Activation,
		Activation<TChannel>
	{
		public void Activate<T>(RoutingContext<T> message)
		{
			message.CanConvertTo<TChannel>(Activate);
		}

		public bool IsAlive
		{
			get { return true; }
		}

		public void Activate(RoutingContext<TChannel> context)
		{
			Add(context);
		}
	}
}