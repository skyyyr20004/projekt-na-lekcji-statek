using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroler : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float flySpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //dodaj do wsp�rz�dnych warto�� x=1 y=0 z=0 pomo�e przez czas
        //mierzony w sekundach od ostatniej klatki
        //transform.position += new Vector3(1, 0, 0) * Time.deltaTime;

        //prezentacja dzia�ania wyg�adzonego sterowania (emulacja joysticka)
        //Debug.Log (Input.GetAxis("Vertical"));

        //sterowanie pr�dko�ci�
        //stw�rz nowy wektor przesuni�cia o wrto�ci 1 do przodu

        Vector3 movement = transform.forward;
        //pomn� przez czas od ostatniej klatki
        movement *= Time.deltaTime;
        //pomn� przez "wychylenie joysticka"
        movement *= Input.GetAxis("Vertical");
        //pomn� przez pr�dko�� lotu
        movement *= flySpeed;
        //dodaj ruch do obiektu
        //zmiana na fizyce
        //transform.position += movement;
        transform.GetComponent<Rigidbody>().AddForce(movement, ForceMode.VelocityChange);

        //obr�t
        //modyfikuj o� "Y" obiektu player
        Vector3 rotation = Vector3.up;
        //przemn� przez czas 
        rotation *= Time.deltaTime;
        //przemn� przez klawiature 
        rotation *= Input.GetAxis("Horizontal");
        //pom� przez pr�dko�� obrotu
        rotation *= rotationSpeed;
        //dodaj obr�t do obiektu
        // nie mo�emy u�y� += poniewa� unity u�ywa Quaternion�w do zapisu rotacji
        transform.Rotate(rotation);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //uruchamia si� automatycznie je�li zetkniemy si� z innym coliderem

        //sprawd� czy dotkneli�my asteroidy
        if (collision.collider.transform.CompareTag("Asteroid"))
        {
            Debug.Log("Boom!");
            //pauza
            Time.timeScale = 0;
        }
    }
}
