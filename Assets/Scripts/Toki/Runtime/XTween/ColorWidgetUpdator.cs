/**********************************************************************************
/*		File Name 		: ColorWidgetUpdator.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-22
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using System.Collections;

public class ColorWidgetUpdator : IColorUpdatable
{
#if NGUI
    private UIWidget _widget;
#endif

    public void Initialize( GameObject target )
    {
#if NGUI
        this._widget = target.GetComponent<UIWidget>();
#endif
    }

    public Color GetColor()
    {
#if NGUI
        return this._widget.color;
#else
        return new Color();
#endif
    }

    public void SetColor(Color color)
    {
#if NGUI
        this._widget.color = color;
        
#endif
    }
}