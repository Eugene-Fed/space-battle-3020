using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float rotationSpeed;
    public float minSpeed, maxSpeed;
    public int health; //дает астероиду единицы здоровья
    public int yellowLaserPower; //мощность вражеского лазера
    public int greenLaserPower; //мощность вражеского лазера
    public int redEnemyLaserPower; //мощность вражеского лазера
    public int shipPower; //сила удара астероида
    public GameObject asteroidExplosion;
    public GameObject playerExplosion;
    
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody asteroid = GetComponent<Rigidbody>();
        asteroid.angularVelocity = Random.insideUnitSphere * rotationSpeed;

        float zSpeed = Random.Range(minSpeed, maxSpeed);
        asteroid.velocity = new Vector3(0, 0, -zSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Для того, чтобы поток астероидов был "плотнее" и они не уничтожали друг друга исключил их взаимоодействие между собой
        switch (other.tag)
        {
            case "Player":
                health -= shipPower;
                break;
            case "Enemy":
                health -= shipPower;
                break;
            case "YellowLaser":
                Destroy(other.gameObject); //лазер просто исчезает
                health -= yellowLaserPower;
                break;
            case "GreenLaser":
                Destroy(other.gameObject); //лазер просто исчезает
                health -= greenLaserPower;
                break;
            case "RedEnemyLaser": //разбивка на разные лазеры для дальнейшей реализации разного урона в зависимости от типа лазера
                Destroy(other.gameObject); //лазер просто исчезает
                health -= redEnemyLaserPower;
                break;
            case "GameBoundary":
            case "Asteroid":
            default:
                //не делаем ничего. Астероиды проскальзывают сквозь друг друга, дабы не превратить игровое пространство в каменоломню
                break;
        }

        if (health <= 0)
        {
            DestroySelf();
        }

    }

    private void DestroySelf()
    {
        Destroy(gameObject);
        Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
    }

}
