using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playercontroler : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float flySpeed = 5f;
    GameObject levelManagerObject;
    //stan os³on w procentach (1=100%)
    float shieldCapacity = 1;

    // Start is called before the first frame update

    void Start()
    {
        levelManagerObject = GameObject.Find("LevelManager");
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

        Vector3 movement = transform.forward;
        //pomnó¿ przez czas od ostatniej klatki
        movement *= Time.deltaTime;
        //pomnó¿ przez "wychylenie joysticka"
        movement *= Input.GetAxis("Vertical");
        //pomnó¿ przez prêdkoœæ lotu
        movement *= flySpeed;
        //dodaj ruch do obiektu
        //zmiana na fizyce
        //transform.position += movement;

        //komponent fizyki wewn¹trz gracza
         Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(movement, ForceMode.VelocityChange);

        //obrót
        //modyfikuj oœ "Y" obiektu player
        Vector3 rotation = Vector3.up;
        //przemnó¿ przez czas 
        rotation *= Time.deltaTime;
        //przemnó¿ przez klawiature 
        rotation *= Input.GetAxis("Horizontal");
        //pomó¿ przez prêdkoœæ obrotu
        rotation *= rotationSpeed;
        //dodaj obrót do obiektu
        // nie mo¿emy u¿yæ += poniewa¿ unity u¿ywa Quaternionów do zapisu rotacji
        transform.Rotate(rotation);
        UpdateUI();
    }
    private void UpdateUI()
    {
        //metoda wykonuje wszystko zwi¹zane z aktualizacj¹ interfejsu u¿ytkownika

        //wyciagnij z menadzera poziomu pozycje wyjscia
            Vector3 target = levelManagerObject.GetComponent<LevelManager>().exitPosition;
        //obroc znacznik w strone wyjscia
        transform.Find("NavUI").LookAt(target);
        //zmien ilosc procentwo widoczna w interfejsie
        //TODO: poprawiæ wyœwietlanie stanu os³on!
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
        //uruchamia siê automatycznie jeœli zetkniemy siê z innym coliderem

        //sprawdŸ czy dotkneliœmy asteroidy
        if (collision.collider.transform.CompareTag("Asteroid"))
        {
            Transform asteroid = collision.collider.transform;
            //policz wektor wed³ug którego odepchniemy asteroide
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
