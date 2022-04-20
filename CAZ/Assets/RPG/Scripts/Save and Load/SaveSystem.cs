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
}
