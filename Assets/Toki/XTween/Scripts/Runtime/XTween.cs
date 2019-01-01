using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Toki.Tween;

public class XTween
{
	private static UpdateTicker _ticker;
	private static UpdateTickerReal _tickerReal;
	private static UpdaterFactory _updaterFactory;

	public static float realDeltaTime
	{
		get
		{
			return _tickerReal.GetDeltaTime(0);
		}
	}
	// frameSkip Max == 4
	public static float DeltaTimeWithFrameSkip( int frameSkip )
	{
		return _ticker.GetDeltaTime( frameSkip );
	}
	// frameSkip Max == 4
	public static float DeltaTimeRealWithFrameSkip( int frameSkip )
	{
		return _tickerReal.GetDeltaTime( frameSkip );
	}

	public static T[] GetArrayFromCollection<T>( System.Collections.ICollection list )
	{
		if( list == null )
        {
            return null;
        }
		T[] arr = new T[list.Count];
		list.CopyTo( arr, 0 );
		return arr;
	}

	public static ITimer GetTicker( bool isReal )
	{
		if( isReal )
		{
			return _tickerReal;
		}
		else
		{
			return _ticker;
		}
	}

	static XTween()
	{
#if UNITY_EDITOR
		if( Application.isPlaying )
		{
			_ticker = UpdateTicker.To;
			_tickerReal = UpdateTickerReal.To;
		}
		else
		{
			_ticker = new UpdateTicker();
			_tickerReal = new UpdateTickerReal();
		}
#else
		_ticker = UpdateTicker.To;
		_tickerReal = UpdateTickerReal.To;
#endif
		_ticker.Initialize();
		_tickerReal.Initialize();
		
		_updaterFactory = new UpdaterFactory();
	}

#if UNITY_EDITOR
	public static void PlayModeChanged( bool isEnterEditorMode )
	{
		if( isEnterEditorMode )
		{
			_ticker.AddUpdate();
			_tickerReal.AddUpdate();
		}
		else
		{
			_ticker.RemoveUpdate();
			_tickerReal.RemoveUpdate();
		}
	}
#endif

	/*===================================== Transform ========================================*/
    public static IAni To( GameObject target, XHash hash, float time = 1.0f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		ObjectTween tween = new ObjectTween( realTime ? (ITimer)_tickerReal : _ticker );
		tween.FrameSkip = frameSkip;
		tween.updater = _updaterFactory.Create(target, hash, hash.GetStart());
		tween.ClassicHandlers = hash;
		tween.time = time;
		tween.easing = (easing != null) ? easing : Linear.easeNone;
		return tween;
    }

    /*===================================== Value ========================================*/
	public static IAni ValueTo( XObjectHash source, Action<XObjectHash> UpdateHandler, float time = 1.0f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		ObjectTween tween = new ObjectTween( realTime ? (ITimer)_tickerReal : _ticker );
		ObjectUpdater updater = (ObjectUpdater)_updaterFactory.Create( source );
		updater.UpdateHandler = UpdateHandler;
		tween.FrameSkip = frameSkip;
		tween.updater = updater;
		tween.ClassicHandlers = source;
		tween.time = time;
		tween.easing = ( easing != null ) ? easing : Linear.easeNone;
		return tween;
	}

	//Proerpty Type Only
	public static IAni ValueTo<T>( T target, XObjectHash hash, float time = 1.0f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		ObjectTween tween = new ObjectTween( realTime ? (ITimer)_tickerReal : _ticker );
		ObjectUpdater<T> updater = (ObjectUpdater<T>)UpdaterFactory.Create<T>( target, hash );
		tween.FrameSkip = frameSkip;
		tween.updater = updater;
		tween.ClassicHandlers = hash;
		tween.time = time;
		tween.easing = ( easing != null ) ? easing : Linear.easeNone;
		return tween;
    }

	/*===================================== Color ========================================*/
	//Sprite
	public static IAni ColorTo( SpriteRenderer target, XColorHash hash, float time = 1.0f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return ColorTo<SpriteRenderer>(target, "color", hash, time, easing, frameSkip, realTime);
	}

	//UI
	public static IAni ColorTo( Graphic target, XColorHash hash, float time = 1.0f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return ColorTo<Graphic>(target, "color", hash, time, easing, frameSkip, realTime);
	}

	//Color Property
	public static IAni ColorTo<T>( T target, string colorPropertyName, XColorHash hash, float time = 1.0f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		ITimer tick = _ticker;
		ITimer tickReal = _tickerReal; 
		ObjectTween tween = new ObjectTween( realTime ? tickReal : tick );
		ColorUpdater<T> updater = (ColorUpdater<T>)UpdaterFactory.Create<T>(target, colorPropertyName, hash, hash.GetStart() );
		tween.FrameSkip = frameSkip;
		tween.updater = updater;
		tween.ClassicHandlers = hash;
		tween.time = time;
		tween.easing = ( easing != null ) ? easing : Linear.easeNone;
		return tween;
    }

	/*===================================== Continue ========================================*/
	public static ContinousTween Continous( GameObject target, XHash hash, float time = 1.0f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		ITimer tick = _ticker;
		ITimer tickReal = _tickerReal; 
		ContinousTween tween = new ContinousTween( realTime ? tickReal : tick );
		tween.FrameSkip = frameSkip;
		tween.time = time;
		tween.easing = (easing != null) ? easing : Linear.easeNone;
		tween.updater = _updaterFactory.CreateContinous(target, hash, hash.GetStart());
		tween.ClassicHandlers = hash;
		return tween;
	}

	/*===================================== Apply ========================================*/
    public static void Apply( GameObject target, XHash hash, float time = 1.0f, float applyTime = 1.0f, IEasing easing = null, bool realTime = false)
	{
		ITimer tick = _ticker;
		ITimer tickReal = _tickerReal; 
		ObjectTween tween = new ObjectTween( realTime ? tickReal : tick );
		tween.FrameSkip = 0;
		tween.updater = _updaterFactory.Create( target, hash, hash.GetStart() );
		tween.time = time;
		tween.easing = ( easing != null ) ? easing : Linear.easeNone;
		tween.ResolveValues();
		tween.UpdateTween( applyTime );
    }

    /*===================================== ParallelTweens ========================================*/
    public static IAniGroup ParallelTweens( List<IAni> tweenList, bool realTime = false )
    {
        ITimer tick = _ticker;
        ITimer tickReal = _tickerReal;
        ParallelTween tween = new ParallelTween(tweenList.ToArray(), realTime ? tickReal : tick, 0);
        tween.FrameSkip = 0;
        return tween;
    }

    public static IAniGroup ParallelTweens( bool realTime = false, params IAni[] tweens )
	{
		ITimer tick = _ticker;
		ITimer tickReal = _tickerReal; 
        ParallelTween tween = new ParallelTween(tweens, realTime ? tickReal : tick, 0);
        tween.FrameSkip = 0;
        return tween;
	}

    /*===================================== SerialTweens ========================================*/
    public static IAniGroup SerialTweens( List<IAni> tweenList, bool realTime = false )
    {
        ITimer tick = _ticker;
        ITimer tickReal = _tickerReal;
        SerialTween tween = new SerialTween(tweenList.ToArray(), realTime ? tickReal : tick, 0);
        tween.FrameSkip = 0;
        return tween;
    }


    public static IAniGroup SerialTweens( bool realTime = false, params IAni[] tweens )
	{
		ITimer tick = _ticker;
		ITimer tickReal = _tickerReal;
        SerialTween tween = new SerialTween(tweens, realTime ? tickReal : tick, 0);
        tween.FrameSkip = 0;
		return tween;
	}

    /*===================================== Reverse ========================================*/
	public static IAni Reverse( IAni tween, bool reversePosition = true)
	{
		IAni newTween;
		float pos = reversePosition ? tween.Duration - tween.Position : 0.0f;
		if (tween is ReversedTween) {
			newTween = new TweenDecorator((tween as ReversedTween).baseTween, pos);
			newTween.FrameSkip = tween.FrameSkip;
			return newTween;
		}
		if (tween is TweenDecorator) {
			newTween = (tween as TweenDecorator).baseTween;
			newTween.FrameSkip = tween.FrameSkip;
		}
		newTween = new ReversedTween(tween as IIAni, pos);
		newTween.FrameSkip = tween.FrameSkip;
		return newTween;
	}

    /*===================================== Repeat ========================================*/
	public static IAni Repeat( IAni tween, int repeatCount )
	{
		IAni newTween = new RepeatedTween( (IIAni)tween, repeatCount );
		newTween.FrameSkip = tween.FrameSkip;
		return newTween;
	}

    /*===================================== Scale ========================================*/
	public static IAni Scale( IAni tween, float scale )
	{
		IAni newTween = new ScaledTween( tween as IIAni, scale );
		newTween.FrameSkip = tween.FrameSkip;
		return newTween;
	}

    /*===================================== Slice ========================================*/
	public static IAni Slice( IAni tween, float begin, float end, bool isPercent = false)
	{
		IAni newTween;
		if (isPercent) {
			begin = tween.Duration * begin;
			end = tween.Duration * end;
		}
		if (begin > end) {
			newTween = new ReversedTween(new SlicedTween(tween as IIAni, end, begin), 0);
			newTween.FrameSkip = tween.FrameSkip;
			return newTween;
		}
		newTween = new SlicedTween(tween as IIAni, begin, end);
		newTween.FrameSkip = tween.FrameSkip;
		return newTween;
	}

    /*===================================== Slice ========================================*/
	public static IAni Delay( IAni tween, float delay, float postDelay = 0.0f )
	{
		IAni newTween = new DelayedTween( tween as IIAni, delay, postDelay );
		newTween.FrameSkip = tween.FrameSkip;
		return newTween;
	}
}