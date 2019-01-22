using System;
using System.Collections;
using System.Collections.Generic;

public static class TEvent<Type>
{
    private static Dictionary<Type, Delegate> dic = new Dictionary<Type, Delegate>();

    public static void Add(Type type, Action method)
    {
        lock (dic)
        {
            if (!dic.ContainsKey(type))
            {
                dic.Add(type, null);
            }
            dic[type] = (Action)dic[type] + method;
        }
    }

    public static void Remove(Type type, Action method)
    {
        lock (dic)
        {
            if (dic.ContainsKey(type))
            {
                dic[type] = (Action)dic[type] - method;

                if (dic[type] == null)
                {
                    dic.Remove(type);
                }
            }
        }
    }

    public static void Execute(Type eventType)
    {
        Delegate del;
        if (dic.TryGetValue(eventType, out del))
        {
            Action method = (Action)del;

            if (method != null)
            {
                method();
            }
        }
    }
}

public static class TEvent<Type, Arg0>
{
    private static Dictionary<Type, Delegate> dic = new Dictionary<Type, Delegate>();

    public static void Add(Type type, Action<Arg0> method)
    {
        lock (dic)
        {
            if (!dic.ContainsKey(type))
            {
                dic.Add(type, null);
            }

            dic[type] = (Action<Arg0>)dic[type] + method;
        }
    }

    public static void Remove(Type type, Action<Arg0> method)
    {
        lock (dic)
        {
            if (dic.ContainsKey(type))
            {
                dic[type] = (Action<Arg0>)dic[type] - method;

                if (dic[type] == null)
                {
                    dic.Remove(type);
                }
            }
        }
    }

    public static void Execute(Type type, Arg0 argument)
    {
        Delegate del;
        if (dic.TryGetValue(type, out del))
        {
            Action<Arg0> method = (Action<Arg0>)del;

            if (method != null)
            {
                method(argument);
            }
        }
    }
}

public static class TEvent<Type, Arg0, Arg1>
{
    private static Dictionary<Type, Delegate> dic = new Dictionary<Type, Delegate>();

    public static void Add(Type type, Action<Arg0, Arg1> method)
    {
        lock (dic)
        {
            if (!dic.ContainsKey(type))
            {
                dic.Add(type, null);
            }

            dic[type] = (Action<Arg0, Arg1>)dic[type] + method;
        }
    }

    public static void Remove(Type type, Action<Arg0, Arg1> method)
    {
        lock (dic)
        {
            if (dic.ContainsKey(type))
            {
                dic[type] = (Action<Arg0, Arg1>)dic[type] - method;

                if (dic[type] == null)
                {
                    dic.Remove(type);
                }
            }
        }
    }

    public static void Execute(Type type, Arg0 arg0, Arg1 arg1)
    {
        Delegate del;

        if (dic.TryGetValue(type, out del))
        {
            Action<Arg0, Arg1> method = (Action<Arg0, Arg1>)del;

            if (method != null)
            {
                method(arg0, arg1);
            }
        }
    }
}