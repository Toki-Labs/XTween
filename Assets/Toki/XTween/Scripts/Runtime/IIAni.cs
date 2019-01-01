using System;

namespace Toki.Tween
{
	public interface IIAni : IAni
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