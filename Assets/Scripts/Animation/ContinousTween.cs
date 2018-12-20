using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContinousTween : AbstractTween, IIAniObject
{
	public ContinousTween( ITimer ticker ) : base( ticker, 0 )
	{
			
	}
		
    protected IUpdating _updater;
	protected IEasing _easing;

	public float time
	{
		get { return _duration; }
		set { _duration = value; }
	}
	
	public IEasing easing
	{
		get { return _easing; }
		set { _easing = value; }
	}
	
	public override void ResolveValues()
	{
		DisplayContinousUpdater display = (DisplayContinousUpdater)_updater;
		display.ResolveValues();
	}

	public bool isPlaying
	{
		get { return _isPlaying; }
		set 
		{
			this._isPlaying = value;
			Debug.Log( "isPlaying" );
			if( !this._isPlaying )
			{
				_ticker.RemoveTimer(this);
			}
		}
	}
		
	public IUpdating updater
	{
		get { return _updater; }
		set 
		{ 
			_updater = value;
				
			if( _updater != null )
			{
				DisplayContinousUpdater display = (DisplayContinousUpdater)_updater;
				display.ticker = this._ticker;
				display.frameSkip = (int)this._frameSkip;
				display.stopOnDestroyHandler = this.StopOnDestroy;
			}
		}
	}
		
	protected override void InternalUpdate( float time )
	{
		float factor = 0.0f;
		
		if (time > 0.0f) {
			if (time < _duration) {
				factor = _easing.Calculate(time, 0.0f, 1.0f, _duration);
			}
			else {
				factor = 1.0f;
			}
		}
		_updater.Updating(factor);
	}

	public override bool TickNormal( float time )
	{
		if (!_isPlaying) {
			return true;
		}
		float t = time - _startTime;
		
		_position = t;
		InternalUpdate(t);
		
		if (_classicHandlers != null && _classicHandlers.onUpdate != null) {
			_classicHandlers.onUpdate.Execute();
		}

		if (_isPlaying) {
			if (t >= _duration) {
				_position = _duration;
			}
			return false;
		}
		return true;
	}

	public override void Play()
	{
		if( !_isPlaying )
		{
			_position = 0;
		}
		base.Play();
	}

	public override void GotoAndPlay( float position )
	{
        //Not implement
	}

	public override void GotoAndStop( float position )
	{
        //Not implement
	}

	public virtual void UpdateTween( float time )
	{
		//Not implement
	}
}