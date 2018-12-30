using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BezierUpdater : DisplayUpdater
{
	protected XPoint _controlPoint;
		
	public XPoint controlPoint
    {
        set { _controlPoint = value; }
    }
    
    private float Calcurate( float[] cpVec, float source, float finish )
    {
        float l;
        int ip;
        float it;
        float p1;
        float p2;
        float result;

        if (_factor != 1.0)
        {
            if ((l = cpVec.Length) == 1)
            {
                result = source + _factor * (2 * _invert * (cpVec[0] - source) + _factor * (finish - source));
            }
            else
            {
                ip = (int)(_factor * l) >> 0;
                it = (_factor - (ip * (1 / l))) * l;
                if (ip == 0)
                {
                    p1 = source;
                    p2 = (cpVec[0] + cpVec[1]) * 0.5f;
                }
                else if (ip == (l - 1))
                {
                    p1 = (cpVec[ip - 1] + cpVec[ip]) * 0.5f;
                    p2 = finish;
                }
                else
                {
                    if( ip >= l )
                    {
                        ip = (int)l - 1;
                        return source + _factor * (2 * _invert * (cpVec[ip] - source) + _factor * (finish - source));
                    }
                    else
                    {
                        p1 = (cpVec[ip - 1] + cpVec[ip]) * 0.5f;
                        p2 = (cpVec[ip] + cpVec[ip + 1]) * 0.5f;
                    }
                }
                result = p1 + it * (2 * (1 - it) * (cpVec[ip] - p1) + it * (p2 - p1));
            }
        }
        else
        {
            result = source * _invert + finish * _factor;
        }
        return result;
    }
    protected override Action GetUpdateX() { if(_controlPoint.x == null) return base.UpdateX; else return this.UpdateX; }
	protected override Action GetUpdateY() { if(_controlPoint.y == null) return base.UpdateY; else return this.UpdateY; }
	protected override Action GetUpdateZ() { if(_controlPoint.z == null) return base.UpdateZ; else return this.UpdateZ; }
    protected override Action GetUpdateScaleX() { if(_controlPoint.scaleX == null) return base.UpdateScaleX; else return this.UpdateScaleX; }
	protected override Action GetUpdateScaleY() { if(_controlPoint.scaleY == null) return base.UpdateScaleY; else return this.UpdateScaleY; }
	protected override Action GetUpdateScaleZ() { if(_controlPoint.scaleZ == null) return base.UpdateScaleZ; else return this.UpdateScaleZ; }
    protected override Action GetUpdateRotationX() { if(_controlPoint.rotationX == null) return base.UpdateRotationX; else return this.UpdateRotationX; }
	protected override Action GetUpdateRotationY() { if(_controlPoint.rotationY == null) return base.UpdateRotationY; else return this.UpdateRotationY; }
	protected override Action GetUpdateRotationZ() { if(_controlPoint.rotationZ == null) return base.UpdateRotationZ; else return this.UpdateRotationZ; }
    protected override void UpdateX()
    {
        _pos.x = this.Calcurate( _controlPoint.x, _sPos.x, _dPos.x );
    }
    protected override void UpdateY()
    {
        _pos.y = this.Calcurate( _controlPoint.y, _sPos.y, _dPos.y );
    }
    protected override void UpdateZ()
    {
        _pos.z = this.Calcurate( _controlPoint.z, _sPos.z, _dPos.z );
    }
    protected override void UpdateScaleX()
    {
        _sca.x = this.Calcurate( _controlPoint.scaleX, _sSca.x, _dSca.x );
    }
    protected override void UpdateScaleY()
    {
        _sca.y = this.Calcurate( _controlPoint.scaleY, _sSca.y, _dSca.y );
    }
    protected override void UpdateScaleZ()
    {
        _sca.z = this.Calcurate( _controlPoint.scaleZ, _sSca.z, _dSca.z );
    }
    protected override void UpdateRotationX()
    {
        _rot.x = this.Calcurate( _controlPoint.rotationX, _sRot.x, _dRot.x );
    }
    protected override void UpdateRotationY()
    {
        _rot.y = this.Calcurate( _controlPoint.rotationY, _sRot.y, _dRot.y );
    }
}

