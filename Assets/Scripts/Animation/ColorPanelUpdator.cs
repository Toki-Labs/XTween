/**********************************************************************************
/*		File Name 		: ColorPanelUpdator.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-22
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using System.Collections;

public class ColorPanelUpdator : IColorUpdatable
{
#if NGUI
    private UIPanel _panel;
#endif
    private Color _color = new Color();

    public void Initialize( GameObject target )
    {
#if NGUI
        this._panel = target.GetComponent<UIPanel>();
#endif
    }

    public Color GetColor()
    {
#if NGUI
        this._color.a = this._panel.alpha;
#endif
        return this._color;
    }

    public void SetColor(Color color)
    {
#if NGUI
        this._panel.alpha = color.a;
#endif
    }
}