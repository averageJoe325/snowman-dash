using System.IO;
using UnityEngine;

//  Stores information about levels.
public static class LevelManager
{
    public static int currentLevel;
    public static bool[] levelsUnlocked = {true, false, false};
    private static string path = "Assets/info.txt";

    //  Store unlocked levels.
    struct Storage
    {
        public bool[] levelsUnlocked;
        public Storage(bool[] levels)
        {
            levelsUnlocked = levels;
        }
    }
    //  Unlock the next level.
    public static void UnlockNext()
    {
        if (currentLevel == levelsUnlocked.Length)
            return;
        levelsUnlocked[currentLevel] = true;
    }

    //  Save levelsUnlocked.
    public static void saveLevels()
    {
        File.Delete(path);
        StreamWriter w = new StreamWriter(path, true);
        w.WriteLine(JsonUtility.ToJson(new Storage(levelsUnlocked)));
        w.Close();
    }

    //  Load data from info.text
    public static void loadLevels()
    {
        string data = "";
        try
        {
            StreamReader r = new StreamReader(path);
            data = r.ReadToEnd();
            r.Close();
        }
        catch (FileNotFoundException e)
        {
            data = "{\"levelsUnlocked\":[true,false,false]}";
        }
        Storage storage = JsonUtility.FromJson<Storage>(data);
        levelsUnlocked = storage.levelsUnlocked;
    }
}
