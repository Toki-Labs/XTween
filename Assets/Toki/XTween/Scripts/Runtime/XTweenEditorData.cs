/**********************************************************************************
/*		File Name 		: XTweenExporter.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-27
/*		Modified Date 	: 
/**********************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Toki.Tween 
{
	[Serializable]
	public class EasingData 
	{
		public string name;
		public AnimationCurve animationCurve = new AnimationCurve (new Keyframe (0f, 0f, 0f, 1f), new Keyframe (1f, 1f, 1f, 0f));

	}

	public class XTweenEditorData : ScriptableObject 
	{
		/************************************************************************
		 *	 	 	 	 	Static Variable Declaration	 	 	 	 	 	    *
		 ************************************************************************/

		/************************************************************************
		 *	 	 	 	 	Static Method Declaration	 	 	 	     	 	*
		 ************************************************************************/
		public static XTweenEditorData _instance;
		public static XTweenEditorData Instance 
		{
			get 
			{
				if (_instance == null) 
				{
					_instance = Resources.Load<XTweenEditorData>("XTweenData");
				}
				return _instance;
			}
		}

		/************************************************************************
		 *	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
		 ************************************************************************/
		private Dictionary<EaseCustom, EaseCustomData> _easingDic;

		/************************************************************************
		 *	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
		 ************************************************************************/

		/************************************************************************
		 *	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
		 ************************************************************************/
		public List<EasingData> easingDataList = new List<EasingData>();

		/************************************************************************
		 *	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
		 ************************************************************************/

		/************************************************************************
		 *	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
		 ************************************************************************/

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
		 *	 	 	 	 	Public Method Declaration	 	 	 	 	 		*
		 ************************************************************************/
		public bool IsExistName (string name, int index) 
		{
			bool exist = false;
			int length = this.easingDataList.Count;
			for (int i = 0; i < length; ++i) 
			{
				if (index == -1) 
				{
					if (this.easingDataList[i].name.ToLower () == name.ToLower ()) 
					{
						exist = true;
						break;
					}
				} 
				else 
				{
					if (this.easingDataList[i].name.ToLower () == name.ToLower () && i != index) 
					{
						exist = true;
						break;
					}
				}
			}
			return exist;
		}

		public EaseCustomData GetEasingData(EaseCustom name)
		{
			EaseCustomData data = null;
			if( this._easingDic == null )
			{
				this._easingDic = new Dictionary<EaseCustom, EaseCustomData>();
				foreach( var item in this.easingDataList )
				{
					EaseCustom easingName = (EaseCustom)Enum.Parse(typeof(EaseCustom), item.name);
					this._easingDic[easingName] = new EaseCustomData(item.animationCurve);
				}
			}
			if( this._easingDic.ContainsKey(name) )
			{
				data = this._easingDic[name];
			}
			return data;
		}

#if UNITY_EDITOR
		public void Save()
		{
			EditorUtility.SetDirty(this);
			try 
			{
				AssetDatabase.SaveAssets();
			} 
			catch ( System.Exception e )
			{
				Debug.Log(e.Message);
			}
		}
#endif
	}
}