using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sonda : MonoBehaviour
{
    public float blad;          // blad transformacji Boxa-Mullera
    public float odleglosc;     // odległość między dwoma obiektami (dno i sonda)
    public int odlegloscAux;    // odległość między dwoma obiektami (dno i sonda), wyrażona w int
    public float czas = 0.0f;   // aktualny czas od ostatniego odświeżenia
    public Text glebokosc;      // zmienna dla informacji o glebokosci
    static System.Random rand = new System.Random();

    double u1;
    double u2;
    double z1;

    void Start()
    {


    }

    void Update()
    {
        RaycastHit detekcja; // wykrywanie dna
        Ray echoRay = new Ray(transform.position, Vector3.down); // konfiguracja raycast

        u1 = 1.0 - rand.NextDouble();                   //generowanie losowej liczby
        u2 = 1.0 - rand.NextDouble();                   //generowanie losowej liczby
        z1 = Math.Sqrt(-0.3 * Math.Log(u1)) * Math.Sin(0.3 * Math.PI * u2); //obliczanie bledu

        if (Physics.Raycast(echoRay, out detekcja)) // petla wykrywająca dno
        {
            blad = (float)z1;
            odleglosc = detekcja.distance + blad; //dystans do obiektu który odbija falę

            Vector3 down = transform.TransformDirection(Vector3.down) * detekcja.distance;  //DEBUG - PROMIEN O DLUGOSCI WYKRYTEJ PRZEZ RAYCASTING
            Debug.DrawRay(transform.position, down, Color.red);                             //DEBUG - PROMIEN SKIEROWANY W DOL, KOLORU CZERWONEGO
            
        }

        if (czas > 1) // pętla odswiezająca sie co sekunde
        {
            glebokosc.text = odleglosc.ToString("0.00") + "m (+/- 0.3m)"; ; // konwersja float to string
            czas = 0; // zerowanie czasu po wykonaniu petli
        }
        czas += UnityEngine.Time.deltaTime; // przypisanie czasu do zmiennej czas
    }
}
