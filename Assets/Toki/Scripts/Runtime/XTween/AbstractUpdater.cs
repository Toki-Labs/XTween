using UnityEngine;
using System.Collections;

public class AbstractUpdater : IUpdating
{
	public virtual GameObject target
	{
		get { return null; }
		set {}
	}
		
	public virtual IClassicHandlable start
	{
		set {}
	}
		
	public virtual IClassicHandlable finish
	{
		set {}
	}

    public virtual void Updating( float factor )
	{
		UpdateObject(factor);
	}
		
	public virtual void ResolveValues()
	{
		
	}
		
	protected virtual void UpdateObject( float factor )
	{
			
	}
		
	public virtual IUpdating Clone()
	{
		AbstractUpdater instance = NewInstance();
		if (instance != null) {
			instance.CopyFrom(this);
		}
		return instance;
	}
		
	protected virtual AbstractUpdater NewInstance()
	{
		return null;
	}
		
	protected virtual void CopyFrom( AbstractUpdater source )
	{
		// Do NOT copy _isResolved property.
	}
}

