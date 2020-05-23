using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrollerScript : MonoBehaviour
{
    public float speed;
    public float bgScale; //сюда задаем значение масштаба фона
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //float zMovement = Mathf.Repeat(Time.time * speed, 100);  // 0 ... 100
        float zMovement = Mathf.Repeat(Time.time * speed, transform.localScale.z * 10); //В зависимости от масштаба нашего BG автоматом пересчитывается диапазон сдвига фона
        transform.position = startPosition + Vector3.back * zMovement; 
    }
}
