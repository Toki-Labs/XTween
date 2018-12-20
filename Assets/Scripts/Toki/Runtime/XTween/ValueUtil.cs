using UnityEngine;
using System.Collections;

public class ValueUtil
{
	public ValueUtil( GameObject target )
	{
		_target = target;
		_localPos = _target.transform.localPosition;
	}
		
	private GameObject _target;
	private Vector3 _localPos;
		
	public float getValue( XProperty propertyName )
	{
		if( propertyName == XProperty.x )
		{
			return _localPos.x;
		}
		if( propertyName == XProperty.y )
		{
			return _localPos.y;
		}
		if( propertyName == XProperty.z )
		{
			return _localPos.z;
		}
		return 0f;
	}
		
	public void setValue( XProperty propertyName, float value )
	{
		if( propertyName == XProperty.x )
		{
			_localPos.x = value;
			_target.transform.localPosition = _localPos;
		}
		if( propertyName == XProperty.y )
		{
			_localPos.y = value;
			_target.transform.localPosition = _localPos;
		}
		if( propertyName == XProperty.z )
		{
			_localPos.z = value;
			_target.transform.localPosition = _localPos;
		}
	}
}