﻿// Copyright 2010 Chris Patterson
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
	using Internal;


	/// <summary>
	///   A conditional consumer is given a message to evaluate, after which it
	///   can determine if it is interested in the message and return an action
	///   to process the message or null
	/// </summary>
	/// <typeparam name = "T">The message type</typeparam>
	/// <param name = "message">The message</param>
	/// <returns>An action to consume the message, or null</returns>
	[CanBeNull]
	public delegate Consumer<T> SelectiveConsumer<in T>([NotNull] T message);
}