using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class WebGLManager : MonoBehaviour 
{
    [DllImport("__Internal")]
    private static extern void Hello();

    [DllImport("__Internal")]
    private static extern void HelloString(string str);

    [DllImport("__Internal")]
    private static extern void PrintFloatArray(float[] array, int size);

    [DllImport("__Internal")]
    private static extern int AddNumbers(int x, int y);

	[DllImport("__Internal")]
    private static extern string GetString();


    [DllImport("__Internal")]
    private static extern string StringReturnValueFunction();

    /*[DllImport("__Internal")]
    private static extern void BindWebGLTexture(int texture); */

	public Text text;

    void Start() 
	{
		/*
        Hello();
        
        HelloString("This is a string.");
        
        float[] myArray = new float[10];
        PrintFloatArray(myArray, myArray.Length);
        
        int result = AddNumbers(5, 7);
        Debug.Log(result);

		this.text.text = StringReturnValueFunction();
        
        Debug.Log(StringReturnValueFunction());
        
        var texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
        BindWebGLTexture(texture.GetNativeTextureID()); */
    }
}