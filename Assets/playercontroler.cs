using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //dodaj do wsp�rz�dnych warto�� x=1 y=0 z=0 pomo�e przez czas
        //mierzony w sekundach od ostatniej klatki
        transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
    }
}
