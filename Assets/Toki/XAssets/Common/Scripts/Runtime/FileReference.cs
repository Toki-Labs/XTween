/**********************************************************************************
/*		File Name 		: FileUtil.cs
/*		Author 			: 이동명
/*		Description 	: 
/*		Created Date 	: 2014-05-14
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using System.IO;
using System.Collections;
using System.Text;

public class FileReference
{
	/************************************************************************
	*	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
	************************************************************************/
		
		
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
	*	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
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
	public static string Read( string path )
	{
		string contents = null;
		SystemUtil.GenerateFolder( path.Substring( 0, path.LastIndexOf( "/" ) ) );
		FileStream file = new FileStream( path, FileMode.Open, FileAccess.Read );
		StreamReader sr = new StreamReader( file );
			
		contents = sr.ReadToEnd();
		sr.Close();
		file.Close();
		return contents;
	}
		
	public static void Write( string path, string contents, bool isOverride = true )
	{
		if( isOverride )
		{
			if( File.Exists( path ) )
			{
				File.Delete( path );
			}
		}
		SystemUtil.GenerateFolder( path.Substring( 0, path.LastIndexOf( "/" ) ) );
		FileStream file = new FileStream( path, FileMode.Create, FileAccess.Write );
		StreamWriter sw = new StreamWriter( file, Encoding.Default );
		sw.Write ( contents );
		sw.Close();
		file.Close();
	}
		
	public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
	{
#if UNITY_EDITOR
		// Get the subdirectories for the specified directory.
		DirectoryInfo dir = new DirectoryInfo(sourceDirName);
			
		if (!dir.Exists)
		{
			throw new DirectoryNotFoundException(
				"Source directory does not exist or could not be found: "
				+ sourceDirName);
		}
			
		DirectoryInfo[] dirs = dir.GetDirectories();
		// If the destination directory doesn't exist, create it.
		if (!Directory.Exists(destDirName))
		{
			Directory.CreateDirectory(destDirName);
		}
			
		// Get the files in the directory and copy them to the new location.
		FileInfo[] files = dir.GetFiles();
		int length = files.Length;
		for ( int i = 0; i < length; ++i )
		{
			FileInfo file = files[i];
			string temppath = Path.Combine(destDirName, file.Name);
			file.CopyTo(temppath, false);
		}
			
		// If copying subdirectories, copy them and their contents to new location.
		if (copySubDirs)
		{
			foreach (DirectoryInfo subdir in dirs)
			{
				string temppath = Path.Combine(destDirName, subdir.Name);
				DirectoryCopy(subdir.FullName, temppath, copySubDirs);
			}
		}
#endif
	}
		
}