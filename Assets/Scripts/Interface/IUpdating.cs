using System;
using UnityEngine;

public interface IUpdating
{		
	GameObject target
	{
		get;
		set;
	}
		
	IClassicHandlable start
	{
		set;
	}
		
	IClassicHandlable finish
	{
		set;
	}
		
	IUpdating Clone();
    void ResolveValues();
	void Updating( float factor );
}