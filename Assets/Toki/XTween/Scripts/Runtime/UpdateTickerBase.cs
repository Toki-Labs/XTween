using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Toki.Tween
{
	public class UpdateTickerBase<Type> : SingleXTween<Type> where Type : MonoBehaviour
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
		public UpdateTickerBase()
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

		protected virtual void TimeSet()
		{

		}

		private TimerListener SetRemoveListener( TimerListener currentListener, TimerListener addListener )
		{
			if( currentListener != null )
			{
				currentListener.nextListener = addListener;
				addListener.prevListener = currentListener;
			}

			return addListener;
		}

		protected virtual void UpdateTickers ()
		{
			this.TimeSet();

			t = _time;
			for ( int i = 0; i < _deltaTimeLength; ++i )
			{
				if( this._deltaTimeArray[i] == null ) return;
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
					removeListener = SetRemoveListener(removeListener, listener);
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
					removeListener = SetRemoveListener(removeListener, listener);
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
					removeListener = SetRemoveListener(removeListener, listener);
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
					removeListener = SetRemoveListener(removeListener, listener);
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
					removeListener = SetRemoveListener(removeListener, listener);
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
					removeListener = SetRemoveListener(removeListener, listener);
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
					removeListener = SetRemoveListener(removeListener, listener);
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
					removeListener = SetRemoveListener(removeListener, listener);
					listener = ll;
					--_numListeners;
				}
			}
			
				
			if ((_first = l.nextListener) != null) {
				_first.prevListener = null;
			}
			l.nextListener = _tickerListenerPaddings[n + 1];
			if( removeListener != null )
			{
				TimerListener prevListener;
				do
				{
					prevListener = removeListener.prevListener;
					removeListener.TickerRemoved();
					removeListener.prevListener = null;
					removeListener.nextListener = null;
					removeListener = prevListener;
				}
				while( removeListener != null );
			}
		}
			
		public float Time
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
				if (_first.prevListener != null) 
				{
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
			
		public void Initialize()
		{
			this.TimeSet();
			this._deltaTimeArray [0] = new DeltaTimeTicker (0, _time);
			this._deltaTimeArray [1] = new DeltaTimeTicker (1, _time);
			this._deltaTimeArray [2] = new DeltaTimeTicker (2, _time);
			this._deltaTimeArray [3] = new DeltaTimeTicker (3, _time);
			this._deltaTimeArray [4] = new DeltaTimeTicker (4, _time);
		}
	}
}