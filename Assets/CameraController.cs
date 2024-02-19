using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //wspp�rz�dne gracza
    Transform player;
    //wysoko�� kamery
    public float cameraHeight = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        //pod��cz pozycj� gracza do lokalnej 
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //oblicz docelow� pozycj� kamery
        Vector3 targetPosition = player.position + Vector3.up * cameraHeight;

        //p�ynnie przesu� kamer� w kierunku gracza
        //funkcja Vector3.Lerp
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
    }
}
