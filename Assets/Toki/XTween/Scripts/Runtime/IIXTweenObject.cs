using System;

namespace Toki.Tween
{
	public interface IIXTweenObject : IXTweenObject, IIXTween
	{
		float Time
		{
			get;
			set;
		}
			
		IEasing Easing
		{
			get;
			set;
		}
			
		IUpdating Updater
		{
			get;
			set;
		}
	}
}