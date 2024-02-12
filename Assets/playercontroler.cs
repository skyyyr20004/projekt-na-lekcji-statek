using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroler : MonoBehaviour
{
    public float rotationSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //dodaj do wspó³rzêdnych wartoœæ x=1 y=0 z=0 pomo¿e przez czas
        //mierzony w sekundach od ostatniej klatki
        //transform.position += new Vector3(1, 0, 0) * Time.deltaTime;

        //prezentacja dzia³ania wyg³adzonego sterowania (emulacja joysticka)
        //Debug.Log (Input.GetAxis("Vertical"));

        //sterowanie prêdkoœci¹
        //stwórz nowy wektor przesuniêcia o wrtoœci 1 do przodu

        Vector3 movement = Vector3.forward;

        movement *= Time.deltaTime;

        movement *= Input.GetAxis("Vertical");

        transform.position += movement;

        //obrót
        //modyfikuj oœ "Y" obiektu player
        Vector3 rotation = Vector3.up;
        //przemnó¿ przez czas 
        rotation *= Time.deltaTime;
        //przemnó¿ przez klawiature 
        rotation *= Input.GetAxis("Horizontal");
        //pomó¿ przez prêdkoœæ obiektu

        //dodaj obrót do obiektu
        // nie mo¿emy u¿yæ += poniewa¿ unity u¿ywa Quaternionów do zapisu rotacji
        transform.Rotate(rotation);
    }
}
