using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class debug : MonoBehaviour
{
    public Slider debug_x;          //suwak przesunięcia w osi X
    public Slider debug_y;          //suwak przesunięcia w osi Y
    public Slider debug_z;          //suwak przesunięcia w osi Z
    public Slider debug_rx;         //suwak obrotu w osi RX
    public Slider debug_ry;         //suwak obrotu w osi RY
    public Slider debug_rz;         //suwak obrotu w osi RZ
    public Button debug_reset;      //przycisk resetowania zmian

    void Start()
    {
        debug_reset.onClick.AddListener(Deb_rst);       //oczekiwanie na naciśnięcie przycisku

    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x+debug_x.value, transform.position.y +debug_y.value, transform.position.z +debug_z.value);      //przesunięcie o wartość sliderów X,Y,Z
        transform.rotation = Quaternion.Euler(transform.rotation.x +debug_rx.value, transform.rotation.y +debug_ry.value, transform.rotation.z +debug_rz.value);      //obrót o wartość sliderów RX,RY,RZ

    }

    void Deb_rst()
    {
        /*      RESETOWANIE ZMIAN        */

        transform.position = new Vector3(2100, 500, 1900);

        debug_x.value = 0;
        debug_y.value = 0;
        debug_z.value = 0;
        debug_rx.value = 0;
        debug_ry.value = 0;
        debug_rz.value = 0;
    }
}


