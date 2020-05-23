using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour
{
    //######## сейчас не используется как отдельный класс. используется функция выхода из GameController
    public GameObject MnuTop;

    void OnMouseDown ()
    {
        //btnMnuExit.SetActive (false);
        Debug.Log("Нажата кнопка Exit");
        MnuTop.SetActive (false);
        Application.Quit();
    }
    
}
