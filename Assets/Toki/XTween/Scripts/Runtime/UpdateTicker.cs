using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UpdateTicker : SingleXTween<UpdateTicker>, ITimer
{
	protected TimerListener _first = null;
	protected int _numListeners = 0;
	protected List<TimerListener> _tickerListenerPaddings;
	protected float _time;
	protected int n;
	protected float t;
	protected TimerListener prevListener;
	protected TimerListener listener;
	protected TimerListener l;
	protected TimerListener ll;
	protected const int _deltaTimeLength = 5;
	protected DeltaTimeTicker[] _deltaTimeArray = new DeltaTimeTicker[_deltaTimeLength];
	
	public float GetDeltaTime( int frameSkip )
	{
		return _deltaTimeArray[frameSkip].deltaTime;
	}
		
#if UNITY_EDITOR
	public UpdateTicker()
	{
		this.Start();
	}
#endif
		
	void Start()
	{
		_tickerListenerPaddings = new List<TimerListener>();
		
		prevListener = null;
		
		for ( int i = 0; i < 10; ++i ) 
		{
			TimerListener listener = new TimerListener();
			if (prevListener != null) {
				prevListener.nextListener = listener;
				listener.prevListener = prevListener;
			}
			prevListener = listener;
			_tickerListenerPaddings.Add( listener );
		}
	}

#if UNITY_EDITOR
	public void AddUpdate()
	{
		InitializeInEditor();
		EditorApplication.update += this.UpdateTickers;
	}

	public void RemoveUpdate()
	{
		InitializeInEditor();
		EditorApplication.update -= this.UpdateTickers;
	}

	private void InitializeInEditor()
	{
		_time = t = 0f;
		_numListeners = n = 0;
		_tickerListenerPaddings = null;
		_first = prevListener = listener = l = ll = null;
		Start();
	}
#endif
		
	void Update()
    {
        this.UpdateTickers();
    }

	protected virtual void UpdateTickers ()
	{
#if UNITY_EDITOR
		if( Application.isPlaying )
			_time = Time.time;
		else
			_time = Time.realtimeSinceStartup;
#else
		_time = Time.time;
#endif
		t = _time;
		for ( int i = 0; i < _deltaTimeLength; ++i )
		{
			this._deltaTimeArray[i].Update(_time);
		}
		n = 8 - (_numListeners % 8); 
		listener = _tickerListenerPaddings[0];
		l = _tickerListenerPaddings[n];
		ll = null;
			
			
		if ((l.nextListener = _first) != null) {
			_first.prevListener = l;
		}
			
		TimerListener removeListener = null;
		while (listener.nextListener != null) {
			if ((listener = listener.nextListener).Tick(t)) {
				if (listener.prevListener != null) {
					listener.prevListener.nextListener = listener.nextListener;
				}
				if (listener.nextListener != null) {
					listener.nextListener.prevListener = listener.prevListener;
				}
				if (listener == _first) {
					_first = listener.nextListener;
				}
				ll = listener.prevListener;
				listener.nextListener = null;
				listener.prevListener = null;
				removeListener = listener;
				listener = ll;
				--_numListeners;
			}
			if ((listener = listener.nextListener).Tick(t)) {
				if (listener.prevListener != null) {
					listener.prevListener.nextListener = listener.nextListener;
				}
				if (listener.nextListener != null) {
					listener.nextListener.prevListener = listener.prevListener;
				}
				if (listener == _first) {
					_first = listener.nextListener;
				}
				ll = listener.prevListener;
				listener.nextListener = null;
				listener.prevListener = null;
				removeListener = listener;
				listener = ll;
				--_numListeners;
			}
			if ((listener = listener.nextListener).Tick(t)) {
				if (listener.prevListener != null) {
					listener.prevListener.nextListener = listener.nextListener;
				}
				if (listener.nextListener != null) {
					listener.nextListener.prevListener = listener.prevListener;
				}
				if (listener == _first) {
					_first = listener.nextListener;
				}
				ll = listener.prevListener;
				listener.nextListener = null;
				listener.prevListener = null;
				removeListener = listener;
				listener = ll;
				--_numListeners;
			}
			if ((listener = listener.nextListener).Tick(t)) {
				if (listener.prevListener != null) {
					listener.prevListener.nextListener = listener.nextListener;
				}
				if (listener.nextListener != null) {
					listener.nextListener.prevListener = listener.prevListener;
				}
				if (listener == _first) {
					_first = listener.nextListener;
				}
				ll = listener.prevListener;
				listener.nextListener = null;
				listener.prevListener = null;
				removeListener = listener;
				listener = ll;
				--_numListeners;
			}
			if ((listener = listener.nextListener).Tick(t)) {
				if (listener.prevListener != null) {
					listener.prevListener.nextListener = listener.nextListener;
				}
				if (listener.nextListener != null) {
					listener.nextListener.prevListener = listener.prevListener;
				}
				if (listener == _first) {
					_first = listener.nextListener;
				}
				ll = listener.prevListener;
				listener.nextListener = null;
				listener.prevListener = null;
				removeListener = listener;
				listener = ll;
				--_numListeners;
			}
			if ((listener = listener.nextListener).Tick(t)) {
				if (listener.prevListener != null) {
					listener.prevListener.nextListener = listener.nextListener;
				}
				if (listener.nextListener != null) {
					listener.nextListener.prevListener = listener.prevListener;
				}
				if (listener == _first) {
					_first = listener.nextListener;
				}
				ll = listener.prevListener;
				listener.nextListener = null;
				listener.prevListener = null;
				removeListener = listener;
				listener = ll;
				--_numListeners;
			}
			if ((listener = listener.nextListener).Tick(t)) {
				if (listener.prevListener != null) {
					listener.prevListener.nextListener = listener.nextListener;
				}
				if (listener.nextListener != null) {
					listener.nextListener.prevListener = listener.prevListener;
				}
				if (listener == _first) {
					_first = listener.nextListener;
				}
				ll = listener.prevListener;
				listener.nextListener = null;
				listener.prevListener = null;
				removeListener = listener;
				listener = ll;
				--_numListeners;
			}
			if ((listener = listener.nextListener).Tick(t)) {
				if (listener.prevListener != null) {
					listener.prevListener.nextListener = listener.nextListener;
				}
				if (listener.nextListener != null) {
					listener.nextListener.prevListener = listener.prevListener;
				}
				if (listener == _first) {
					_first = listener.nextListener;
				}
				ll = listener.prevListener;
				listener.nextListener = null;
				listener.prevListener = null;
				removeListener = listener;
				listener = ll;
				--_numListeners;
			}
		}
		
			
		if ((_first = l.nextListener) != null) {
			_first.prevListener = null;
		}
		l.nextListener = _tickerListenerPaddings[n + 1];
		if( removeListener != null ) removeListener.TickerRemoved();
	}
		
	public float time
	{
		get { return _time; }
		set { _time = value; }
	}
		
	public void AddTimer( TimerListener listener )
	{
		if (listener.nextListener != null || listener.prevListener != null) {
			return;
		}
		if (_first != null) {
			if (_first.prevListener != null) {
				_first.prevListener.nextListener = listener;
				listener.prevListener = _first.prevListener;
			}
			listener.nextListener = _first;
			_first.prevListener = listener;
		}
		_first = listener;

		++_numListeners;
		// Debug.Log("Add: " + _numListeners);
	}
		
	public void RemoveTimer( TimerListener listener )
	{
		TimerListener l = _first;
			
		while (l != null) {
				
			if (l == listener) {
				if (l.prevListener != null) {
					l.prevListener.nextListener = l.nextListener;
					l.nextListener = null;
				}
				else {
					_first = l.nextListener;
				}
				if (l.nextListener != null) {
					l.nextListener.prevListener = l.prevListener;
					l.prevListener = null;
				}
				--_numListeners;
			}
				
			l = l.nextListener;
		}
		// Debug.Log("Remove: " + _numListeners);
	}
		
	public void Initialize()
	{
#if UNITY_EDITOR
		if( Application.isPlaying )
			_time = Time.time;
		else
			_time = Time.realtimeSinceStartup;
#else
		_time = Time.time;
#endif
		this._deltaTimeArray [0] = new DeltaTimeTicker (0, _time);
		this._deltaTimeArray [1] = new DeltaTimeTicker (1, _time);
		this._deltaTimeArray [2] = new DeltaTimeTicker (2, _time);
		this._deltaTimeArray [3] = new DeltaTimeTicker (3, _time);
		this._deltaTimeArray [4] = new DeltaTimeTicker (4, _time);
	}
}