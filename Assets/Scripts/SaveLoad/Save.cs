using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    //Fields
    //[SerializeField] //если убрать это объявление, в json поле не сериализуется
    int maxScore; // сюда временно сохраняется количество выигранных монстров. чисто для себя. для работы стартовая сцена берет количество монстров из длинны списка monsterCollection

    //Settings
    public int MaxScore
    {
        get
        {
            return maxScore;
        }
        set
        {
            maxScore = (int)value > 0 ? (int)value : 0;
        }
    }
/*
    //в этот параметр сохраняется глобальное время последнего действия (надо сохранять время старта игры)
    public DateTime SavedDateTime
    {
        get
        {
            return savedDateTime;
        }
        set
        {
            savedDateTime = value;
        }
    }

    public bool LoadMainScene
    {
        get 
        {
            return loadMainScene;
        }
        set 
        {
            loadMainScene = value;
        }
    }

    public List<short> MonsterCollection
    {
        get 
        {
           return monsterCollection;
        }
        set 
        {
            monsterCollection = value;
        }
    }
*/
}
