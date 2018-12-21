/**********************************************************************************
/*		File Name 		: XTweenEditorManager.cs
/*		Author 			: R0biN
/*		Description 	: 
/*		Created Date 	: 2018-10-21
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;

[Serializable]
public class XTweenData
{
	public string version;
}

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

	public static string AbsPath
	{
		get
		{
#if UNITY_EDITOR
			string absPath = Application.dataPath;
			return absPath.Substring( 0, absPath.LastIndexOf( "/" ) );
#endif
			return Application.dataPath;
		}
	}

	public static string ReadText( string path )
	{
		return File.ReadAllText(path);
	}

	public static void WriteText( string path, string content )
	{
		StreamWriter writer = File.CreateText(path);
        writer.Write(content);
        writer.Close();
	}
	/************************************************************************
	*	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
	************************************************************************/
	private XTweenData _data;
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

	public XTweenData Data
    {
        get
        {
            return _data;
        }
    }

	public string JsonPath
    {
        get
        {
            return AbsPath + "/Assets/Toki/XTween/Export/Scripts/Editor/xtween_config.json";
        }
    }

	public static string ExportDefaultPath
    {
		get
		{
			string path = "";
			if (Application.platform == RuntimePlatform.WindowsEditor)
			{
				path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}
			else
			{
				path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Documents";
			}
			return path;
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
	private void Load()
    {
        if( File.Exists(this.JsonPath) )
        {
            string jsonStr = ReadText(this.JsonPath);
            this._data = JsonUtility.FromJson<XTweenData>(jsonStr);
        }
        else
        {
            this._data = new XTweenData();
            this.Save();
        }
    }
	
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
			this.Load();
		}
	}

    public void Save()
    {
        string jsonStr = JsonUtility.ToJson(this._data);
        WriteText(this.JsonPath, jsonStr);
    }

	
}