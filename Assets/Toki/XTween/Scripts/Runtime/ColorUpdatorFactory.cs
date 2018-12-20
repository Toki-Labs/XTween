/**********************************************************************************
/*		File Name 		: ColorUpdatorFactory.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-22
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ColorUpdatorFactory
{
    private delegate IColorUpdatable GetUpdator();
    private static Dictionary<Type,GetUpdator> map = new Dictionary<Type,GetUpdator>{
        {typeof(SpriteRenderer),GetSpriteUpdator}
        /*,{typeof(UIWidget),GetWidgetUpdator}
        ,{typeof(UIPanel),GetPanelUpdator}*/
    };

	public static IColorUpdatable Create( Type type, GameObject target )
    {
        IColorUpdatable updator = null;
        if( map.ContainsKey(type) )
        {
            Component comp = target.GetComponent(type);
            if( comp != null )
            {
                updator = map[type].Invoke();
                updator.Initialize( target );
            }
        }
        return updator;
    }

    public static IColorUpdatable Find( GameObject target )
    {
        Type[] keys = XTween.GetArrayFromCollection<Type>( map.Keys );
        int length = keys.Length;
        Component comp;
        IColorUpdatable updator = null;
        for ( int i = 0; i < length; ++i )
        {
            comp = target.GetComponent( keys[i] );
            if( comp != null )
            {
                updator = map[keys[i]].Invoke();
                updator.Initialize( target );
                break;
            }
        }
        return updator;
    }

    private static IColorUpdatable GetSpriteUpdator()
    {
        return new ColorSpriteUpdator();
    }

    private static IColorUpdatable GetWidgetUpdator()
    {
        return new ColorWidgetUpdator();
    }

    private static IColorUpdatable GetPanelUpdator()
    {
        return new ColorPanelUpdator();
    }
	
}