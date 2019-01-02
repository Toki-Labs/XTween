using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class AbstractTween : TimerListener, IIAni
	{
		public AbstractTween( ITimer ticker, float position )
		{
			_ticker = ticker;
			_isRealTime = (ticker is UpdateTickerReal);
			_position = position;
		}

		protected ITimer _ticker;
		protected float _position = 0f;
		protected float _duration = 0f;
		protected float _startTime;
		protected bool _isPlaying = false;
		protected bool _isRealTime = false;
		protected bool _stopOnComplete = true;
		protected uint _frameSkip = 1;
		protected uint _frameSkipCount = 0;
		protected bool _enableGroup = true;
		protected Action _decoratorStopOnDestroy;
		protected IClassicHandlable _classicHandlers;
		protected TickListener _tickListener;
			
		protected float _time;
			
		public ITimer Ticker
		{
			get { return _ticker; }
		}
			
		public float Duration
		{
			get { return _duration; }
		}
			
		public float Position
		{
			get { return _position; }
		}
			
		public bool IsPlaying
		{
			get { return _isPlaying; }
			set 
			{
				this._isPlaying = value;
				if( !this._isPlaying )
				{
					_ticker.RemoveTimer(this);
				}
			}
		}

		public virtual Action DecoratorStopOnDestroy
		{
			set
			{
				this._decoratorStopOnDestroy = value;
			}
		}

		public bool IsRealTime
		{
			get
			{
				return this._isRealTime;
			}
			set
			{
				if( this._isRealTime == value ) return;
				this._isRealTime = value;
				this._ticker = XTween.GetTicker( this._isRealTime );
				this._startTime = this._ticker.time - this._position;
			}
		}

		public uint FrameSkip
		{
			get
			{
				return this._frameSkip;
			}
			set
			{
				this._frameSkip = value;
				if( this._frameSkip > 0 )
				{
					if( this._frameSkip > 4 ) this._frameSkip = 4;
					this._tickListener = this.TickByCount;
				}
				else
				{
					this._tickListener = this.TickNormal;
				}
			}
		}
		
		public bool StopOnComplete
		{
			get { return _stopOnComplete; }
			set { _stopOnComplete = value; }
		}
			
		public IClassicHandlable ClassicHandlers
		{
			get { return _classicHandlers; }
			set { _classicHandlers = value; }
		}
			
		public IExecutable OnPlay
		{
			get { return _classicHandlers != null ? _classicHandlers.OnPlay : null; }
			set { GetClassicHandlers.OnPlay = value; }
		}
			
		public IExecutable OnStop
		{
			get { return _classicHandlers != null ? _classicHandlers.OnStop : null; }
			set { GetClassicHandlers.OnStop = value; }
		}
			
		public IExecutable OnUpdate
		{
			get { return _classicHandlers != null ? _classicHandlers.OnUpdate : null; }
			set { GetClassicHandlers.OnUpdate = value; }
		}
			
		public IExecutable OnComplete
		{
			get { return _classicHandlers != null ? _classicHandlers.OnComplete : null; }
			set { GetClassicHandlers.OnComplete = value; }
		}
			
		protected IClassicHandlable GetClassicHandlers
		{
			get 
			{
				if( this._classicHandlers == null )
				{
					this._classicHandlers = new XHash();
				}
				return _classicHandlers; 
			}
		}

		public override void StopOnDestroy()
		{
			if( this._decoratorStopOnDestroy != null )
			{
				this._decoratorStopOnDestroy.Invoke();
			}
			else
			{
				this._isPlaying = false;
			}
			this._enableGroup = false;
		}

		public virtual void StopFromDisposeList()
		{
			this._isPlaying = false;
		}

		public void IntializeGroup()
		{
			this._position = 0f;
			this._time = 0f;
		}

		public virtual void Play()
		{
			if (!_isPlaying) {
				if (_position >= _duration) _position = 0;
				ResolveValues();
				float t = _ticker.time;
				_startTime = t - _position;
				PlayNow(t);
			}
		}

		public virtual WaitForTweenPlay WaitForPlay()
		{
			return new WaitForTweenPlay(this);
		}

		private void PlayNow(float time)
		{
	#if UNITY_EDITOR
			if( Application.isPlaying )
			{
				_time = (this._ticker is UpdateTicker) ? Time.time : Time.realtimeSinceStartup;
			}
			else
			{
				_time = Time.realtimeSinceStartup;
			}
	#else
			_time = (this._ticker is UpdateTicker) ? Time.time : Time.realtimeSinceStartup;
	#endif
			_isPlaying = true;
			_ticker.AddTimer(this);
			if (_classicHandlers != null && _classicHandlers.OnPlay != null) 
				_classicHandlers.OnPlay.Execute();
			Tick(time);
		}

		public virtual void ResolveValues()
		{

		}
			
		public virtual void StartPlay()
		{
			if (_classicHandlers != null && _classicHandlers.OnPlay != null)
				_classicHandlers.OnPlay.Execute();
		}

		public virtual void Stop()
		{
			if (_isPlaying) 
			{
				_isPlaying = false;
				if (_classicHandlers != null && _classicHandlers.OnStop != null) 
					_classicHandlers.OnStop.Execute();
			}
		}

		public virtual void StartStop()
		{
			if (_classicHandlers != null && _classicHandlers.OnStop != null) 
				_classicHandlers.OnStop.Execute();
		}
			
		public virtual void TogglePause()
		{
			if (_isPlaying) Stop();
			else Play();
		}
			
		public virtual void GotoAndPlay( float position )
		{
			if (position < 0) position = 0;
			if (position > _duration) position = _duration;
			_position = position;
			_startTime = _ticker.time - _position;
			ResolveValues();
			InternalUpdate(position);
			PlayNow(_ticker.time);
		}
			
		public virtual void GotoAndStop( float position ) 
		{
			if (position < 0) position = 0;
			if (position > _duration) position = _duration;
			
			_position = position;
			ResolveValues();
			InternalUpdate(position);
			if (_classicHandlers != null && _classicHandlers.OnUpdate != null) 
				_classicHandlers.OnUpdate.Execute();

			Stop();
		}
			
		public virtual void UpdateTween( float time )
		{
			bool isComplete = false;
				
			if ((_position < _duration && _duration <= time) || (0 < _position && time <= 0)) 
			{
				isComplete = true;
			}
				
			_position = time;
			if( this._enableGroup )
			{
				InternalUpdate(time);
				
				if (_classicHandlers != null && _classicHandlers.OnUpdate != null)
					_classicHandlers.OnUpdate.Execute();
				
				if (isComplete)
				{
					if (_classicHandlers != null && _classicHandlers.OnComplete != null)
						_classicHandlers.OnComplete.Execute();
				}
			}	
		}
			
		public override bool Tick( float time )
		{
			return this._tickListener( time );
		}

		public virtual bool TickNormal( float time )
		{
			if (!_isPlaying) {
				return true;
			}
				
			float t = time - _startTime;
				
			_position = t;
			InternalUpdate(t);
				
			if (_classicHandlers != null && _classicHandlers.OnUpdate != null)
				_classicHandlers.OnUpdate.Execute();
				
			if (_isPlaying) 
			{
				if (t >= _duration) 
				{
					_position = _duration;
					if (_stopOnComplete) return true;
					else 
					{
						if (_classicHandlers != null && _classicHandlers.OnComplete != null)
							_classicHandlers.OnComplete.Execute();

						_position = t - _duration;
						_startTime = time - _position;
						Tick(time);
					}
				}
				return false;
			}			
			return true;
		}

		public override void TickerRemoved()
		{
			// Debug.Log("Remove Ticker");
			if(_isPlaying && _stopOnComplete)
			{
				_isPlaying = false;
				if (_classicHandlers != null && _classicHandlers.OnComplete != null)
					_classicHandlers.OnComplete.Execute();
			}
		}

		public bool TickByCount( float time )
		{
			bool remove = false;
			this._frameSkipCount++;
			if( this._frameSkip < this._frameSkipCount )
			{
				remove = this.TickNormal( time );
				this._frameSkipCount = 0;
			}
			return remove;
		}
			
		protected virtual void InternalUpdate( float time )
		{
				
		}
			
		public IAni Clone()
		{
			AbstractTween instance = NewInstance();
			if (instance != null) {
				instance.CopyFrom(this);
			}
			return instance;
		}
			
		protected virtual AbstractTween NewInstance()
		{
			return null;
		}
			
		protected virtual void CopyFrom( AbstractTween source )
		{
			_ticker = source._ticker;
			_duration = source._duration;
			_stopOnComplete = source._stopOnComplete;
			if (source._classicHandlers != null) 
			{
				_classicHandlers = new XObjectHash();
				_classicHandlers.CopyFrom(source._classicHandlers);
			}
		}
	}
}