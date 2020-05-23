using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//[System.Serializable]
public class SaveGame //: MonoBehaviour //по идее бы наследовать от класса Save и не пришлось бы копипастить половину класса. а MonoBehaviour тут и не нужен
{
    //Field
    int maxScore;

    //Settings
    public int MaxScore
    {
        get 
        {
            return maxScore;
        }
        set 
        {
            maxScore = value;
        }
    }

    public void Save()
    {
        Save save = CreateSaveGameObject();

        // *** serialized Savefile START ***
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
        // *** serialized Savefile END ***

        Debug.Log("Game Saved. MaxScore = " + save.MaxScore);
    }
/*  
    //###################################### SaveJSON Start ################################################
    public void SaveAsJSON() //сейчас нигде не используется запустить его в работу позже по необходимости
    {
        Save saveJSON = CreateSaveGameObject();
        //*** JSON save void START ***
        if (!File.Exists(Application.persistentDataPath + "gamesave.json")) //если файла сейва еще нет
        {
            FileStream fileJSON = File.Create(Application.persistentDataPath + "gamesave.json"); // то создать его и закрыть
            fileJSON.Close();
        }

        // путь "gamesave.json" работает на десктопе, однако ломает работу мобильного приложения. испытать на мобилке формат пути "/gamesave.json"

        stringJSON = JsonUtility.ToJson(saveJSON);
        StreamWriter sw = new StreamWriter("gamesave.json", false); //второй параметр False означает, что файл перезаписывается. True - обозначает добавление в конец файла.
        sw.WriteLine(stringJSON);
        sw.Close();
        Debug.Log("Saving as JSON: " + stringJSON);
        //*** JSON save void END ***
    }
    //###################################### SaveJSON End ################################################
 */

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        save.MaxScore = maxScore;
        return save;
    }
}
