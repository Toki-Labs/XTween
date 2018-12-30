using System;
using UnityEngine;

public interface IUpdating
{		
	Action StopOnDestroyHandler
	{
		set;
	}
		
	IClassicHandlable Start
	{
		set;
	}
		
	IClassicHandlable Finish
	{
		set;
	}
		
    void ResolveValues();
	void Updating( float factor );
}