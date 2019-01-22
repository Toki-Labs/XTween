/**********************************************************************************
/*		File Name 		: ModuleXTweenUpdate.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2019-1-22
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using UnityEditor;

namespace Toki.Tween
{
    public class ModuleXTweenUpdate : ModuleAccordian
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
        private const string NETWORK_ERROR_MSG = "Some with wrong in check update.";
        private const string INIT_MSG = "Checking version...";
        private XTweenEditorData _data;
        private XTweenVersionController _versionController;
        private bool _checkResult;
        private bool _checkForce = false;
        private string _lastVersion = INIT_MSG;


        /************************************************************************
        *	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
        ************************************************************************/


        /************************************************************************
        *	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
        ************************************************************************/
        public override string ModuleName
        {
            get
            {
                return "Update";
            }
        }


        /************************************************************************
        *	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
        ************************************************************************/


        /************************************************************************
        *	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
        ************************************************************************/


        /************************************************************************
        *	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
        ************************************************************************/
        public override void Initialize(EditorWindowBase window)
        {
            base.Initialize(window);
        }

        public override void OnEnable()
        {
            this._versionController = new XTweenVersionController(this.CheckedLastVersion);
			if( !EditorApplication.isPlaying ) this._versionController.Check();
        }

        public override void OnGUI()
        {
            base.OnGUI();

            if( this._open )
            {
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
                    if( !string.IsNullOrEmpty(this._lastVersion) )
                    {
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
                }
                else
                {
                    GUILayout.Space(70f);
                }
                GUILayout.EndVertical();
            }
        }
        
        public override void OnDestroy()
        {
            
        }


        /************************************************************************
        *	 	 	 	 	Coroutine Declaration	 	  			 	 		*
        ************************************************************************/


        /************************************************************************
        *	 	 	 	 	Private Method Declaration	 	 	 	 	 		*
        ************************************************************************/
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
			this._window.SetDirty();
		}
        
        /************************************************************************
        *	 	 	 	 	Protected Method Declaration	 	 	 	 	 	*
        ************************************************************************/


        /************************************************************************
        *	 	 	 	 	Public Method Declaration	 	 	 	 	 		*
        ************************************************************************/


    }
    
}