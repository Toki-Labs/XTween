using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class TickerListener
	{
		public TickerListener prevListener = null;
		public TickerListener nextListener = null;
		public virtual bool tick( float time )
		{
			return false;
		}
	}
}
