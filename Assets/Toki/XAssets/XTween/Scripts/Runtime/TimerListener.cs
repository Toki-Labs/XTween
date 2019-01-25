using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public delegate bool TickListener( float time );

	public class TimerListener
	{
		public TimerListener prevListener = null;
		public TimerListener nextListener = null;
		public virtual void TickerRemoved()
		{

		}
		public virtual void StopOnDestroy()
		{
			
		}
		public virtual bool Tick( float time )
		{
			return false;
		}
	}
}