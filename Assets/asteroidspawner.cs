#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

        if (timeSinceSpawn >0.1) 
        {
            GameObject asteroid = SpawnAsteroid(staticAsteroid);
            timeSinceSpawn = 0;
        }
        AsteroidCountControll();
    }

    GameObject? SpawnAsteroid(GameObject prefab)
    {

        //stwórz losową pozycję na okręgu 
        Vector2 randomCirclePosition = Random.insideUnitCircle.normalized;

        // losowa pozycja w odległości 10 jednostek od środka świata
        Vector3 randomPosition = new Vector3(randomCirclePosition.x, 0, randomCirclePosition.y) * 10;

        randomPosition += player.position;

        //sprawdź czy miejsce jest wolne

        if (!Physics.CheckSphere(randomPosition, 5)) 
        {
            //nałóż pozycję gracza - teraz mamy pozycje 10 jednostek od gracza
            GameObject asteroid = Instantiate(staticAsteroid, randomPosition, Quaternion.identity);

            //zwróć asteroide jako wynik działania
            return asteroid;
        }
        else
        {
            return null;
        }


       
    }
    void AsteroidCountControll()
    {
        //przygotuj tablicê wszystkich asteroidów na scenie
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        //przejdŸ pêtl¹ przez wyszystkie
        foreach (GameObject asteroid in asteroids)
        {
            //odleg³oœæ od gracza

            //wektor przesuniêcia miêdzy graczem a asteroid¹
            //(o ile musze przesun¹c gracza, ¿eby znalaz³ siê w miejscu asteroidy
            Vector3 delta = player.position - asteroid.transform.position;

            //magnitude to dugoœæ wektora = odleg³oœæ od gracza
            float distanceToPlayer = delta.magnitude;

            if (distanceToPlayer > 30)
            {
                Destroy(asteroid);
            }
        }
    }
}
    


    //przejdź pętle 
