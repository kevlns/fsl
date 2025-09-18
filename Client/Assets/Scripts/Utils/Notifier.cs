using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSL
{
    /// <summary>
    /// 事件管理器（观察者模式实现，支持多参数）
    /// </summary>
    public static class Notifier
    {
        // 封装监听器
        private class ListenerInfo
        {
            public Delegate OriginalDelegate { get; }
            public Action<object[]> Invoker { get; }
            public Type[] ParamTypes { get; }
            public object Target => OriginalDelegate.Target;

            public ListenerInfo(Delegate del, Action<object[]> invoker, Type[] paramTypes)
            {
                OriginalDelegate = del;
                Invoker = invoker;
                ParamTypes = paramTypes;
            }
        }

        // 使用 object 作为键，支持任意枚举类型
        private static readonly Dictionary<object, List<ListenerInfo>> _eventListeners =
            new Dictionary<object, List<ListenerInfo>>();

        private static readonly Type[] EmptyTypes = Array.Empty<Type>();

        // ======================
        // 注册监听器
        // ======================
        public static void Listen(Enum eventType, Action callback)
        {
            RegisterListener(eventType, callback, EmptyTypes, args => callback());
        }

        public static void Listen<T1>(Enum eventType, Action<T1> callback)
        {
            RegisterListener(eventType, callback, new[] { typeof(T1) },
                args => callback((T1)args[0]));
        }

        public static void Listen<T1, T2>(Enum eventType, Action<T1, T2> callback)
        {
            RegisterListener(eventType, callback, new[] { typeof(T1), typeof(T2) },
                args => callback((T1)args[0], (T2)args[1]));
        }

        public static void Listen<T1, T2, T3>(Enum eventType, Action<T1, T2, T3> callback)
        {
            RegisterListener(eventType, callback, new[] { typeof(T1), typeof(T2), typeof(T3) },
                args => callback((T1)args[0], (T2)args[1], (T3)args[2]));
        }

        // ======================
        // 触发事件
        // ======================
        public static void Trigger(Enum eventType)
        {
            TriggerInternal(eventType, Array.Empty<object>());
        }

        public static void Trigger<T1>(Enum eventType, T1 arg1)
        {
            TriggerInternal(eventType, new object[] { arg1 });
        }

        public static void Trigger<T1, T2>(Enum eventType, T1 arg1, T2 arg2)
        {
            TriggerInternal(eventType, new object[] { arg1, arg2 });
        }

        public static void Trigger<T1, T2, T3>(Enum eventType, T1 arg1, T2 arg2, T3 arg3)
        {
            TriggerInternal(eventType, new object[] { arg1, arg2, arg3 });
        }

        // ======================
        // 移除监听器
        // ======================
        public static void Unlisten(Enum eventType, Delegate callback)
        {
            if (_eventListeners.TryGetValue(eventType, out var listeners))
            {
                listeners.RemoveAll(info =>
                    info.OriginalDelegate.Target == callback.Target &&
                    info.OriginalDelegate.Method == callback.Method);
            }
        }

        // 移除某个对象所有监听器
        public static void MarkListenersInvalid(object target)
        {
            foreach (var kvp in _eventListeners)
            {
                kvp.Value.RemoveAll(info => info.Target == target);
            }
        }

        // 清除所有监听器
        public static void Clear()
        {
            _eventListeners.Clear();
        }

        // ======================
        // 内部核心
        // ======================
        private static void RegisterListener(Enum eventType, Delegate original, Type[] paramTypes,
            Action<object[]> invoker)
        {
            if (!_eventListeners.ContainsKey(eventType))
            {
                _eventListeners[eventType] = new List<ListenerInfo>();
            }

            _eventListeners[eventType].Add(new ListenerInfo(original, invoker, paramTypes));
        }

        private static void TriggerInternal(Enum eventType, object[] args)
        {
            if (!_eventListeners.TryGetValue(eventType, out var listeners)) return;

            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                var info = listeners[i];

                // 参数数量检查
                if (info.ParamTypes.Length != args.Length)
                {
                    continue;
                }

                // 参数类型检查
                bool mismatch = false;
                for (int j = 0; j < args.Length; j++)
                {
                    var arg = args[j];
                    if (arg != null)
                    {
                        var argType = arg.GetType();
                        if (!info.ParamTypes[j].IsAssignableFrom(argType))
                        {
                            mismatch = true;
                            break;
                        }
                    }
                }

                if (mismatch)
                {
                    continue;
                }

                try
                {
                    info.Invoker(args);
                }
                catch (Exception e)
                {
                    Debug.LogError($"事件 {eventType} 触发异常: {e}");
                }
            }
        }
    }
}