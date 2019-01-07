using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class AbstractTween : TimerListener, IIXTween
	{
		public virtual void Initialize( ITimer ticker, float position )
		{
			_ticker = ticker;
			_isRealTime = (ticker is UpdateTickerReal);
			_position = position;
		}

		protected ITimer _ticker;
		protected float _time;
		protected float _position = 0f;
		protected float _duration = 0f;
		protected float _startTime;
		protected bool _isPlaying = false;
		protected bool _isRealTime = false;
		protected bool _stopOnComplete = true;
		protected uint _frameSkip = 0;
		protected uint _frameSkipCount = 0;
		protected bool _enableGroup = true;
		protected bool _autoDispose = true;
		//when wrapped in decorator
		protected Action _decoratorStopOnDestroy;
		protected IClassicHandlable _classicHandlers;
			
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
				}
			}
		}
		
		public IClassicHandlable ClassicHandlers
		{
			get 
			{
				if( _classicHandlers == null )
				{
					_classicHandlers = new XHash();
				} 
				return _classicHandlers; 
			}
			set { _classicHandlers = value; }
		}
			
		public IExecutable OnPlay
		{
			get { return _classicHandlers != null ? _classicHandlers.OnPlay : null; }
			set { ClassicHandlers.OnPlay = value; }
		}
			
		public IExecutable OnStop
		{
			get { return _classicHandlers != null ? _classicHandlers.OnStop : null; }
			set { ClassicHandlers.OnStop = value; }
		}
			
		public IExecutable OnUpdate
		{
			get { return _classicHandlers != null ? _classicHandlers.OnUpdate : null; }
			set { ClassicHandlers.OnUpdate = value; }
		}
			
		public IExecutable OnComplete
		{
			get { return _classicHandlers != null ? _classicHandlers.OnComplete : null; }
			set { ClassicHandlers.OnComplete = value; }
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
			this.Release();
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

		private void CheckDisposed()
		{
			if( _ticker == null )
			{
				throw new System.Exception("Tweener is disposed. if you want to use for reusable instance. Set to \"Lock()\" in instance");
			}
		}

		//Play Directly
		public virtual IXTween Play()
		{
			CheckDisposed();
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
			CheckDisposed();
			WaitForTweenPlay wait;
			if( _ticker is WaitForTweenPlay )
			{
				wait = (WaitForTweenPlay)_ticker;
			}
			else
			{
				wait = Pool<WaitForTweenPlay>.Pop();
				wait.Initialize(_ticker, this);
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
				{
					_classicHandlers.OnStop.Execute();
					InternalRelease();
				}
			}
		}

		public virtual void Reset()
		{
			GotoAndStop(0);
		}

		public virtual void StartStop()
		{
			if (_classicHandlers != null && _classicHandlers.OnStop != null) 
				_classicHandlers.OnStop.Execute();
		}
			
		//Goto And Play Directly
		public virtual IXTween GotoAndPlay( float position )
		{
			CheckDisposed();
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
			CheckDisposed();
			WaitForTweenPlay wait;
			if( _ticker is WaitForTweenPlay )
			{
				wait = (WaitForTweenPlay)_ticker;
			}
			else
			{
				wait = Pool<WaitForTweenPlay>.Pop();
				wait.Initialize(_ticker, this);
				_ticker = wait;
			}
			GotoAndPlayTween(position);
			return wait;
		}

		public virtual void GotoAndStop( float position ) 
		{
			CheckDisposed();
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

					InternalRelease();
				}
			}	
		}

		public override bool Tick( float time )
		{
			if(_frameSkip < 1) return TickNormal(time);
			else return TickByCount(time);
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
						InternalRelease();
					}
				}
				return false;
			}			
			return true;
		}

		public override void TickerRemoved()
		{
			if(_isPlaying && _stopOnComplete)
			{
				_isPlaying = false;
				if (_classicHandlers != null && _classicHandlers.OnComplete != null)
					_classicHandlers.OnComplete.Execute();

				InternalRelease();
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
			
		public IXTween Clone()
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

		public IXTween Lock()
		{
			this._autoDispose = false;
			return this;
		}

		public IXTween AddOnComplete(Action listener)
		{
			return AddOnComplete(Executor.New(listener));
		}

		public IXTween AddOnComplete(IExecutable executor)
		{
			_classicHandlers.OnComplete = executor;
			return this;
		}

		public IXTween AddOnStop(Action listener)
		{
			return AddOnStop(Executor.New(listener));
		}

		public IXTween AddOnStop(IExecutable executor)
		{
			_classicHandlers.OnStop = executor;
			return this;
		}

		public IXTween AddOnPlay(Action listener)
		{
			return AddOnPlay(Executor.New(listener));
		}

		public IXTween AddOnPlay(IExecutable executor)
		{
			_classicHandlers.OnPlay = executor;
			return this;
		}

		public IXTween AddOnUpdate(Action listener)
		{
			return AddOnUpdate(Executor.New(listener));
		}

		public IXTween AddOnUpdate(IExecutable executor)
		{
			_classicHandlers.OnUpdate = executor;
			return this;
		}

		//Force Release
		public virtual void Release()
		{
			this._autoDispose = true;
			this.InternalRelease();
		}

		protected virtual void InternalRelease()
		{
			throw new System.Exception("You should implement this method in sub class. " + this.GetType());
		}

		public virtual void Dispose()
		{
			WaitForTweenPlay wait;
			if( (wait = this._ticker as WaitForTweenPlay) != null ) 
				wait.PoolPush();
			this._ticker = null;
			this._time = 0f;
			this._position = 0f;
			this._duration = 0f;
			this._startTime = 0f;
			this._isPlaying = false;
			this._isRealTime = false;
			this._stopOnComplete = true;
			this._frameSkip = 1;
			this._frameSkipCount = 0;
			this._autoDispose = true;
			this._enableGroup = true;
			this._decoratorStopOnDestroy = null;
			this._classicHandlers = null;
		}
	}
}