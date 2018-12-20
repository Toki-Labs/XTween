/**********************************************************************************
/*		File Name 		: XTweenEditorManager.cs
/*		Author 			: R0biN
/*		Description 	: 
/*		Created Date 	: 2018-10-21
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;


[InitializeOnLoad]
public class XTweenEditorManager
{
	/************************************************************************
	*	 	 	 	 	Static Variable Declaration	 	 	 	 	 	    *
	************************************************************************/
	private static bool _initialized = false;
	private static XTweenEditorManager _instance;
	
	/************************************************************************
	*	 	 	 	 	Static Method Declaration	 	 	 	     	 	*
	************************************************************************/
	static XTweenEditorManager()
	{
		Instance.Initialize();
	}
	
	public static XTweenEditorManager Instance
	{
		get
		{
			if( _instance == null )
			{
				_instance = new XTweenEditorManager();
			}
			return _instance;
		}
	}
	/************************************************************************
	*	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
	************************************************************************/
	private PlayModeStateChange _playMode;
	
	/************************************************************************
	*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
	************************************************************************/
	public bool Initialized
	{
		get
		{
			return _initialized;
		}
	}


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
	*	 	 	 	 	Event Method Declaration	 	 	 	     	 	*
	************************************************************************/
	private void ChangedPlayMode( PlayModeStateChange state )
	{
		if( this._playMode != state )
		{
			bool send = false;
			switch ( state )
			{
				case PlayModeStateChange.EnteredEditMode:
				case PlayModeStateChange.EnteredPlayMode:
					send = true;
					break;
                default:
                    break;
			}

			this._playMode = state;
			if( send ) XTween.PlayModeChanged( this._playMode.Equals(PlayModeStateChange.EnteredEditMode) );
		}
    }

	//============================== Ani ====================================
	//============================== Net ====================================
	//============================== UI =====================================
	
	/************************************************************************
	*	 	 	 	 	Public Method Declaration	 	 	 	 	 		*
	************************************************************************/
	public void Initialize()
	{
		if( !_initialized )
		{
			_initialized = true;

            EditorApplication.playModeStateChanged += this.ChangedPlayMode;
		}
	}
}