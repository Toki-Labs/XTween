/**********************************************************************************
/*		File Name 		: XControlPoint.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-21
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using System.Collections;

public struct XPoint
{
	/************************************************************************
	 *	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
	 ************************************************************************/
    private float[] _x;
    private float[] _y;
    private float[] _z;
    private float[] _scaleX;
    private float[] _scaleY;
    private float[] _scaleZ;
    private float[] _rotationX;
    private float[] _rotationY;
    private float[] _rotationZ;
    private float[] _red;
    private float[] _green;
    private float[] _blue;
    private float[] _alpha;
	
	
	/************************************************************************
	 *	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	 ************************************************************************/
	
	
	/************************************************************************
	 *	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	 ************************************************************************/
	
	
	/************************************************************************
	 *	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
	 ************************************************************************/
    public static XPoint New
    {
        get
        {
            XPoint controlPoint = new XPoint();
            return controlPoint;
        }
    }

    public float[] x
    {
        get
        {
            return this._x;
        }
    }
    public float[] y
    {
        get
        {
            return this._y;
        }
    }
    public float[] z
    {
        get
        {
            return this._z;
        }
    }
    public float[] scaleX
    {
        get
        {
            return this._scaleX;
        }
    }
    public float[] scaleY
    {
        get
        {
            return this._scaleY;
        }
    }
    public float[] scaleZ
    {
        get
        {
            return this._scaleZ;
        }
    }
    public float[] rotationX
    {
        get
        {
            return this._rotationX;
        }
    }
    public float[] rotationY
    {
        get
        {
            return this._rotationY;
        }
    }
    public float[] rotationZ
    {
        get
        {
            return this._rotationZ;
        }
    }
    public float[] red
    {
        get
        {
            return this._red;
        }
    }
    public float[] green
    {
        get
        {
            return this._green;
        }
    }
    public float[] blue
    {
        get
        {
            return this._blue;
        }
    }
    public float[] alpha
    {
        get
        {
            return this._alpha;
        }
    }
	
	/************************************************************************
	 *	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
	 ************************************************************************/
	
	
	/************************************************************************
	 *	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
	 ************************************************************************/

	
	/************************************************************************
	 *	 	 	 	 	Coroutine Declaration	 	  			 	 		*
	 ************************************************************************/
	
	
	/************************************************************************
	 *	 	 	 	 	Private Method Declaration	 	 	 	 	 		*
	 ************************************************************************/
	
	
	/************************************************************************
	 *	 	 	 	 	Protected Method Declaration	 	 	 	 	 	*
	 ************************************************************************/
	
	
	/************************************************************************
	 *	 	 	 	 	Public Method Declaration	 	 	 	 	 		*
	 ************************************************************************/
    public XPoint AddX( params float[] values )
    {
        this._x = values;
        return this;
    }
    public XPoint AddY(params float[] values)
    {
        this._y = values;
        return this;
    }
    public XPoint AddZ(params float[] values)
    {
        this._z = values;
        return this;
    }
    public XPoint AddScaleX(params float[] values)
    {
        this._scaleX = values;
        return this;
    }
    public XPoint AddScaleY(params float[] values)
    {
        this._scaleY = values;
        return this;
    }
    public XPoint AddScaleZ(params float[] values)
    {
        this._scaleZ = values;
        return this;
    }
    public XPoint AddRotationX(params float[] values)
    {
        this._rotationX = values;
        return this;
    }
    public XPoint AddRotationY(params float[] values)
    {
        this._rotationY = values;
        return this;
    }
    public XPoint AddRotationZ(params float[] values)
    {
        this._rotationZ = values;
        return this;
    }
    public XPoint AddRed(params float[] values)
    {
        this._red = values;
        return this;
    }
    public XPoint AddGreen(params float[] values)
    {
        this._green = values;
        return this;
    }
    public XPoint AddBlue(params float[] values)
    {
        this._blue = values;
        return this;
    }
    public XPoint AddAlpha(params float[] values)
    {
        this._alpha = values;
        return this;
    }
	
	
}