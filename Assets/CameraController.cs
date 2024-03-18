using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //wsppó³rzêdne gracza
    Transform player;
    //wysokoœæ kamery
    public float cameraHeight = 10.0f;
    //prêdkoœæ kamery
    Vector3 cameraSpeed;
    //szyboœæ
    public float dampSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //pod³¹cz pozycjê gracza do lokalnej 
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //oblicz docelow¹ pozycjê kamery
        Vector3 targetPosition = player.position + Vector3.up * cameraHeight;

        //p³ynnie przesuñ kamerê w kierunku gracza
        //funkcja Vector3.Lerp
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
        //
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraSpeed, dampSpeed);
    }
}
