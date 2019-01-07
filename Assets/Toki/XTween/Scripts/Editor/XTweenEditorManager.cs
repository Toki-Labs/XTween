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

namespace Toki.Tween
{
	[Serializable]
	public class XTweenConfigData
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

		public static string TempPath
		{
			get
			{
				return Path.Combine(AbsPath, "XTweenTemp");
			}
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
	#else
				return Application.dataPath;
	#endif
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

		public static void UpdateEasingName()
		{
			string tempNamePath = Path.Combine(TempPath, "EaseCustomTemp");
			string nameStartPath = XTweenEditorManager.AbsPath + "/Assets/Toki/XTween/Scripts/EaseCustom.cs";
			if( File.Exists(tempNamePath) )
			{
				File.Copy(tempNamePath, nameStartPath, true);
				File.Delete(tempNamePath);
				return;
			}

			List<EasingData> easingList = XTweenEditorData.Instance.easingDataList;
			List<string> easingNameList = new List<string>();
			easingList.ForEach(x => easingNameList.Add(x.name));
			string[] names = easingNameList.ToArray();
			string filePath = AbsPath + "/Assets/Toki/XTween/Scripts/EaseCustom.cs";

			bool refresh = false;
			if( File.Exists(filePath) )
			{
				List<string> scriptEnumList = new List<string>(XTweenDataUtil.GetEnumNameList<EaseCustom>());
				if( scriptEnumList.Count == easingNameList.Count )
				{
					int length = easingNameList.Count;
					for ( int i = 0; i < length; ++i )
					{
						if( !scriptEnumList.Contains(easingNameList[i]) )
						{
							refresh = true;
							break;
						}
					}
				}
				else
				{
					refresh = true;
				}
			}
			else
			{
				refresh = true;
			}

			if( refresh )
			{
				string replaceStr = string.Join(",\n\t", names);
				string path = AbsPath + "/Assets/Toki/XTween/Scripts/Editor/EaseCustomTemplete";
				string content = ReadText(path);
				content = content.Replace("/* Name List */", replaceStr);
				WriteText(filePath, content);
				XTweenEditorData.Instance.Save();
				AssetDatabase.Refresh();
			}
		}

		/************************************************************************
		*	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
		************************************************************************/
		private PlayModeStateChange _playMode;
		private XTweenConfigData _data;
		
		/************************************************************************
		*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
		************************************************************************/
		
		/************************************************************************
		*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
		************************************************************************/
		public Action initializeListener;
		
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

		public XTweenConfigData Data
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
                return XTweenEditorManager.AbsPath + "/Assets/Toki/XTween/Scripts/Editor/xtween_config.json";
            }
        }

		/************************************************************************
		*	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
		************************************************************************/
		public XTweenEditorManager()
		{
			this.Load();
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

		private void CheckEditorData()
		{
			string destDir = "Assets/Resources";
			string destPath = destDir + "/XTweenData.asset";
			if( !File.Exists(AbsPath + "/" + destPath) )
			{
				Directory.CreateDirectory(AbsPath + "/" + destDir);
				XTweenEditorData asset = ScriptableObject.CreateInstance<XTweenEditorData>();
				AssetDatabase.CreateAsset (asset, destPath);
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}

			UpdateEasingName();
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
				this.CheckEditorData();
			}
		}

		public void Load()
        {
            if( File.Exists(this.JsonPath) )
            {
                string jsonStr = XTweenEditorManager.ReadText(this.JsonPath);
                this._data = JsonUtility.FromJson<XTweenConfigData>(jsonStr);
            }
            else
            {
                this._data = new XTweenConfigData();
                this.Save();
            }
        }
		
		public void Save()
        {
            string jsonStr = JsonUtility.ToJson(this._data);
            XTweenEditorManager.WriteText(this.JsonPath, jsonStr);
        }
	}
}