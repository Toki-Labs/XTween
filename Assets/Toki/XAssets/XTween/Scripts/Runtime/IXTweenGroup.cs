using System;
using System.Collections.Generic;

namespace Toki.Tween
{
	public interface IXTweenGroup : IXTween
	{
		bool Contains( IXTween tween );
		IXTween GetTweenAt( int index );
		int GetTweenIndex( IXTween tween );
		IIXTween[] tweens
		{
			get;
		}
	}
}