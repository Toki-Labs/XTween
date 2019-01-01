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
		
		/************************************************************************
		*	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
		************************************************************************/
		private XTweenEditorData _data;
		private EasingData _easingData;
		
		/************************************************************************
		*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
		************************************************************************/
			
		/************************************************************************
		*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
		************************************************************************/
			
		/************************************************************************
		*	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
		************************************************************************/
		
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
			EditorGUIUtility.labelWidth = 110f;
			int index = this._data.index;

			if( this._data.easingDataList.Count > index )
			{
				EasingData data = this._data.easingDataList[index];
				AnimationCurve curve = EditorGUILayout.CurveField("Animation Curve", data.animationCurve, GUILayout.Width(170f), GUILayout.Height(62f));
				if(GUILayout.Button("Test"))
				{
					Debug.Log("Test: " + data.animationCurve.Evaluate(0.5f));
				}
			}
			GUILayout.EndVertical();
		}
		
		/************************************************************************
		*	 	 	 	 	Private Method Declaration	 	 	 	 	 		*
		************************************************************************/
		private void ReadData()
		{
			XTweenEditorManager.Instance.initializeListener -= this.ReadData;
			this._data = Resources.Load<GameObject>("XTweenData").GetComponent<XTweenEditorData>();
		}
		
		/************************************************************************
		*	 	 	 	 	Protected Method Declaration	 	 	 	 	 	*
		************************************************************************/
		
		/************************************************************************
		*	 	 	 	 	Public Method Declaration	 	 	 	 	 		*
		************************************************************************/
		
	}
}