/**********************************************************************************
/*		File Name 		: ColorSpriteUpdator.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-22
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using System.Collections;

public class ColorSpriteUpdator : IColorUpdatable
{
    private SpriteRenderer _sprite;

    public void Initialize( GameObject target )
    {
        this._sprite = target.GetComponent<SpriteRenderer>();
    }

    public Color GetColor()
    {
        return this._sprite.color;
    }

    public void SetColor(Color color)
    {
        this._sprite.color = color;
    }
}