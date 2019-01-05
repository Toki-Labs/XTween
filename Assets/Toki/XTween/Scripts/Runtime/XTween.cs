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

	public static float RealDeltaTime
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
	//Transform
	public static IXTween To( GameObject target, XHash hash, float time, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		ObjectTween tween = new ObjectTween( realTime ? (ITimer)_tickerReal : _ticker );
		tween.FrameSkip = frameSkip;
		tween.Updater = UpdaterFactory.Create(target, hash, hash.GetStart());
		tween.ClassicHandlers = hash;
		tween.Time = time;
		tween.Easing = (easing != null) ? easing : Linear.easeNone;
		return tween;
    }

    /*===================================== Value ========================================*/
	//Value - Single
	public static IXTween ToValue( Action<float> setter, float start, float end, float time, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return ToValueBezier(setter, start, end, null, time, easing, frameSkip, realTime);
	}

	//Value - Single Bezier
	public static IXTween ToValueBezier( Action<float> setter, float start, float end, float[] controlPoints, float time, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		ObjectTween tween = new ObjectTween( realTime ? (ITimer)_tickerReal : _ticker );
		GetSetUpdater updater = UpdaterFactory.Create( setter, start, end, controlPoints );
		tween.FrameSkip = frameSkip;
		tween.Updater = updater;
		tween.ClassicHandlers = new XEventHash();
		tween.Time = time;
		tween.Easing = ( easing != null ) ? easing : Linear.easeNone;
		return tween;
	}

	//Value - Multi, Bezier
	public static IXTween ToValueMuli( XObjectHash source, Action<XObjectHash> UpdateHandler, float time, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		ObjectTween tween = new ObjectTween( realTime ? (ITimer)_tickerReal : _ticker );
		ObjectUpdater updater = UpdaterFactory.Create( source );
		updater.UpdateHandler = UpdateHandler;
		tween.FrameSkip = frameSkip;
		tween.Updater = updater;
		tween.ClassicHandlers = source;
		tween.Time = time;
		tween.Easing = ( easing != null ) ? easing : Linear.easeNone;
		return tween;
	}

	//Property - Single
	public static IXTween ToProperty<T>( T target, string propertyName, float end, float time, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return ToPropertyMulti<T>(target, XObjectHash.New.Add(propertyName, end), time, easing, frameSkip, realTime);
	}

	//Property - Single
	public static IXTween ToProperty<T>( T target, string propertyName, float start, float end, float time, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return ToPropertyMulti<T>(target, XObjectHash.New.Add(propertyName, start, end), time, easing, frameSkip, realTime);
	}

	//Property - Single Bezier
	public static IXTween ToPropertyBezier<T>( T target, string propertyName, float start, float end, float[] controlPoints, float time, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return ToPropertyMulti<T>(target, XObjectHash.New.Add(propertyName, start, end).AddControlPoint(propertyName, controlPoints), time, easing, frameSkip, realTime);
	}

	//Proerpty - Multi, Bezier
	public static IXTween ToPropertyMulti<T>( T target, XObjectHash hash, float time, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		ObjectTween tween = new ObjectTween( realTime ? (ITimer)_tickerReal : _ticker );
		ObjectUpdater<T> updater = (ObjectUpdater<T>)UpdaterFactory.Create<T>( target, hash );
		tween.FrameSkip = frameSkip;
		tween.Updater = updater;
		tween.ClassicHandlers = hash;
		tween.Time = time;
		tween.Easing = ( easing != null ) ? easing : Linear.easeNone;
		return tween;
    }

	/*===================================== Color ========================================*/
	//Sprite
	public static IXTween ToColor( SpriteRenderer target, XColorHash hash, float time, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return ToColor<SpriteRenderer>(target, "color", hash, time, easing, frameSkip, realTime);
	}

	//UI
	public static IXTween ToColor( Graphic target, XColorHash hash, float time, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return ToColor<Graphic>(target, "color", hash, time, easing, frameSkip, realTime);
	}

	//Color Property
	public static IXTween ToColor<T>( T target, string colorPropertyName, XColorHash hash, float time = 1.0f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		ITimer tick = _ticker;
		ITimer tickReal = _tickerReal; 
		ObjectTween tween = new ObjectTween( realTime ? tickReal : tick );
		ColorUpdater<T> updater = UpdaterFactory.Create<T>(target, colorPropertyName, hash, hash.GetStart() );
		tween.FrameSkip = frameSkip;
		tween.Updater = updater;
		tween.ClassicHandlers = hash;
		tween.Time = time;
		tween.Easing = ( easing != null ) ? easing : Linear.easeNone;
		return tween;
    }

	/*===================================== Continue ========================================*/
	public static ContinousTween Continous( GameObject target, XHash hash, float time = 1.0f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		ITimer tick = _ticker;
		ITimer tickReal = _tickerReal; 
		ContinousTween tween = new ContinousTween( realTime ? tickReal : tick );
		tween.FrameSkip = frameSkip;
		tween.Time = time;
		tween.Easing = (easing != null) ? easing : Linear.easeNone;
		tween.Updater = _updaterFactory.CreateContinous(target, hash, hash.GetStart());
		tween.ClassicHandlers = hash;
		return tween;
	}

	/*===================================== ParallelTweens ========================================*/
    public static IXTweenGroup ParallelTweens( bool realTime = false, params IXTween[] tweens )
	{
		ITimer tick = _ticker;
		ITimer tickReal = _tickerReal; 
        ParallelTween tween = new ParallelTween(tweens, realTime ? tickReal : tick, 0);
        tween.FrameSkip = 0;
        return tween;
	}

    /*===================================== SerialTweens ========================================*/
    public static IXTweenGroup SerialTweens( bool realTime = false, params IXTween[] tweens )
	{
		ITimer tick = _ticker;
		ITimer tickReal = _tickerReal;
        SerialTween tween = new SerialTween(tweens, realTime ? tickReal : tick, 0);
        tween.FrameSkip = 0;
		return tween;
	}

    /*===================================== Reverse ========================================*/
	public static IXTween Reverse( IXTween tween, bool reversePosition = true)
	{
		IXTween newTween;
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
		newTween = new ReversedTween(tween as IIXTween, pos);
		newTween.FrameSkip = tween.FrameSkip;
		return newTween;
	}

    /*===================================== Repeat ========================================*/
	public static IXTween Repeat( IXTween tween, int repeatCount )
	{
		IXTween newTween = new RepeatedTween( (IIXTween)tween, repeatCount );
		newTween.FrameSkip = tween.FrameSkip;
		return newTween;
	}

    /*===================================== Scale ========================================*/
	public static IXTween Scale( IXTween tween, float scale )
	{
		IXTween newTween = new ScaledTween( tween as IIXTween, scale );
		newTween.FrameSkip = tween.FrameSkip;
		return newTween;
	}

    /*===================================== Slice ========================================*/
	public static IXTween Slice( IXTween tween, float begin, float end, bool isPercent = false)
	{
		IXTween newTween;
		if (isPercent) {
			begin = tween.Duration * begin;
			end = tween.Duration * end;
		}
		if (begin > end) {
			newTween = new ReversedTween(new SlicedTween(tween as IIXTween, end, begin), 0);
			newTween.FrameSkip = tween.FrameSkip;
			return newTween;
		}
		newTween = new SlicedTween(tween as IIXTween, begin, end);
		newTween.FrameSkip = tween.FrameSkip;
		return newTween;
	}

    /*===================================== Slice ========================================*/
	public static IXTween Delay( IXTween tween, float delay, float postDelay = 0.0f )
	{
		IXTween newTween = new DelayedTween( tween as IIXTween, delay, postDelay );
		newTween.FrameSkip = tween.FrameSkip;
		return newTween;
	}
}

public static class XTweenShorcutExtensions
{
	//Poistion -Trasform
	public static IXTween To(this Transform trans, XHash hash, float time = 1f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return XTween.To(trans.gameObject, hash, time, easing, frameSkip, realTime);
	}

	//Position - GameObject
	public static IXTween To(this GameObject gameObject, XHash hash, float time = 1f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return XTween.To(gameObject, hash, time, easing, frameSkip, realTime);
	}

	//Value - setter
	public static IXTween ToValue(this object obj, Action<float> setter, float start, float end, float time = 1f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return XTween.ToValueBezier(setter, start, end, null, time, easing, frameSkip, realTime);
	}

	//Value - setter Bezier
	public static IXTween ToValueBezier(this object obj, Action<float> setter, float start, float end, float[] controlPoints, float time = 1f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return XTween.ToValueBezier(setter, start, end, controlPoints, time, easing, frameSkip, realTime);
	}

	//Value - Multi value
	public static IXTween ToValueMulti(this object obj, XObjectHash source, Action<XObjectHash> UpdateHandler, float time = 1f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return XTween.ToValueMuli(source, UpdateHandler, time, easing, frameSkip, realTime);
	}

	//Property - Property Type
	public static IXTween ToProperty<T>(this T target, string propertyName, float end, float time = 1f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return XTween.ToProperty<T>(target, propertyName, end, time, easing, frameSkip, realTime);
	}

	//Property - Property Type
	public static IXTween ToProperty<T>(this T target, string propertyName, float start, float end, float time = 1f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return XTween.ToProperty<T>(target, propertyName, start, end, time, easing, frameSkip, realTime);
	}

	//Property - Property Bezier
	public static IXTween ToPropertyBezier<T>(this T target, string propertyName, float start, float end, float[] controlPoints, float time = 1f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return XTween.ToPropertyBezier<T>(target, propertyName, start, end, controlPoints, time, easing, frameSkip, realTime);
	}

	//Property - Multi property
	public static IXTween ToPropertyMulti<T>(this T target, XObjectHash hash, float time = 1f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return XTween.ToPropertyMulti<T>(target, hash, time, easing, frameSkip, realTime);
	}

	//Color - Sprite
	public static IXTween ToColor(this SpriteRenderer target, XColorHash hash, float time = 1f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return XTween.ToColor<SpriteRenderer>(target, "color", hash, time, easing, frameSkip, realTime);
	}

	//Color - UI
	public static IXTween ToColor(this Graphic target, XColorHash hash, float time = 1f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return XTween.ToColor<Graphic>(target, "color", hash, time, easing, frameSkip, realTime);
	}

	//Color - Property
	public static IXTween ToColor<T>(this T target, string colorPropertyName, XColorHash hash, float time = 1f, IEasing easing = null, uint frameSkip = 0, bool realTime = false )
	{
		return XTween.ToColor<T>(target, colorPropertyName, hash, time, easing, frameSkip, realTime);
	}
}
