using System;

namespace Toki.Tween
{
	public interface IIXTween : IXTween
	{
		ITimer Ticker
		{
			get;
		}
		Action DecoratorStopOnDestroy
		{
			set;
		}

		void IntializeGroup();
		void StartPlay();
		void StartStop();
		void ResolveValues();
		void UpdateTween( float time );
	}
}