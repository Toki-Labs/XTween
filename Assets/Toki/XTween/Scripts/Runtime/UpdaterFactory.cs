using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class UpdaterFactory
	{
		public static ObjectUpdater<T> Create<T>( T target, XObjectHash source )
		{
			ObjectUpdater<T> updator = Pool<ObjectUpdater<T>>.Pop();
			updator.Target = target;
			updator.Finish = source;
			return updator;
		}

		public static GetSetUpdater Create( Action<float> setter, float start, float end, float[] controlPoints, XEventHash hash )
		{
			GetSetUpdater updater = Pool<GetSetUpdater>.Pop();
			updater.Setter = setter;
			updater.StartValue = start;
			updater.EndValue = end;
			updater.ControlPoints = controlPoints;
			updater.Finish = hash;
			return updater;
		}

		public static ColorUpdater<T> Create<T>( T target, string propertyName, XColorHash dest, XColorHash source )
		{
			ColorUpdater<T> updator = Pool<ColorUpdater<T>>.Pop();
			updator.Target = target;
			updator.PropertyName = propertyName;
			updator.Start = source;
			updator.Finish = dest;
			return updator;
		}

		public static ObjectUpdater Create( IClassicHandlable source )
		{
			ObjectUpdater updator = Pool<ObjectUpdater>.Pop();
			updator.Finish = source;
			return updator;
		}
			
		public static DisplayUpdater Create( GameObject target, IClassicHandlable dest, IClassicHandlable source )
		{
			DisplayUpdater updater = Pool<DisplayUpdater>.Pop();
			updater.Start = source;
			updater.Finish = dest;
			updater.Target = target;
			return updater;
		}

		public static DisplayContinousUpdater CreateContinous( GameObject target, IClassicHandlable dest, IClassicHandlable source )
		{
			DisplayContinousUpdater updater = Pool<DisplayContinousUpdater>.Pop();
			updater.Start = source;
			updater.Finish = dest;
			updater.Target = target;
			return updater;
		}
	}
}