using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class LevelInfo
{
    public int sideLength;
    public int[] tileArray;
}

public class Level
{
    public int sideLength;
    public int[,] tileArray;
}

public class LevelManager : MonoBehaviour {
    public Level ActiveLevel {
        get {
            return activeLevel;
        }
        set {
            activeLevel = value;
        }
    }

    private Level activeLevel = new Level();
    private readonly string jsonFilePath = "Assets/Resources/levels.json";


    public void LoadFromJson(string lName)
    {
        string levelJson = JsonHelper.GetJsonObject(Resources.Load<TextAsset>("levels").text , lName);

        LevelInfo loadedLevel = JsonUtility.FromJson<LevelInfo>(levelJson);
        activeLevel.sideLength = loadedLevel.sideLength;
        activeLevel.tileArray = Construct2dArray(loadedLevel.tileArray, loadedLevel.sideLength);

        ActiveLevel = activeLevel;
    }

    int[,] Construct2dArray(int[] array, int length)
    {
        int[,] t = new int[length, length];
        for (int i = 0; i < length; ++i)
        {
            for (int j = 0; j < length; ++j)
                t[i, j] = array[(i * length + j)];
        }
        return t;
    }


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}