using UnityEngine;
using System.Collections;

public class TickerListener
{
	public TickerListener prevListener = null;
	public TickerListener nextListener = null;
	public virtual bool tick( float time )
	{
		return false;
	}
}
