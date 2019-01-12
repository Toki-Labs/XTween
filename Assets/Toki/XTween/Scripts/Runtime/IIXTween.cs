using System;

namespace Toki.Tween
{
	public interface IIXTween : IXTween, IDisposable
	{
		ITimer Ticker
		{
			get;
		}
		bool Disposed
		{
			get;
		}

		void Initialize( ITimer ticker, float position );
		void InitializeGroup();
		void StartPlay();
		void StartStop();
		void ResolveValues();
		bool Tick( float time );
	}
}