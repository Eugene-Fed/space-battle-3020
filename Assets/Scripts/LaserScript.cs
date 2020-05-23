using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);
        Rigidbody laserShot = GetComponent<Rigidbody>();
        laserShot.velocity = speed * transform.forward; //это позволяет направлять лазер "вперед" вне зависимости от глобальных координат
    }
}
