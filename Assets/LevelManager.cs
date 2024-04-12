using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    Transform player;
    //odleg�os� od ko�ca poziomu
    public float levelExitDistance = 100;
    //punkt ko�ca poziomu
    public Vector3 exitPosition;
    // Start is called before the first frame update
    void Start()
    {
        //znajdz gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //wylosuj pozycj� na kole o �rednicy 100 jednostek
        Vector2 spawnCircle = Random.insideUnitCircle; //losowa pozycja x,y wewn�trz ko�a o r=1
        //chcemy tylko pozycj� na okr�gu, a nie wewn�trz ko�a
        spawnCircle = spawnCircle.normalized; //pozycje x,y w odleg�o�ci 1 od �rodka
        spawnCircle *= levelExitDistance; //pozycja x,y w odleg�o�ci 100 od �rodka
        //konwertujemy do Vector3
        //podstawiamy: x=x, y=0, z=y
        exitPosition = new Vector3(spawnCircle.x, 0, spawnCircle.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
