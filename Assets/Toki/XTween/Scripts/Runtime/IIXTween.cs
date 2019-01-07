using System;

namespace Toki.Tween
{
	public interface IIXTween : IXTween, IDisposable
	{
		ITimer Ticker
		{
			get;
		}
		TweenDecorator Decorator
		{
			set;
		}

		void Initialize( ITimer ticker, float position );
		void IntializeGroup();
		void StartPlay();
		void StartStop();
		void ResolveValues();
		void UpdateTween( float time );
	}
}