using UnityEngine;
using System;
using System.Collections;
using Toki.Tween;

namespace Toki.Tween
{
    public class WaitForTweenPlay : IEnumerator, ITimer, IDisposable
    {
        /************************************************************************
        *	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
        ************************************************************************/
        protected AbstractTween _tween;
        protected ITimer _ticker;

        /************************************************************************
        *	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
        ************************************************************************/

        /************************************************************************
        *	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
        ************************************************************************/

        /************************************************************************
        *	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
        ************************************************************************/
        public float Time
        {
            get
            {
                return _ticker.Time;
            }
            set
            {
                _ticker.Time = value;
            }
        }

        public ITimer Ticker { get{return _ticker;} }
        public object Current { get{return null;} }

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
        public void Initialize(ITimer ticker, AbstractTween tween)
        {
            _ticker = ticker;
            _tween = tween;
        }
        
        public bool MoveNext()
        {
            bool isDone = false;
            if( _tween.IsPlaying )
            {
                isDone = _tween.Tick(_ticker.Time);
            }

            if( _tween == null )
                isDone = true;
            else 
                if( isDone ) _tween.TickerRemoved();

            return !isDone;
        }

        public void Reset()
        {
            _tween.GotoAndStop(0);
        
        }

        public float GetDeltaTime( int frameSkip )
        {
            return _ticker.GetDeltaTime( frameSkip );
        }

        public void Dispose()
        {
            this._tween = null;
            this._ticker = null;
        }

        public void Initialize() {}
        public void AddTimer( TimerListener listener ) {}
        public void RemoveTimer( TimerListener listener ) {}
    }
}
