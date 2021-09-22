using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoader
{
    private static string _path = Application.persistentDataPath + "/Slot1.Guards";

    private static bool Exists()
    {
        return File.Exists(_path);
    }

    public static SaveData LoadSave()
    {
        if (Exists())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(_path, FileMode.Open);
            SaveData data = (SaveData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            return CreateFile();
        }
    }

    private static SaveData CreateFile()
    {
        MonoBehaviour.print(_path);
        var file = File.Create(_path);
        SaveData save = new SaveData();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(file, save);
        file.Close();

        Save(save);
        return save;
    }

    public static void Save(SaveData save)
    {
        if (Exists())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            var file = File.Open(_path, FileMode.Open);
            formatter.Serialize(file, save);
            file.Close();
        }
        else
        {
            CreateFile();
        }
    }
}
