using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool weaponButtonDown = false; //пока оставляем Public, чтобы использовать в PlayerScript напрямую, дабы убрать ошибку в логах

    public bool WeaponButtonDown
    {
        get
        {
            return weaponButtonDown;
        }
        set 
        {
            weaponButtonDown = value;
        }
    }

    void Start()
    {
        weaponButtonDown = false;
    }

    //void OnMouseDown()
    public void OnPointerDown(PointerEventData eventData) //должен быть public, иначе не имплементируется IPointerDownHandler
    {
        weaponButtonDown = true;
        Debug.Log("weaponButtonsDown = " + weaponButtonDown);
    }

    //public void OnMouseUp()
    public void OnPointerUp(PointerEventData eventData)
    {
        weaponButtonDown = false;
        Debug.Log("weaponButtonsDown = " + weaponButtonDown);
    }


/* 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 */
}
