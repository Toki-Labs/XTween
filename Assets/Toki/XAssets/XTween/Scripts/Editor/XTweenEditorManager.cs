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

		public static void UpdateEasingName()
		{
			List<EasingData> easingList = XTweenEditorData.Instance.easingDataList;
			List<string> easingNameList = new List<string>();
			easingList.ForEach(x => easingNameList.Add(x.name));
			string[] names = easingNameList.ToArray();
			string filePath = SystemUtil.AbsPath + "/Assets/Toki/XAssets/XTween/Scripts/EaseCustom.cs";

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
				string path = SystemUtil.AbsPath + "/Assets/Toki/XAssets/XTween/Scripts/Editor/EaseCustomTemplete";
				string content = FileReference.Read(path);
				content = content.Replace("/* Name List */", replaceStr);
				FileReference.Write(filePath, content);
				XTweenEditorData.Instance.Save();
				AssetDatabase.Refresh();
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

		/************************************************************************
		*	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
		************************************************************************/
		public XTweenEditorManager()
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
			if( !File.Exists(SystemUtil.AbsPath + "/" + destPath) )
			{
				Directory.CreateDirectory(SystemUtil.AbsPath + "/" + destDir);
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
	}
}