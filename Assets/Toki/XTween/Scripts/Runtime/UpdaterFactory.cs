using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpdaterFactory
{
	public static IUpdating Create<T>( IClassicHandlable source )
	{
		IUpdating updator = new ObjectUpdater<T>();
		if( source != null )
		{
			updator.finish = source;
		}
			
		return updator;
	}

	public IUpdating Create( IClassicHandlable source )
	{
		IUpdating updator = new ObjectUpdater();
		if( source != null )
		{
			updator.finish = source;
		}
			
		return updator;
	}
		
	public IUpdating Create( GameObject target, IClassicHandlable dest, IClassicHandlable source )
	{
		IUpdating updater = new DisplayUpdater();
		
		updater.start = source;
		updater.finish = dest;
		updater.target = target;
			
		return updater;
	}

	public IUpdating CreateContinous( GameObject target, IClassicHandlable dest, IClassicHandlable source )
	{
		IUpdating updater = new DisplayContinousUpdater();
		
		updater.start = source;
		updater.finish = dest;
		updater.target = target;
		
		return updater;
	}
		
	public IUpdating CreateBezier( GameObject target, XHash dest, XHash source, XPoint controlPoint )
	{
		BezierUpdater bezierUpdater = new BezierUpdater();
		bezierUpdater.target = target;
        bezierUpdater.start = source;
        bezierUpdater.finish = dest;
        bezierUpdater.controlPoint = controlPoint;
			
		return bezierUpdater;
	}
}