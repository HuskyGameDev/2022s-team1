using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGameData(GameManager gm)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savefile.caz";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveGameData data = new SaveGameData(gm);

        formatter.Serialize(stream, data);

        stream.Close();

    }

    public static SaveGameData LoadGameData()
    {
        string path = Application.persistentDataPath + "/savefile.caz";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveGameData data = formatter.Deserialize(stream) as SaveGameData;
            stream.Close();

            return data;
        }
        else {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

    //For when we start a new game, delete the data!
    public static void RemoveGameData(){
        string path = Application.persistentDataPath + "/savefile.caz";
        if (File.Exists(path)){
            File.Delete(path);
        } else {
            Debug.LogError("Save File not found in " + path + ". Cannot Delete.");
        }
    }

        public static void SaveGameSettings(float vol)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.caz";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveGameSettings data = new SaveGameSettings(vol);

        formatter.Serialize(stream, data);

        stream.Close();

    }

    public static SaveGameSettings LoadGameSettings()
    {
        string path = Application.persistentDataPath + "/settings.caz";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveGameSettings data = formatter.Deserialize(stream) as SaveGameSettings;
            stream.Close();

            return data;
        }
        else {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

}
