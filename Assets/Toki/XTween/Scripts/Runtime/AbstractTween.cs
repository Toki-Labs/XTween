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
		}

		public virtual Action DecoratorStopOnDestroy
		{
			set
			{
				this._decoratorStopOnDestroy = value;
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
				_isPlaying = false;
			}
			_enableGroup = false;
		}

		public virtual void StopFromDisposeList()
		{
			_isPlaying = false;
		}

		public void IntializeGroup()
		{
			this._position = 0f;
			this._time = 0f;
		}

		private void TickerChange()
		{
			if( _ticker is WaitForTweenPlay )
				_ticker = (_ticker as WaitForTweenPlay).Ticker;
		}

		//Play Directly
		public virtual IAni Play()
		{
			TickerChange();
			PlayTween();
			return this;
		}

		private void PlayTween()
		{
			if (!_isPlaying) 
			{
				if (_position >= _duration) _position = 0;
				float t = _ticker.Time;
				_startTime = t - _position;
				ResolveValues();
				PlayCompose(t);
			}
		}

		public virtual WaitForTweenPlay WaitForPlay()
		{
			WaitForTweenPlay wait;
			if( _ticker is WaitForTweenPlay )
			{
				wait = (WaitForTweenPlay)_ticker;
			}
			else
			{
				wait = new WaitForTweenPlay(_ticker, this);
				_ticker = wait;
			}
			PlayTween();
			return wait;
		}

		private void PlayCompose(float time)
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
			
		//Goto And Play Directly
		public virtual IAni GotoAndPlay( float position )
		{
			TickerChange();
			GotoAndPlayTween(position);
			return this;
		}

		private void GotoAndPlayTween(float position)
		{
			if (position < 0) position = 0;
			if (position > _duration) position = _duration;
			_position = position;
			_startTime = _ticker.Time - _position;
			ResolveValues();
			InternalUpdate(position);
			PlayCompose(_ticker.Time);
		}
		
		public virtual WaitForTweenPlay WaitForGotoAndPlay(float position)
		{
			WaitForTweenPlay wait;
			if( _ticker is WaitForTweenPlay )
			{
				wait = (WaitForTweenPlay)_ticker;
			}
			else
			{
				wait = new WaitForTweenPlay(_ticker, this);
				_ticker = wait;
			}
			GotoAndPlayTween(position);
			return wait;
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
			return _tickListener( time );
		}

		public virtual bool TickNormal( float time )
		{
			if (!_isPlaying) return true;
				
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

		public IAni AddOnComplete(Action listener)
		{
			return AddOnComplete(Executor.New(listener));
		}

		public IAni AddOnComplete(IExecutable executor)
		{
			_classicHandlers.OnComplete = executor;
			return this;
		}

		public IAni AddOnStop(Action listener)
		{
			return AddOnStop(Executor.New(listener));
		}

		public IAni AddOnStop(IExecutable executor)
		{
			_classicHandlers.OnStop = executor;
			return this;
		}

		public IAni AddOnPlay(Action listener)
		{
			return AddOnPlay(Executor.New(listener));
		}

		public IAni AddOnPlay(IExecutable executor)
		{
			_classicHandlers.OnPlay = executor;
			return this;
		}

		public IAni AddOnUpdate(Action listener)
		{
			return AddOnUpdate(Executor.New(listener));
		}

		public IAni AddOnUpdate(IExecutable executor)
		{
			_classicHandlers.OnUpdate = executor;
			return this;
		}
	}
}