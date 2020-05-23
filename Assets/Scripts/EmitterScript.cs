using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour
{
    GameObject asteroid;
    
    //создаю список, по индексам которого проходит Random.Range(). 
    List<GameObject> enemyList;
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject shield;
    
    public float minDelay, maxDelay;

    float nextLaunchTime;

    void Start()
    {
         
        //Исходя из списка, имеем разную частоту появления разнотипных объектов
        //По сути тут кустарным ручным путем настраиваем соотношение врагов и ништяков на карте
        //До тех пор, пока враги не поворачивались лицом к игроку, можно было больше их выпускать в игру. Но с учето стрельбы сбоку и сзади, приходится ограничивать
        //И защищать тылы дополнительными специально пропущенными мимо себя астероидами
        enemyList = new List<GameObject>(){asteroid1, asteroid1, asteroid1, asteroid1, asteroid1, asteroid1, asteroid1, 
                                            asteroid2, asteroid2, asteroid2, asteroid2, asteroid2,
                                            asteroid3, asteroid3, 
                                            enemy1, enemy1, enemy1, enemy2, enemy2, enemy3, 
                                            shield};
 
        //enemyList = new List<GameObject>(){enemy1, enemy2, enemy3}; // просто временно убрать астероиды и оставить только врагов
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.isStarted == false)
        {
            return;
        }
        
        if (Time.time > nextLaunchTime)
        {
            int asteroidIndex = Random.Range(0, enemyList.Count); // получаем рандомный индекс астероида
            asteroid = enemyList[asteroidIndex]; //получаем астероид в соответствии с рандомным индексом
            
            float xPosition = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
            float zPosition = transform.position.z;
            Vector3 asteroidPosition = new Vector3(xPosition, 0, zPosition);
            
            Instantiate(asteroid, asteroidPosition, Quaternion.identity);
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
