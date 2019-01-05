using System;

namespace Toki.Tween
{
	public interface IIAniObject : IAniObject, IIAni
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