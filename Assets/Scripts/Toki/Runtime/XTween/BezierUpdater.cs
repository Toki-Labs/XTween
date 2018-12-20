using UnityEngine;
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
                ip = (int)(_factor * l);
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
                    p1 = (cpVec[ip - 1] + cpVec[ip]) * 0.5f;
                    p2 = (cpVec[ip] + cpVec[ip + 1]) * 0.5f;
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
    protected override void UpdateRotationZ()
    {
        _rot.z = this.Calcurate( _controlPoint.rotationZ, _sRot.z, _dRot.z );
    }
    protected override void UpdateColorRed()
    {
        _col.r = this.Calcurate( _controlPoint.red, _sColor.r, _dColor.r );
    }
    protected override void UpdateColorGreen()
    {
        _col.g = this.Calcurate( _controlPoint.green, _sColor.g, _dColor.g );
    }
    protected override void UpdateColorBlue()
    {
        _col.b = this.Calcurate( _controlPoint.blue, _sColor.b, _dColor.b );
    }
    protected override void UpdateColorAlpha()
    {
        _col.a = this.Calcurate( _controlPoint.alpha, _sColor.a, _dColor.a );
    }
		
	protected override AbstractUpdater NewInstance() 
	{
        BezierUpdater updater = new BezierUpdater();
        updater._start = this._start;
        updater._finish = this._finish;
        return updater;
	}
		
	protected override void CopyFrom( AbstractUpdater source )
	{
		base.CopyFrom(source);
			
		BezierUpdater obj = source as BezierUpdater;
        this._target = obj._target;
        this._start = obj._start;
        this._finish = obj._finish;
        this._controlPoint = obj._controlPoint;
        this._stopOnDestroyHandler = obj._stopOnDestroyHandler;
	}

    public override IUpdating Clone()
    {
        BezierUpdater instance = new BezierUpdater();
        instance.start = this._start;
        instance.finish = this._finish;
        instance.controlPoint = this._controlPoint;
        return base.Clone();
    }
}

