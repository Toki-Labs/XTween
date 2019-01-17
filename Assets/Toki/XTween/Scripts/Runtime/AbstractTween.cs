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
		protected float _preDelay;
		protected float _preDelayTarget;
		protected float _postDelay;
		protected float _postDelayTarget;
		protected float _time;
		protected float _position = 0f;
		protected float _duration = 0f;
		protected float _durationTarget = 0f;
		protected float _scale = 1f;
		protected float _scaleReverse;
		protected float _startTime;
		protected int _repeatTotal = 1;
		protected int _repeatCurrent = 0;
		protected bool _reverse = false;
		protected bool _isPlaying = false;
		protected bool _isRealTime = false;
		protected uint _frameSkip = 0;
		protected uint _frameSkipCount = 0;
		protected bool _isGroup = false;
		protected bool _autoDispose = true;
		protected IClassicHandlable _classicHandlers;
			
		public ITimer Ticker
		{
			get { return _ticker; }
		}

		public virtual bool Disposed
		{
			get
			{
				return true;
			}
		}
			
		public float Duration
		{
			get { return DurationTotal * _repeatTotal; }
		}
			
		public float Position
		{
			get { return _position; }
		}
			
		public bool IsPlaying
		{
			get { return _isPlaying; }
		}

		private float DurationTotal
		{
			get
			{
				return _durationTarget + _preDelayTarget;
			}
		}

		public IClassicHandlable ClassicHandlers
		{
			get 
			{
				if( _classicHandlers == null )
				{
					_classicHandlers = new XEventHash();
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

		public void InitializeGroup()
		{
			this._time = 0f;
			this._position = 0f;
			this._isGroup = true;
			this._isPlaying = true;
			this.ComposeTime(0f);
		}

		public override void StopOnDestroy()
		{
			this.Release();
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
				Debug.Log(this.GetType());
				throw new System.Exception("Tweener were disposed. if you want to use for reusable instance. Set to \"SetLock()\" in instance");
			}
		}

		//Play Directly
		public virtual IXTween Play()
		{
			return this.Play(this._position + this._preDelayTarget);
		}

		//Goto And Play Directly
		public virtual IXTween Play( float position )
		{
			CheckDisposed();
			TickerChange();
			GotoAndPlayTween(position);
			return this;
		}

		private void GotoAndPlayTween(float position)
		{
			if( !_isPlaying )
			{
				ComposeTime(position);
				ResolveValues();
				InternalUpdate(_position * _scaleReverse);
				ComposePlay(_ticker.Time);
			}
		}

		public virtual IXTween Seek( float position ) 
		{
			CheckDisposed();
			ComposeTime(position);
			ResolveValues();
			InternalUpdate(_position * _scaleReverse);
			if (_classicHandlers != null && _classicHandlers.OnUpdate != null) 
				_classicHandlers.OnUpdate.Execute();

			Stop();
			return this;
		}

		private void ComposeTime(float position)
		{
			ComposeDecorator();
			if (position < 0) position = 0;
			if (position > DurationTotal * _repeatTotal) position = DurationTotal * _repeatTotal;
			if (position > DurationTotal && _repeatTotal > 1)
			{
				_repeatCurrent = Mathf.FloorToInt(position / DurationTotal);
				position = position - (_repeatCurrent * DurationTotal);
			}

			_position = position - _preDelayTarget;
			if( _reverse )
			{
				if( _position > 0f ) _position = _duration * _scale - _position;
				else _position = _durationTarget + _postDelayTarget;
			}

			float currentTime = _isGroup ? _time : _ticker.Time;
			_startTime = currentTime - position + _preDelayTarget;
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
			GotoAndPlayTween(this._position + this._preDelayTarget);
			return wait;
		}

		private void ComposePlay(float time)
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

				InternalRelease();
			}
		}

		public virtual void Reset()
		{
			Seek(0f);
		}

		public virtual void StartStop()
		{
			if (_classicHandlers != null && _classicHandlers.OnStop != null) 
				_classicHandlers.OnStop.Execute();
		}
		
		public virtual WaitForTweenPlay WaitForPlay(float position)
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

		public override bool Tick( float time )
		{
			if(_frameSkip < 1) return TickNormal(time);
			else return TickByCount(time);
		}

		public virtual bool TickNormal( float t )
		{
			if (!_isPlaying) return true;
			float time = t - _startTime;
			float controlTime = time;

			if( _reverse )
			{
				if( controlTime > 0f ) controlTime = _durationTarget - _postDelayTarget - controlTime;
				else controlTime = _durationTarget + _postDelayTarget;
			} 
				
			_position = time;
			InternalUpdate(controlTime * _scaleReverse);
				
			if (_classicHandlers != null && _classicHandlers.OnUpdate != null)
				_classicHandlers.OnUpdate.Execute();
				
			if (_isPlaying) 
			{
				// if( this.GetType() != typeof(SerialTween) )
				// if( this.GetType() == typeof(SerialTween) )
					// Debug.Log("Test: " + this.GetType() + ", " + time + ", " + _durationTarget);
					// Debug.Log(t + ", " + time + ", " + _durationTarget);
				if (time >= _durationTarget)
				{
					_repeatCurrent++;
					if( _repeatCurrent < _repeatTotal )
					{
						if( _isGroup ) _time = DurationTotal * _repeatCurrent;
						ComposeTime(0f);
					}
					else
					{
						_position = _durationTarget;
						return true;
					}
				}
				return false;
			}			
			return true;
		}

		public override void TickerRemoved()
		{
			if(_isPlaying)
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
			if (source._classicHandlers != null) 
			{
				_classicHandlers = new XObjectHash();
				_classicHandlers.CopyFrom(source._classicHandlers);
			}
		}

		private void ComposeDecorator()
		{
			this._scaleReverse = 1f / _scale;
			this._preDelayTarget = this._preDelay * this._scale;
			this._postDelayTarget = this._postDelay * this._scale;
			this._durationTarget = this._duration * this._scale + this._postDelayTarget;
		}

		public IXTween SetFrameSkip(uint skip)
		{
			this._frameSkip = skip;
			if( this._frameSkip > 0 )
			{
				if( this._frameSkip > 4 ) this._frameSkip = 4;
			}
			return this;
		}

		public virtual IXTween SetLock()
		{
			this._autoDispose = false;
			return this;
		}

		public IXTween SetReverse()
		{
			this._reverse = !this._reverse;
			return this;
		}

		public IXTween SetRepeat(int count)
		{
			this._repeatTotal = count;
			this._repeatCurrent = 0;
			return this;
		}

		public IXTween SetScale(float scale)
		{
			this._scale = scale;
			return this;
		}
		public IXTween SetDelay(float preDelay, float postDelay = 0f)
		{
			this._preDelay = preDelay;
			this._postDelay = postDelay;
			return this;
		}

		public IXTween AddOnComplete(Action listener)
		{
			return AddOnComplete(Executor.New(listener));
		}

		public IXTween AddOnComplete(IExecutable executor)
		{
			ClassicHandlers.OnComplete = executor;
			return this;
		}

		public IXTween AddOnStop(Action listener)
		{
			return AddOnStop(Executor.New(listener));
		}

		public IXTween AddOnStop(IExecutable executor)
		{
			ClassicHandlers.OnStop = executor;
			return this;
		}

		public IXTween AddOnPlay(Action listener)
		{
			return AddOnPlay(Executor.New(listener));
		}

		public IXTween AddOnPlay(IExecutable executor)
		{
			ClassicHandlers.OnPlay = executor;
			return this;
		}

		public IXTween AddOnUpdate(Action listener)
		{
			return AddOnUpdate(Executor.New(listener));
		}

		public IXTween AddOnUpdate(IExecutable executor)
		{
			ClassicHandlers.OnUpdate = executor;
			return this;
		}

		//Force Release
		public virtual void Release()
		{
			this._autoDispose = true;
			InternalRelease();
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
			this._reverse = false;
			this._preDelay = 0f;
			this._preDelayTarget = 0f;
			this._postDelay = 0f;
			this._postDelayTarget = 0f;
			this._duration = 0f;
			this._durationTarget = 0f;
			this._startTime = 0f;
			this._scale = 1f;
			this._repeatTotal = 1;
			this._repeatCurrent = 0;
			this._isPlaying = false;
			this._isRealTime = false;
			this._frameSkip = 0;
			this._frameSkipCount = 0;
			this._isGroup = false;
			this._autoDispose = true;
			this._classicHandlers = null;
		}
	}
}