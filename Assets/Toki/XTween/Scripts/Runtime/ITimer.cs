using System;

namespace Toki.Tween
{
	public interface ITimer
	{
		float time
		{
			get;
			set;
		}
		float GetDeltaTime( int frameSkip );
		void Initialize();
		void AddTimer( TimerListener listener );
		void RemoveTimer( TimerListener listener );
	}
}