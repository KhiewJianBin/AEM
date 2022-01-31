using UnityEngine;

public static class FileHelper
{
	public static string[] ReadTilemapString(TextAsset file)
	{
		string[] tileData = file.text.Split('\n');

		return tileData;
	}
	public static char[][] ReadTilemapChar(TextAsset file)
	{
		return(StringArrayToCharArray(ReadTilemapString(file)));
	}
	public static char[][] StringArrayToCharArray(string[] sarray)
	{
		char[][] c = new char[sarray.Length][];
        for(int i = 0;i<sarray.Length;i++)
        {
            c[i] = sarray[i].ToCharArray();
        }
		return c;
	}
}
