using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public interface IExecutable
{
	void Execute();
}

public struct Executor : IExecutable
{
	public Action listener;
	
	public Executor( Action listener )
	{
		this.listener = listener;
	}
	
	public void Execute()
	{
		this.listener.Invoke();
	}

    public static Executor New( Action listener )
    {
        return new Executor( listener );
    }
}
	
public struct Executor<T> : IExecutable
{
	public Action<T> listener;
	public T parameter;
		
	public Executor( Action<T> listener, T param )
	{
		this.listener = listener;
		this.parameter = param;
	}
		
	public void Execute()
	{
		this.listener.Invoke( this.parameter );
	}

    public static Executor<T> New(Action<T> listener, T param)
    {
        return new Executor<T>(listener, param);
    }
}
	
public struct Executor<T0,T1> : IExecutable
{
	public Action<T0,T1> listener;
	public T0 parameter0;
	public T1 parameter1;
		
	public Executor( Action<T0,T1> listener, T0 param0, T1 param1 )
	{
		this.listener = listener;
		this.parameter0 = param0;
		this.parameter1 = param1;
	}
		
	public void Execute()
	{
		this.listener.Invoke( this.parameter0, this.parameter1 );
	}

    public static Executor<T0,T1> New(Action<T0,T1> listener, T0 param0, T1 param1)
    {
        return new Executor<T0,T1>(listener, param0, param1);
    }
}

public struct Executor<T0,T1,T2> : IExecutable
{
	public Action<T0,T1,T2> listener;
	public T0 parameter0;
	public T1 parameter1;
	public T2 parameter2;
		
	public Executor( Action<T0,T1,T2> listener, T0 param0, T1 param1, T2 param2 )
	{
		this.listener = listener;
		this.parameter0 = param0;
		this.parameter1 = param1;
		this.parameter2 = param2;
	}
		
	public void Execute()
	{
		this.listener.Invoke( this.parameter0, this.parameter1, this.parameter2 );
	}

    public static Executor<T0,T1,T2> New(Action<T0,T1,T2> listener, T0 param0, T1 param1, T2 param2)
    {
        return new Executor<T0,T1,T2>(listener, param0, param1, param2);
    }
}