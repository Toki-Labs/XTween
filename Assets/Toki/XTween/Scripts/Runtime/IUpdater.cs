using System;
using UnityEngine;

namespace Toki.Tween
{
	public interface IUpdater
	{
		bool isRelative
		{
			get;
			set;
		}
			
		string targetName
		{
			get;
			set;
		}
			
		GameObject target
		{
			get;
			set;
		}
			
		XHash source
		{
			set;
		}
			
		XHash destination
		{
			set;
		}
			
		void setSourceValue( string propertyName, float value, bool isRelative = false);		
		void setDestinationValue( string propertyName, float value, bool isRelative = false);
		object getObject( string propertyName );
		void setObject( string propertyName, object value );
		void update( float factor );
		IUpdater clone();
	}
}