/**********************************************************************************
/*		File Name 		: XTweenExporter.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-27
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class XTweenEditorWindow : EditorWindow
	{
		/************************************************************************
		*	 	 	 	 	Static Variable Declaration	 	 	 	 	 	    *
		************************************************************************/
		
		/************************************************************************
		*	 	 	 	 	Static Method Declaration	 	 	 	     	 	*
		************************************************************************/
		[MenuItem("Window/XTween Editor #%9",priority=16)]
		public static void OpenXTweenEditorWindow()
		{
			EditorWindow.GetWindow<XTweenEditorWindow>(false, "XTween Editor", true);
		}

		public static bool DrawHeader (string text, bool forceOn) { return DrawHeader(text, text, forceOn); }
	
		public static bool DrawHeader (string text, string key, bool forceOn)
		{
			GUILayout.BeginVertical();
			bool state = EditorPrefs.GetBool(key, true);
			
			//GUILayout.Space(3f);
			if (!forceOn && !state) GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
			GUILayout.BeginHorizontal();
			//GUILayout.Space(3f);
			
			GUI.changed = false;
			if (!GUILayout.Toggle(true, "<b><size=12>" + text + "</size></b>", "dragtab"))
			{
				state = !state;
			}
			if (GUI.changed) EditorPrefs.SetBool(key, state);
			
			GUILayout.EndHorizontal();
			GUI.backgroundColor = Color.white;
			if (!forceOn && !state) GUILayout.Space(3f);
			GUILayout.EndVertical();
			return state;
		}
		
		/************************************************************************
		*	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
		************************************************************************/
		private const string NETWORK_ERROR_MSG = "Some with wrong in check update.";
		private const string INIT_MSG = "Checking version...";
		private XTweenVersionController _versionController;
		private XTweenEditorData _data;
		private int _easingIndex = -1;
		private Vector2 _easingScroll = Vector2.zero;
		private string _easingName = "";
		private AnimationCurve _easingCurve;
		private bool _checkResult;
		private string _lastVersion = INIT_MSG;
		private bool _checkForce = false;
		
		/************************************************************************
		*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
		************************************************************************/
			
		/************************************************************************
		*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
		************************************************************************/
			
		/************************************************************************
		*	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
		************************************************************************/
		private AnimationCurve EasingDefault
		{
			get
			{
				return new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
			}
		}
		
		/************************************************************************
		*	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
		************************************************************************/
		
		/************************************************************************
		*	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
		************************************************************************/
		void OnEnable()
		{
			this._versionController = new XTweenVersionController(this.CheckedLastVersion);
			if( !EditorApplication.isPlaying ) this._versionController.Check();
			if( XTweenEditorManager.Instance.Initialized )
			{
				this.ReadData();
			}
			else
			{
				XTweenEditorManager.Instance.initializeListener += this.ReadData;
			}
		}
		
		/************************************************************************
		*	 	 	 	 	Coroutine Declaration	 	  			 	 		*
		************************************************************************/
		void OnGUI()
		{
			GUILayout.BeginVertical();

			GUILayout.Height(20f);
			GUILayout.BeginVertical("Box");
			{
				GUILayout.Space(3f);
				GUILayout.Label("Update check", "BoldLabel");
				GUILayout.Space(3f);
			}
			GUILayout.EndVertical();

			GUILayout.Space(0f);
			GUILayout.BeginVertical("Box");
			GUILayout.Space(5f);

			string currentVersion = XTweenEditorManager.Instance.Data.version;
			GUILayout.BeginHorizontal();
			GUILayout.Label("Current version", GUILayout.Width(110f));
			GUILayout.Label(currentVersion, "BoldLabel");
			GUILayout.EndHorizontal();

			if( !EditorApplication.isPlaying )
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label("Lastest version", GUILayout.Width(110f));
				GUILayout.Label(this._lastVersion, "BoldLabel");
				GUILayout.EndHorizontal();

				GUILayout.Space(10f);
				int currentVersionInt = int.Parse(currentVersion.Replace(".", ""));
				int lastVersionInt = int.Parse(this._lastVersion.Replace(".", ""));
				if( lastVersionInt < currentVersionInt && 
					!this._versionController.IsChecking &&
					!this._versionController.IsDownloading )
				{
					this._versionController.Check(true);
				}
				this._checkResult = this._lastVersion != NETWORK_ERROR_MSG && 
									this._lastVersion != INIT_MSG &&
									lastVersionInt > currentVersionInt;
				if( this._checkResult )
				{
					GUI.backgroundColor = this._versionController.IsDownloading ? Color.gray : Color.green;
					if( GUILayout.Button("Update to lastest version", GUILayout.Height(30f)) && !this._versionController.IsDownloading )
					{
						if( EditorUtility.DisplayDialog("Update", "Do you wanna update to lastest version?", "Yes", "No") )
						{
							this._versionController.Update();
						}
					}
					GUI.backgroundColor = Color.white;
				}
				else
				{
					bool enableButton = !this._versionController.IsChecking && !this._versionController.IsDownloading;
					GUI.backgroundColor = enableButton ? Color.white : Color.gray;
					if(GUILayout.Button("Check Update", GUILayout.Height(30f)) && enableButton )
					{
						this._checkForce = true;
						this._versionController.Check(true);
					}
					GUI.backgroundColor = Color.white;
				}
				GUILayout.Space(10f);
			}
			else
			{
				GUILayout.Space(70f);
			}
			GUILayout.EndVertical();
			GUILayout.Space(10f);
			GUILayout.Height(20f);
			GUILayout.BeginVertical("Box");
			{
				GUILayout.Space(3f);
				GUILayout.Label("Easing", "BoldLabel");
				GUILayout.Space(3f);
			}
			GUILayout.EndVertical();

			GUILayout.Space(0f);

			GUILayout.BeginVertical("Box");
			GUILayout.Space(5f);

			//Contents
			List<EasingData> easeList = this._data.easingDataList;

			DrawHeader("Custom Easing List", true);
            {
                GUILayout.Space(-4f);
				GUI.backgroundColor = Color.white;
                GUILayout.BeginVertical("Box", GUILayout.Height(110f));
                this._easingScroll = EditorGUILayout.BeginScrollView(this._easingScroll, GUILayout.MaxHeight(110f));
				GUILayout.Space(0f);
                int length = easeList.Count;
				if( length > 0 )
				{
					for (int i = 0; i < length; ++i)
					{
						EasingData data = this._data.easingDataList[i];
						GUILayout.Space(-1f);
						GUI.color = (this._easingIndex == i) ? Color.grey : Color.white;
						GUILayout.BeginHorizontal("Box", GUILayout.Height(25f));
						bool clicked0 = GUILayout.Button((i + 1).ToString(), "BoldLabel", GUILayout.MaxWidth(30f));
						bool clicked1 = GUILayout.Button( data.name, "BoldLabel");
						if ( clicked0 || clicked1 )
						{
							this._easingIndex = i;
							this._easingName = data.name;
							this._easingCurve = data.animationCurve;
							GUIUtility.keyboardControl = 0;
						}
						// GUI.backgroundColor = this._easingIndex == i ? Color.white : Color.red;
						if( GUILayout.Button("X", GUILayout.Width(20f)) )
						{
							this._data.easingDataList.RemoveAt(i);
							XTweenEditorManager.UpdateEasingName();
						}
						// GUI.backgroundColor = Color.white;
						
						GUILayout.EndHorizontal();
						GUILayout.Space(-4f);
						GUI.color = Color.white;
					}
				}
                EditorGUILayout.EndScrollView();
                GUILayout.EndVertical();
            }
			GUILayout.Space(10f);

			bool isNew = this._easingIndex == -1;
			DrawHeader(isNew ? "Create New Easing" : "Modify Easing", true);
			GUILayout.Space(-4f);
			GUILayout.BeginVertical("Box");
			GUILayout.Space(5f);
			EditorGUIUtility.labelWidth = 110f;

			this._easingName = EditorGUILayout.TextField("Name", this._easingName, GUILayout.Width(300f)).Replace(" ", "");
			this._easingCurve = EditorGUILayout.CurveField("Easing Graph", this._easingCurve, GUILayout.Width(300f), GUILayout.Height(185f));
			Keyframe[] keys = this._easingCurve.keys;
			int keyLength = keys.Length;
			int keyLast = keyLength - 1;
			if( keyLength > 1 )
			{
				Keyframe frameFirst = keys[0];
				if( frameFirst.value != 0f || frameFirst.time != 0f )
				{
					frameFirst.value = frameFirst.time = 0f;
					keys[0] = frameFirst;
				}
				
				Keyframe frameLast = keys[keyLast];
				if( frameLast.value != 1f || frameLast.time != 1f )
				{
					float rateValue = 1f / frameLast.value;
					float rateTime = 1f / frameLast.time;
					for ( int i = 0; i < keyLength; ++i )
					{
						Keyframe frame = keys[i];
						if( i == keyLast )
						{
							frame.value = frame.time = 0f;
						}
						else
						{
							frame.value = frame.value * rateValue;
							frame.time = frame.time * rateTime;
						}
						keys[i] = frame;
					}
				}
				
				this._easingCurve.keys = keys;
			}
			else
			{
				this._easingCurve = this.EasingDefault;
			}
						
			GUILayout.Space(10f);
			string validatedMsg = this.ValidateEasing();
			bool validated = validatedMsg == null;
			if( !validated )
			{
				EditorGUILayout.HelpBox(validatedMsg, MessageType.Error);
			}

			if( isNew )
			{
				//Create
				GUI.backgroundColor = validated ? Color.cyan : Color.gray;
				if(GUILayout.Button("Add Easing", GUILayout.Height(30f)) && validated)
				{
					EasingData data = new EasingData();
					data.name = this._easingName;
					data.animationCurve = this._easingCurve;
					this._data.easingDataList.Add(data);
					XTweenEditorManager.UpdateEasingName();
					this.InitUI();
				}
				GUI.backgroundColor = Color.white;
			}
			else
			{
				//Modify
				GUILayout.BeginHorizontal();
				GUI.backgroundColor = validated ? Color.green : Color.gray;
				if(GUILayout.Button("Save Easing", GUILayout.Height(30f)) && validated)
				{
					EasingData data = this._data.easingDataList[this._easingIndex];
					string beforeName = data.name;
					data.name = this._easingName;
					data.animationCurve = this._easingCurve;
					if( beforeName != this._easingName )
						XTweenEditorManager.UpdateEasingName();
					XTweenEditorData.Instance.Save();
					GUIUtility.keyboardControl = 0;
				}
				GUI.backgroundColor = Color.white;
				if(GUILayout.Button("Create New", GUILayout.Height(30f)))
				{
					this.InitUI();
				}
				GUILayout.EndHorizontal();
			}

			GUILayout.Space(5f); 
			GUILayout.EndVertical();
			GUILayout.EndVertical();

			GUILayout.Space(10f);

			GUILayout.EndVertical();
		}
		
		/************************************************************************
		*	 	 	 	 	Private Method Declaration	 	 	 	 	 		*
		************************************************************************/
		private string ValidateEasing()
		{
			if( string.IsNullOrEmpty(this._easingName) ) return "Name is empty";
			if( this._data.IsExistName(this._easingName, this._easingIndex) ) return "Same name is exist";
			return null;
		}

		private void InitUI()
		{
			this._easingName = "";
			this._easingCurve = this.EasingDefault;
			this._easingIndex = -1;
			GUIUtility.keyboardControl = 0;
		}

		private void ReadData()
		{
			XTweenEditorManager.Instance.initializeListener -= this.ReadData;
			this.InitUI();
			this._data = XTweenEditorData.Instance;
		}

		private void CheckedLastVersion( string version )
		{
			if( string.IsNullOrEmpty(version) )
			{
				//set stored
				this._lastVersion = this._versionController.StoredLastVersion;
			}
			else if( version.Equals("error") )
			{
				this._lastVersion = NETWORK_ERROR_MSG;
			}
			else
			{
				string current = XTweenEditorManager.Instance.Data.version;
				if( !this._checkResult && version == current && this._checkForce )
				{
					this._checkForce = false;
					EditorUtility.DisplayDialog("Infomation", "You had already lastest version.", "OK");
				}
				//set downloaded
				this._lastVersion = version;
			}
		}
		
		/************************************************************************
		*	 	 	 	 	Protected Method Declaration	 	 	 	 	 	*
		************************************************************************/
		
		/************************************************************************
		*	 	 	 	 	Public Method Declaration	 	 	 	 	 		*
		************************************************************************/
		
	}
}