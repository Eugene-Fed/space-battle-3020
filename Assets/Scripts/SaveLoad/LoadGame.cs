using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGame //: MonoBehaviour //по идее бы наследовать от класса Save и не пришлось бы копипастить половину класса. а MonoBehaviour тут и не нужен
{
    //[SerializeField]
    //*** Private Fields ***
    int maxScore = 0; //Просто временный параметр, необходимый для отладки. В дальнейшем на что-то будет заменен
    string stringJSON; //Сюда прилетает текст из файла "/savegame.json"
    string saveFilePath; //не используется
    bool isSaveFileExists = true; //для того, чтобы делать только одну проверку на наличие файла. если это не делать часто, возможно исключить из работы

    //*** Public Settings ***
    public int MaxScore
    {
        get 
        {
            return maxScore;
        }
        private set //присваивать извне нельзя, поотому что незачем
        {
            maxScore = (int)value > 0 ? (int)value : 0;
        }
    }

    //void Start() //загружается ПОСЛЕ Load()
    //{
        //isSaveFileExists = File.Exists(Application.persistentDataPath + "/gamesave.save");
        //saveFilePath = Application.persistentDataPath + "/gamesave.save";
        //Debug.Log ("Старт LoadGame. Application.persistentDataPath = " + Application.persistentDataPath +
        //        "\nsaveFilePath = " + saveFilePath);
    //}

//#################################### сейчас этот метод не используется
    public bool IsSafeFileExists() // решить надо ли юзать отдельный метод или можно обратно вставить в условие проверки наличия файла возвращаемое значение этого метода
    {
        // Решить юзаем этом метод или поле isSaveFileExists
        //isSaveFileExists = File.Exists(Application.persistentDataPath + "/gamesave.save"); //по этот буль больше не используетя, можно удалять
        isSaveFileExists = File.Exists(saveFilePath);
        //так же можно удалить и весь метод
        return isSaveFileExists;
    }
//####################################

    //public void Load()
    public void LoadSavedGame()
    {
        Debug.Log("############# STARTING LoadGame.LoadSavedGame ################");
        saveFilePath = Application.persistentDataPath + "/gamesave.save";
        //if (IsSafeFileExists()) //возможно убрать лишний метод в классе и использовать проверку прям тут, в условии
        if (File.Exists(saveFilePath))
        {
            //Далее происходит загрузка сохраненной игры или старт новой, если файл сохранения пуст.
            Debug.Log("SaveFile Exists");
            BinaryFormatter bf = new BinaryFormatter();

            Save save = new Save();
            FileStream file = File.Open(saveFilePath, FileMode.Open);
            save = (Save)bf.Deserialize(file);
            maxScore = save.MaxScore;
            Debug.Log("MaxCount in LoadGame AFTER load savefile = " + maxScore.ToString());

            file.Close();
            //Debug.Log(string.Format("Game Loaded with MaxScore = {0} and DateTime = {1}. Last action was {2:00}:{3:00}:{4:00} ago.", maxScore, dateTime, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
        }
        else //В противном случае остаемся на стартовой сцене и ждем выбор яйца пользователем.
        {
            maxScore = 0;
            Debug.Log("ПЕРВЫЙ ЗАПУСК ИГРЫ. НЕТ НИ ОДНОГО ФАЙЛА СОХРАНЕНИЯ. Save File Path = " + saveFilePath);
        }
    }

/*  //###################################### LoadJSON Start ################################################
    //в мобилке пока не работает - ломает работу приложения. разобраться в чем дело, чтобы в дальнейшем использовать для передачи данных на сервак.
    //идея - делать сейв в JSON, отправлять данные и удалять файл gamesave.json.
    //возможно, так же получать gamesave.json с сервака, загружать с него данные в приложение, удалять json и дальше юзать сериализованный класс Save.
    public void LoadJSON() //решить, сделать ли этот метод private и запускать ли принудительно из метода Load(). Скорее всего НЕТ.
    {
        Save saveJSON = new Save(); //для загрузки из файла JSON

        //Сделать предварительную проверку наличия файла gamesave.json
        StreamReader sr = new StreamReader("gamesave.json"); // открытие файла gamesave.json 
        stringJSON = sr.ReadToEnd();
        saveJSON = JsonUtility.FromJson<Save>(stringJSON);
        Debug.Log("JSON file: " + stringJSON);
        sr.Close();
    }
 */ //###################################### LoadJSON End ################################################
}
