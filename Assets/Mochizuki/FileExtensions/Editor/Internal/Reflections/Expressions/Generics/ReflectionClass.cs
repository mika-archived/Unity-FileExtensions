/*-------------------------------------------------------------------------------------------
 * Copyright (c) Fuyuno Mikazuki / Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using UnityEngine;

namespace Mochizuki.FileExtensions.Editor.Internal.Reflections.Expressions.Generics
{
    public class ReflectionClass<T> where T : class
    {
        protected T Instance { get; private set; }

        protected ReflectionClass(T instance)
        {
            Instance = instance;
        }

        protected TResult InvokeMethod<TResult>(string name, BindingFlags bindingFlags, params object[] parameters)
        {
            var methods = Cache<T>.Methods;
            Func<T, object[], object> cache;
            methods.TryGetValue(name, out cache);

            if (cache != null)
                return (TResult) cache.Invoke(Instance, parameters);

            var mi = Instance.GetType().GetMethod(name, bindingFlags);
            if (mi == null)
                throw new InvalidOperationException(string.Format("Method '{0}' is not found in this class", name));

            methods.Add(name, CreateMethodAccessor(mi));
            return (TResult) methods[name].Invoke(Instance, parameters);
        }

        protected TResult InvokeMember<TResult>(string name)
        {
            var members = Cache<T>.Members;
            Func<T, object> cache;
            Cache<T>.Members.TryGetValue(name, out cache);

            if (cache != null)
                return (TResult) cache.Invoke(Instance);

            members.Add(name, CreateMemberAccessor(name));
            return (TResult) members[name].Invoke(Instance);
        }

        private static Func<T, object[], object> CreateMethodAccessor(MethodInfo mi)
        {
            var instance = Expression.Parameter(typeof(T), "instance");
            var args = Expression.Parameter(typeof(object[]), "args");
            var body = mi.GetParameters().Length == 0
                ? Expression.Call(instance, mi)
                : Expression.Call(instance, mi, mi.GetParameters().Select((w, i) => Expression.Convert(Expression.ArrayIndex(args, Expression.Constant(i)), w.ParameterType)).Cast<Expression>().ToArray());

            Debug.Log(string.Format("Method Accessor is created for {0} in {1}", mi.Name, typeof(T).FullName));

            return Expression.Lambda<Func<T, object[], object>>(Expression.Convert(body, typeof(object)), instance, args).Compile();
        }

        private static Func<T, object> CreateMemberAccessor(string name)
        {
            try
            {
                var instance = Expression.Parameter(typeof(T), "instance");
                var body = Expression.PropertyOrField(instance, name);

                Debug.Log(string.Format("Member Accessor is created for {0} in {1}", name, typeof(T).FullName));

                return Expression.Lambda<Func<T, object>>(Expression.Convert(body, typeof(object)), instance).Compile();
            }
            catch
            {
                throw new InvalidOperationException(string.Format("Member '{0}' is not found in this class", name));
            }
        }

        private static class Cache<TCache> where TCache : class
        {
            public static readonly Dictionary<string, Func<TCache, object[], object>> Methods = new Dictionary<string, Func<TCache, object[], object>>();
            public static readonly Dictionary<string, Func<TCache, object>> Members = new Dictionary<string, Func<TCache, object>>();
        }
    }
}