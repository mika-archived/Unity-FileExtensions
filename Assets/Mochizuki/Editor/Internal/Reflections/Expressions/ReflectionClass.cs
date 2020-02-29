using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using UnityEngine;

namespace Mochizuki.Editor.Internal.Reflections.Expressions
{
    public class ReflectionClass
    {
        private static readonly Hashtable Caches = new Hashtable();
        private readonly object _instance;
        private readonly Type _type;

        protected ReflectionClass(object instance, Type type)
        {
            _instance = instance;
            _type = type;

            if (Caches[_type] == null)
                Caches[_type] = new Cache();
        }

        protected TResult InvokeMethod<TResult>(string name, BindingFlags flags, params object[] parameters)
        {
            var methods = ((Cache) Caches[_type]).Methods;
            Func<object, object[], object> cache;
            methods.TryGetValue(name, out cache);

            if (cache != null)
                return (TResult) cache.Invoke(_instance, parameters);

            var mi = _instance.GetType().GetMethod(name, flags);
            if (mi == null)
                throw new InvalidOperationException(string.Format("Method {0} is not found in this instance", name));

            methods.Add(name, CreateMethodAccessor(mi));
            return (TResult) methods[name].Invoke(_instance, parameters);
        }

        protected TResult InvokeMember<TResult>(string name)
        {
            var members = ((Cache) Caches[_type]).Members;
            Func<object, object> cache;
            members.TryGetValue(name, out cache);

            if (cache != null)
                return (TResult) cache.Invoke(_instance);

            members.Add(name, CreateMemberAccessor(name));
            return (TResult) members[name].Invoke(_instance);
        }

        private Func<object, object[], object> CreateMethodAccessor(MethodInfo mi)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var args = Expression.Parameter(typeof(object[]), "args");
            var body = mi.GetParameters().Length == 0
                ? Expression.Call(Expression.Convert(instance, _type), mi)
                : Expression.Call(Expression.Convert(instance, _type), mi, mi.GetParameters().Select((w, i) => Expression.Convert(Expression.ArrayIndex(args, Expression.Constant(i)), w.ParameterType)).Cast<Expression>().ToArray());

            Debug.Log(string.Format("Method Accessor is created for {0} in {1}", mi.Name, _type.FullName));

            return Expression.Lambda<Func<object, object[], object>>(Expression.Convert(body, typeof(object)), instance, args).Compile();
        }

        private Func<object, object> CreateMemberAccessor(string name)
        {
            try
            {
                var instance = Expression.Parameter(typeof(object), "instance");
                var body = Expression.PropertyOrField(Expression.Convert(instance, _type), name);

                Debug.Log(string.Format("Member Accessor is created for {0} in {1}", name, _type.FullName));

                return Expression.Lambda<Func<object, object>>(Expression.Convert(body, typeof(object)), instance).Compile();
            }
            catch
            {
                throw new InvalidOperationException(string.Format("Member '{0}' is not found in this class", name));
            }
        }

        private class Cache
        {
            public readonly Dictionary<string, Func<object, object>> Members = new Dictionary<string, Func<object, object>>();
            public readonly Dictionary<string, Func<object, object[], object>> Methods = new Dictionary<string, Func<object, object[], object>>();
        }
    }
}