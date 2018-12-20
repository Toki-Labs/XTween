/**********************************************************************************
/*		File Name 		: IColorUpdatable.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-22
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using System.Collections;

public interface IColorUpdatable
{
    void Initialize( GameObject target );
    Color GetColor();
    void SetColor( Color color  );
}