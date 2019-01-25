using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

#if T_DEBUG
using ChartboostSDK;
#endif

namespace Toki.Editors.EditorWindows
{
	public abstract class EditorWindowAccordian : EditorWindowBase
	{
        /************************ Implements Interface *****************************/
        

        /************************  Get & Set  **************************************/


        /************************  Initialize & Destroy  ***************************/


        /************************  Private Variable  *******************************/


        /************************  Public Method  **********************************/


        /************************  Protected Method  *******************************/

        
        /************************  Private Method  *********************************/
        public override void Enable()
        {
            base.Enable();

            string[] opened = EditorPrefs.GetString(this.GetType().ToString(), "").Split(',');

            int length = this._moduleList.Count;
            int openedLength = opened.Length;
            if( openedLength > 0 )
            {
                int openedIndex = 0;
                for ( int i = 0; i < length; ++i )
                {
                    if( openedIndex < openedLength )
                    {
                        int openValue = int.Parse(opened[openedIndex]);
                        ModuleAccordian accordian = this._moduleList[i] as ModuleAccordian;
                        if( i == openValue )
                        {
                            openedIndex++;
                            accordian.open = true;
                        }
                        else
                        {
                            accordian.open = false;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public override void UpdateGUI()
        {
            base.UpdateGUI();
        }

        public virtual void OpenChanged()
        {
            int length = this._moduleList.Count;
            List<string> openList = new List<string>();
            for ( int i = 0; i < length; ++i )
            {
                ModuleAccordian accordian = this._moduleList[i] as ModuleAccordian;
                if( accordian.open )
                {
                    openList.Add( i.ToString() );

                }
            }
            EditorPrefs.SetString(this.GetType().ToString(), string.Join(",", openList.ToArray()));
        }
	}
}