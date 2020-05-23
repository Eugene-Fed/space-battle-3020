using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public float speed;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed); // щит всегда движется сверху вниз, поэтому достаточно такой конструкции.
        //если будет более сложный функционал, можно использовать по аналогии с классом LaserScript
    }

}
