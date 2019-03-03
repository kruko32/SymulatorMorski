using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sonda : MonoBehaviour
{

    public float odleglosc2;
    public float odleglosc; // odległość między dwoma obiektami (dno i sonda)
    public int odlegloscAux; // odległość między dwoma obiektami (dno i sonda), wyrażona w int
    public float czas = 0.0f; // aktualny czas od ostatniego odświeżenia
    public Text glebokosc; // zmienna dla informacji o glebokosci
    void Start()
    {

    }

    void Update()
    {
        RaycastHit detekcja; // wykrywanie dna
        Ray echoRay = new Ray(transform.position, Vector3.down); // konfiguracja raycast

        if (czas > 1) // pętla odswiezająca sie co sekunde
        {
            if (Physics.Raycast(echoRay, out detekcja)) // petla wykrywająca dno
            {
                odleglosc = (int)detekcja.distance; //dystans do obiektu który odbija falę + konwersja float to int
                glebokosc.text = odleglosc.ToString() + "m (+- 0.5m)"; ; // konwersja int to string
             
            }
            czas = 0; // zerowanie czasu po wykonaniu petli
        }
        czas += UnityEngine.Time.deltaTime; // przypisanie czasu do zmiennej czas

    }
}
