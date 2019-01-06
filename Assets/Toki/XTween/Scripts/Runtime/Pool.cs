/**********************************************************************************
/*		File Name 		: PoolClass
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2019-1-5
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using UnityEngineInternal;
using System;
using System.Collections;
using System.Collections.Generic;
using Toki.Tween;

namespace Toki
{
	public class Pool<T> where T : IDisposable, new()
	{
		/************************************************************************
		*	 	 	 	 	Static Variable Declaration	 	 	 	 	 	    *
		************************************************************************/
		private static Pool<T> _instance;
		
		/************************************************************************
		*	 	 	 	 	Static Method Declaration	 	 	 	     	 	*
		************************************************************************/
		public static Pool<T> Instance
		{
			get
			{
				if( _instance == null )
				{
					_instance = new Pool<T>();
				}
				return _instance;
			}
		}

		public static void Initialize( int size )
		{
			Pool<T>.Instance.InitialCreate(size);
		}

		public static T Pop()
		{
			return Pool<T>.Instance.PopInstance();
		}

		public static bool Contains(T target)
		{
			return Pool<T>.Instance.ContainsInstance(target);
		}

		public static void Push( T target )
		{
			Pool<T>.Instance.PushInstance(target);
		}

		/************************************************************************
		*	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
		************************************************************************/
		private Stack<T> _stack = new Stack<T>();
		
		/************************************************************************
		*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
		************************************************************************/
			
		/************************************************************************
		*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
		************************************************************************/
			
		/************************************************************************
		*	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
		************************************************************************/
		
		/************************************************************************
		*	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
		************************************************************************/
		
		/************************************************************************
		*	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
		************************************************************************/

		/************************************************************************
		*	 	 	 	 	Coroutine Declaration	 	  			 	 		*
		************************************************************************/
		
		/************************************************************************
		*	 	 	 	 	Private Method Declaration	 	 	 	 	 		*
		************************************************************************/
		
		/************************************************************************
		*	 	 	 	 	Protected Method Declaration	 	 	 	 	 	*
		************************************************************************/
		
		/************************************************************************
		*	 	 	 	 	Public Method Declaration	 	 	 	 	 		*
		************************************************************************/
		// private int _count = 0;
		public void InitialCreate( int size )
		{
			while( this._stack.Count < size )
			{
				T instance = new T();
				this._stack.Push(instance);
			}
		}

		public T PopInstance()
		{
			T instance;
			if( this._stack.Count > 0 )
			{
				instance = this._stack.Pop();
			}
			else
			{
				// _count++;
				instance = new T();
			}
			// Debug.Log(typeof(T) + " - Create Count: " + this._stack.Count);
			return instance;
		}

		public void PushInstance(T target)
		{
			target.Dispose();
			this._stack.Push(target);
			// _count--;
			// Debug.Log(target.GetType() + " - Dispose: " + _count );
		}

		public bool ContainsInstance(T target)
		{
			return this._stack.Contains(target);
		}
	}

	public static class PoolShorcut
	{
		public static void PoolPush<T>(this T target) where T : IDisposable, new()
		{
			Pool<T>.Push(target);
		}

		public static bool PoolContains<T>(this T target) where T : IDisposable, new()
		{
			return Pool<T>.Contains(target);
		}
	}
}