using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidspawner : MonoBehaviour
{
    //gracz (jego pozycja)
    Transform player;

    //prefab statycznej asteroidy
    public GameObject staticAsteroid;

    //czas do ostatnio wygenreowanej asteroidy
    float timeSinceSpawn;
    // Start is called before the first frame update
    void Start()
    {
        //znajdz gracz 
        player = GameObject.FindWithTag("Player").transform;

        //zeruj czas 
        timeSinceSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {

        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn > 1) 
        {
            GameObject asteroid = SpawnAsteroid(staticAsteroid);
            timeSinceSpawn = 0;
        }
    }

    GameObject SpawnAsteroid(GameObject prefab)
    {




        Vector3 randomPosition = Random.onUnitSphere * 10;

        randomPosition += player.position;

        //na³ó¿ pozycjê gracza - teraz mamy pozycje 10 jednostek od gracza
        GameObject asteroid = Instantiate(staticAsteroid, randomPosition, Quaternion.identity);

        //zwróæ asteroide jako wynik dzia³ania
        return asteroid;
    }
}
