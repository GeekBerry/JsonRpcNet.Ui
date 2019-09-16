﻿using System;
using System.Reflection;
using JsonRpcNet.Attributes;
using WebSocketSharp.Server;

namespace JsonRpcNet.WebSocketSharp.Extensions
{
	public static class WebSocketServerExtensions
	{
		public static void AddService<T>(this WebSocketServer server, Func<T> initializer) where T : WebSocketBehavior
		{
			var serviceType = typeof(T);
			var routePrefix = serviceType.GetCustomAttribute<JsonRpcServiceAttribute>()?.Path;
			if (string.IsNullOrWhiteSpace(routePrefix))
			{
				throw new InvalidOperationException($"Service {serviceType.FullName} does not have a {nameof(JsonRpcServiceAttribute)} or a route prefix is not defined");
			}
			server.AddWebSocketService(routePrefix, initializer);
		}
	}
}
