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
		private XTweenEditorData _data;
		private int _easingIndex = -1;
		private Vector2 _easingScroll = Vector2.zero;
		private string _easingName = "";
		private AnimationCurve _easingCurve;
		
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
                int length = easeList.Count;
				if( length > 0 )
				{
					for (int i = 0; i < length; ++i)
					{
						EasingData data = this._data.easingDataList[i];
						GUILayout.Space(-1f);
						GUI.color = (this._easingIndex == i) ? Color.cyan : Color.white;
						GUILayout.BeginHorizontal("AS TextArea", GUILayout.Height(25f));
						bool clicked0 = GUILayout.Button((i + 1).ToString(), "OL TextField", GUILayout.MaxWidth(30f));
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
			this._easingCurve = EditorGUILayout.CurveField("Animation Curve", this._easingCurve, GUILayout.Width(300f), GUILayout.Height(200f));
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
		
		/************************************************************************
		*	 	 	 	 	Protected Method Declaration	 	 	 	 	 	*
		************************************************************************/
		
		/************************************************************************
		*	 	 	 	 	Public Method Declaration	 	 	 	 	 		*
		************************************************************************/
		
	}
}