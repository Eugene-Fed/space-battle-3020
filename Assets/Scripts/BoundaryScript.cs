using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryScript : MonoBehaviour
{
    public int decrementScoreEnemy;
    
    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                GameController.instance.IncrementScore(-decrementScoreEnemy);
                break;
        }
        
        Destroy(other.gameObject);
    }
}
