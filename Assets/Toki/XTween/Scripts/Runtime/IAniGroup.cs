using System;
using System.Collections.Generic;

namespace Toki.Tween
{
	public interface IAniGroup : IAni
	{
		bool Contains( IAni tween );
		IAni GetTweenAt( int index );
		int GetTweenIndex( IAni tween );
		IIAni[] tweens
		{
			get;
		}
	}
}