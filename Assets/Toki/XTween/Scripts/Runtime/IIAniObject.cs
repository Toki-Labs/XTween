using System;

namespace Toki.Tween
{
	public interface IIAniObject : IAniObject, IIAni
	{
		float time
		{
			get;
			set;
		}
			
		IEasing easing
		{
			get;
			set;
		}
			
		IUpdating updater
		{
			get;
			set;
		}
	}
}