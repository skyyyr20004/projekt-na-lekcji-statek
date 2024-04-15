using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playercontroler : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float flySpeed = 5f;
    GameObject levelManagerObject;
    //stan os�on w procentach (1=100%)
    float shieldCapacity = 1;

    // Start is called before the first frame update

    void Start()
    {
        levelManagerObject = GameObject.Find("LevelManager");
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

        //komponent fizyki wewn�trz gracza
         Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(movement, ForceMode.VelocityChange);

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
        UpdateUI();
    }
    private void UpdateUI()
    {
        //metoda wykonuje wszystko zwi�zane z aktualizacj� interfejsu u�ytkownika

        //wyciagnij z menadzera poziomu pozycje wyjscia
            Vector3 target = levelManagerObject.GetComponent<LevelManager>().exitPosition;
        //obroc znacznik w strone wyjscia
        transform.Find("NavUI").LookAt(target);
        //zmien ilosc procentwo widoczna w interfejsie
        //TODO: poprawi� wy�wietlanie stanu os�on!
        TextMeshProUGUI shieldText =
            GameObject.Find("Canvas").transform.Find("ShieldCapacityText").GetComponent<TextMeshProUGUI>();
        shieldText.text = " Shield: " + (shieldCapacity*100).ToString() + "%";

        if(levelManagerObject.GetComponent<LevelManager>().levelComplete)
        {
            GameObject.Find("Canvas").transform.Find("LevelCompleteScreen").gameObject.SetActive(true);

        }
        if (levelManagerObject.GetComponent<LevelManager>().levelFailed)
        {
            GameObject.Find("Canvas").transform.Find("GameOverScreen").gameObject.SetActive(true);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //uruchamia si� automatycznie je�li zetkniemy si� z innym coliderem

        //sprawd� czy dotkneli�my asteroidy
        if (collision.collider.transform.CompareTag("Asteroid"))
        {
            Transform asteroid = collision.collider.transform;
            //policz wektor wed�ug kt�rego odepchniemy asteroide
            //Vector3 target = levelManagerObject.GetComponent<levelMenager>().exitPosition;
            Vector3 shieldForce = asteroid.position - transform.position;
            //popchnij asteroide
            asteroid.GetComponent<Rigidbody>().AddForce(shieldForce * 5, ForceMode.Impulse);
            shieldCapacity -= 0.25f;
            if(shieldCapacity <= 0)
            {
                levelManagerObject.GetComponent<LevelManager>().OnFailure();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("LevelExit"))
        {
            levelManagerObject.GetComponent<LevelManager>().levelComplete = true;
        }
    }

}
