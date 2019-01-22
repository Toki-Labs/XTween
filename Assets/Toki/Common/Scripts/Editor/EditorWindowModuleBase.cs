/**********************************************************************************
/*		File Name 		: EditorWindowModuleBase.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-27
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EditorWindowModuleBase
{
	/************************************************************************
	 *	 	 	 	 	Static Variable Declaration	 	 	 	 	 	    *
	 ************************************************************************/
	
	
	/************************************************************************
	 *	 	 	 	 	Static Method Declaration	 	 	 	     	 	*
	 ************************************************************************/
	
	
	/************************************************************************
	 *	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
	 ************************************************************************/
	
	
	/************************************************************************
	 *	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	 ************************************************************************/
    protected EditorWindowBase _window;
    protected Dictionary<KeyCode, Action> _keyUpListenerDic = new Dictionary<KeyCode, Action>();
    protected Dictionary<KeyCode, Action> _keyDownListenerDic = new Dictionary<KeyCode, Action>();

    /************************************************************************
	 *	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	 ************************************************************************/


    /************************************************************************
	 *	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
	 ************************************************************************/
    public virtual string ModuleName
    {
        get
        {
            return "";
        }
    }
	
	
	/************************************************************************
	 *	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
	 ************************************************************************/
	
	
	/************************************************************************
	 *	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
	 ************************************************************************/
	public virtual void Initialize( EditorWindowBase window )
	{
        this._window = window;
	}

    public virtual void OnEnable()
	{

	}

    public void OnEvent()
    {
        Event e = Event.current;
        
        switch (e.type)
        {
            case EventType.KeyDown:
                foreach( KeyValuePair<KeyCode, Action> entry in this._keyDownListenerDic )
                {
                    if( Event.current.keyCode == entry.Key )
                    {
                        entry.Value();
                    }
                }
                break;
            case EventType.KeyUp:
                foreach( KeyValuePair<KeyCode, Action> entry in this._keyUpListenerDic )
                {
                    if( Event.current.keyCode == entry.Key )
                    {
                        entry.Value();
                    }
                }
                break;
        }   
    }

    public virtual void OnGUI()
	{
        
	}

    public virtual void OnDestroy()
	{
		
	}

    public virtual void OnDataReady()
    {

    }

    /************************************************************************
	 *	 	 	 	 	Coroutine Declaration	 	  			 	 		*
	 ************************************************************************/


    /************************************************************************
	 *	 	 	 	 	Private Method Declaration	 	 	 	 	 		*
	 ************************************************************************/


    /************************************************************************
	 *	 	 	 	 	Protected Method Declaration	 	 	 	 	 	*
	 ************************************************************************/
    protected void AddKeyEventListener( Action listener, KeyCode keyCode, bool isDown )
    {
        Dictionary<KeyCode, Action> target = (isDown) ? this._keyDownListenerDic : this._keyUpListenerDic;
        if( !target.ContainsKey(keyCode) )
        {
            target[keyCode] = null;
        }
        target[keyCode] += listener;
    }

    protected void RemoveKeyEventListener( Action listener, KeyCode keyCode, bool isDown )
    {
        Dictionary<KeyCode, Action> target = (isDown) ? this._keyDownListenerDic : this._keyUpListenerDic;
        target[keyCode] -= listener;

        if( target[keyCode] == null )
        {
            target.Remove(keyCode);
        }
    }
	
	
	/************************************************************************
	 *	 	 	 	 	Public Method Declaration	 	 	 	 	 		*
	 ************************************************************************/
	
	
}