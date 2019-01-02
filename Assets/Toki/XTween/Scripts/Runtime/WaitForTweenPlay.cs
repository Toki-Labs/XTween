using UnityEngine;
using System.Collections;
using Toki.Tween;

namespace Toki.Tween
{
    public class WaitForTweenPlay : IEnumerator, ITimer
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

        /************************************************************************
        *	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
        ************************************************************************/

        /************************************************************************
        *	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
        ************************************************************************/
        public WaitForTweenPlay(ITimer ticker, AbstractTween tween)
        {
            _ticker = ticker;
            _tween = tween;
        }

        public object Current { get{return null;} }

        public bool MoveNext()
        {
            bool isDone = false;
            if( _tween.IsPlaying )
            {
                isDone = _tween.Tick(_ticker.Time);
            }
            if( isDone ) _tween.TickerRemoved();
            return !isDone;
        }

        public void Reset()
        {
            _tween.GotoAndStop(0);
        }

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
        public float GetDeltaTime( int frameSkip )
        {
            return _ticker.GetDeltaTime( frameSkip );
        }

        public void Initialize() {}
        public void AddTimer( TimerListener listener ) {}
        public void RemoveTimer( TimerListener listener ) {}
    }
}
