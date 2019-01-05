using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class UpdaterFactory
	{
		public static IUpdating Create<T>( T target, XObjectHash source )
		{
			ObjectUpdater<T> updator = new ObjectUpdater<T>();
			updator.Target = target;
			updator.Finish = source;
			return updator;
		}

		public static GetSetUpdater Create( Action<float> setter, float start, float end, float[] controlPoints )
		{
			GetSetUpdater updater = new GetSetUpdater();
			updater.Setter = setter;
			updater.StartValue = start;
			updater.EndValue = end;
			updater.ControlPoints = controlPoints;
			return updater;
		}

		public static ColorUpdater<T> Create<T>( T target, string propertyName, XColorHash dest, XColorHash source )
		{
			ColorUpdater<T> updator = new ColorUpdater<T>();
			updator.Target = target;
			updator.PropertyName = propertyName;
			updator.Start = source;
			updator.Finish = dest;
			return updator;
		}

		public static ObjectUpdater Create( IClassicHandlable source )
		{
			ObjectUpdater updator = new ObjectUpdater();
			updator.Finish = source;
			return updator;
		}
			
		public static DisplayUpdater Create( GameObject target, IClassicHandlable dest, IClassicHandlable source )
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
	}
}