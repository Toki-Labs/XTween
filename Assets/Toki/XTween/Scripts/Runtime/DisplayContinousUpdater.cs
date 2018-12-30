using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
	
public class DisplayContinousUpdater : DisplayUpdater
{
	public ITimer ticker;
	public int frameSkip;
	private float _deltaTime;

	public override void ResolveValues()
	{
		base.ResolveValues();
	}

	public override void Updating( float factor )
	{
		_factor = factor;
		this._deltaTime = ticker.GetDeltaTime( frameSkip );
		if (this._target == null)
        {
            this._stopOnDestroyHandler.Invoke();
            this.Dispose();
        }
        else
        {
            for (int i = 0; i < this._updateCount; ++i)
            {
                this._updateList[i].Invoke();
            }
        }
	}

    protected override void UpdateX()
	{
		_pos.x += _dPos.x * _factor * _deltaTime;
	}
	protected override void UpdateY()
	{
		_pos.y += _dPos.y * _factor * _deltaTime;
	}
	protected override void UpdateZ()
	{
		_pos.z += _dPos.z * _factor * _deltaTime;
	}
	protected override void UpdateScaleX()
	{
		_sca.x += _dSca.x * _factor * _deltaTime;
	}
	protected override void UpdateScaleY()
	{
		_sca.y += _dSca.y * _factor * _deltaTime;
	}
	protected override void UpdateScaleZ()
	{
		_sca.z += _dSca.z * _factor * _deltaTime;
	}
	protected override void UpdateRotationX()
	{
		_rot.x += _dRot.x * _factor * _deltaTime;
	}
	protected override void UpdateRotationY()
	{
		_rot.y += _dRot.y * _factor * _deltaTime;
	}
	protected override void UpdateRotationZ()
	{
		_rot.z += _dRot.z * _factor * _deltaTime;
	}
}