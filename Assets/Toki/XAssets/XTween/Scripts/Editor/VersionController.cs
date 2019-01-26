/**********************************************************************************
/*		File Name 		: VersionController.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2019-1-25
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Common
{
	[Serializable]
	public class VersionData
	{
		public string version = "0.0.1";
	}

	public abstract class VersionController
	{
		/************************************************************************
		*	 	 	 	 	Static Variable Declaration	 	 	 	 	 	    *
		************************************************************************/
		private static Dictionary<string,VersionController> _versionControllerDic = new Dictionary<string, VersionController>();
		
		/************************************************************************
		*	 	 	 	 	Static Method Declaration	 	 	 	     	 	*
		************************************************************************/
		public static void Push( VersionController controller )
		{
			_versionControllerDic.Add(controller.AssetName, controller);
		}

		public static VersionController Get( string assetName )
		{
			if(_versionControllerDic.ContainsKey(assetName))
				return _versionControllerDic[assetName];
			else
				return null;
		}

		public static string TempPath
		{
			get
			{
				return Path.Combine(SystemUtil.AbsPath, "XAssetTemp");
			}
		}
		
		/************************************************************************
		*	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
		************************************************************************/
		/************************************************************************
		*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
		************************************************************************/
		protected string _assetName;
		protected VersionData _data;
		protected UnityWebRequest _http;
		protected Action _packageLoad;
		protected Action<string> _listener;
			
		/************************************************************************
		*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
		************************************************************************/

		/************************************************************************
		*	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
		************************************************************************/
		public bool IsDownloading { get; set; }
		public bool IsChecking { get; set; }
		public abstract string URL { get; }
		public abstract string URLPackage { get; }
		public string AssetName
		{
			get
			{
				return this._assetName;
			}
		}

		public Action<string> VersionCheckListener
		{
			set
			{
				this._listener = value;
			}
		}

		public VersionData Data
        {
            get
            {
                return _data;
            }
        }

		public string StoreCheckedDate
		{
			get
			{
				return this._assetName + "." + "store_checked_date";
			}
		}
		public string StoreLastVersion
		{
			get
			{
				return this._assetName + "." + "store_checked_date";
			}
		}

		public string JsonPath
        {
            get
            {

                return SystemUtil.AbsPath + "/Assets/Toki/XAssets/" + this._assetName + "/Scripts/Editor/" + this._assetName.ToLower() + "_version.json";
            }
        }

		public string StoredLastVersion
		{
			get
			{
				return EditorPrefs.GetString(StoreLastVersion, this._data.version);
			}
		}
		
		/************************************************************************
		*	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
		************************************************************************/
		
		/************************************************************************
		*	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
		************************************************************************/
		public VersionController(string assetName)
		{
			this._assetName = assetName;
			this.Load();
		}
		
		/************************************************************************
		*	 	 	 	 	Coroutine Declaration	 	  			 	 		*
		************************************************************************/
		private IEnumerator CoroutineVersionLoad()
		{
			this._http = new UnityWebRequest(URL);
			Debug.Log(URL);
			this._http.SetRequestHeader("Content-Type", "application/json");
			this._http.SetRequestHeader("Accepted", "application/json");
			this._http.downloadHandler = new DownloadHandlerBuffer();
			yield return this._http.SendWebRequest();
			do
			{
				yield return null;
			}
			while (!this._http.isDone);

			bool isSuccess = false;
			if( string.IsNullOrEmpty(this._http.error) || 
				(this._http.responseCode < 300 && this._http.responseCode >= 200) )
			{
				//Check version
				try
				{
					VersionData data = JsonUtility.FromJson<VersionData>(this._http.downloadHandler.text);
					EditorPrefs.SetString(StoreCheckedDate, this.GetToday());
					EditorPrefs.SetString(StoreLastVersion, data.version);
					if( !IsDownloading )
						this._listener(data.version);
					isSuccess = true;
				}
				catch ( System.Exception e ) 
				{
					Debug.Log(e.Message);
					this._listener("error");
				}
			}

			this._http.Dispose();
			if( !isSuccess ) this._listener("error");
			this.IsChecking = false;
			if( this._packageLoad != null ) this._packageLoad();
		}

		private IEnumerator CoroutinePackageLoad()
		{
			string fileName = this._assetName + "_" + this.StoredLastVersion + ".unitypackage";
			string tempPath = VersionController.TempPath;
			Directory.CreateDirectory(tempPath);
			string filePath = Path.Combine(tempPath, fileName);
			this._http = UnityWebRequest.Get(this.URLPackage + fileName);
			this._http.downloadHandler = new DownloadHandlerFile(filePath);
			yield return this._http.SendWebRequest();
			do
			{
				yield return null;
			}
			while (!this._http.isDone);
			if( string.IsNullOrEmpty(this._http.error) )
			{
				this.ImportPrevProcess();
				AssetDatabase.ImportPackage(filePath, false);
				this._data.version = this.StoredLastVersion;
				EditorPrefs.DeleteKey(StoreCheckedDate);
				EditorPrefs.DeleteKey(StoreLastVersion);

				EditorUtility.DisplayDialog("Information", "You had successfully updated!", "OK");

				yield return null;
				this.ImportPostProcess();
			}
			else
			{
				//Error
				EditorUtility.DisplayDialog("Error!", "Something wrong with download file. try next time.", "OK");
			}
			this._http.Dispose();
			this.IsDownloading = false;
			this._packageLoad = null;
		}

		private void EmptyTemp()
		{
			string tempPath = TempPath;
			if( Directory.Exists(tempPath) )
			{
				Directory.Delete(tempPath, true);
			}
		}
		
		/************************************************************************
		*	 	 	 	 	Private Method Declaration	 	 	 	 	 		*
		************************************************************************/
		private string GetToday()
		{
			DateTime date = DateTime.Now;
			return date.Year + "-" + date.Month + "-" + date.Day;
		}
		
		/************************************************************************
		*	 	 	 	 	Protected Method Declaration	 	 	 	 	 	*
		************************************************************************/
		protected abstract void ImportPrevProcess();
		protected abstract void ImportPostProcess();
		
		/************************************************************************
		*	 	 	 	 	Public Method Declaration	 	 	 	 	 		*
		************************************************************************/
		public void Check( bool checkForce = false )
		{
			bool needToCheck = checkForce;
			this.EmptyTemp();
			string date = EditorPrefs.GetString(StoreCheckedDate, null);
			if( string.IsNullOrEmpty(date) )
			{
				needToCheck = true;
			}
			else
			{
				string today = this.GetToday();
				if( !date.Equals(today) )
				{
					needToCheck = true;
				}
			}

			if( needToCheck )
			{
				this.IsChecking = true;
				EditorCoroutine.Start(CoroutineVersionLoad());
			}
			else
			{
				this._listener(null);
			}
		}

		public void Update()
		{
			this.IsDownloading = true;
			this.Check(true);
			this._packageLoad = () => EditorCoroutine.Start(CoroutinePackageLoad());
		}

		public void Load()
        {
            if( File.Exists(this.JsonPath) )
            {
                string jsonStr = FileReference.Read(this.JsonPath);
                this._data = JsonUtility.FromJson<VersionData>(jsonStr);
            }
            else
            {
                this._data = new VersionData();
                this.Save();
            }
        }
		
		public void Save()
        {
            string jsonStr = JsonUtility.ToJson(this._data);
            FileReference.Write(this.JsonPath, jsonStr);
        }
	}
	
	public class EditorCoroutine
	{
		public static EditorCoroutine Start( IEnumerator _routine )
		{
			EditorCoroutine coroutine = new EditorCoroutine(_routine);
			coroutine.Start();
			return coroutine;
		}

		private readonly IEnumerator _enumerator;
		EditorCoroutine( IEnumerator _routine )
		{
			this._enumerator = _routine;
		}

		void Start()
		{
			EditorApplication.update += Update;
		}
		public void Stop()
		{
			EditorApplication.update -= Update;
		}

		void Update()
		{
			if (!_enumerator.MoveNext())
			{
				Stop();
			}
		}
	}
}