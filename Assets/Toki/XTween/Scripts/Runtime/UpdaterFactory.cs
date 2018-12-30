using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpdaterFactory
{
	public static IUpdating Create<T>( T target, XObjectHash source )
	{
		ObjectUpdater<T> updator = new ObjectUpdater<T>();
		updator.Target = target;
		updator.Finish = source;
		return updator;
	}

	public static IUpdating Create<T>( T target, string propertyName, XColorHash dest, XColorHash source )
	{
		ColorUpdater<T> updator = new ColorUpdater<T>();
		updator.Target = target;
		updator.PropertyName = propertyName;
		updator.Start = source;
		updator.Finish = dest;
		return updator;
	}

	public IUpdating Create( IClassicHandlable source )
	{
		IUpdating updator = new ObjectUpdater();
		updator.Finish = source;
		return updator;
	}
		
	public IUpdating Create( GameObject target, IClassicHandlable dest, IClassicHandlable source )
	{
		DisplayUpdater updater = new DisplayUpdater();
		updater.Start = source;
		updater.Finish = dest;
		updater.Target = target;
		return updater;
	}

	public IUpdating CreateContinous( GameObject target, IClassicHandlable dest, IClassicHandlable source )
	{
		DisplayContinousUpdater updater = new DisplayContinousUpdater();
		updater.Start = source;
		updater.Finish = dest;
		updater.Target = target;
		return updater;
	}
		
	public IUpdating CreateBezier( GameObject target, XHash dest, XHash source, XPoint controlPoint )
	{
		BezierUpdater bezierUpdater = new BezierUpdater();
		bezierUpdater.Target = target;
        bezierUpdater.Start = source;
        bezierUpdater.Finish = dest;
        bezierUpdater.controlPoint = controlPoint;
			
		return bezierUpdater;
	}
}