using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sterowanie : MonoBehaviour
{
    public Slider predkoscNastawa;
    public Slider pletwaNastawa;
    public Text predkosc;
    public Text pletwa;
    public Text cog;
    public Text predkoscAktHUD;
    public Text wychylenieAktHUD;
    public Text COGAktHUD;
    public Text ROTAktHUD;
    public RawImage north;


    public float silnik = 0.0f;
    public float pletwaAkt = 0.0f;
    public float predkoscAktualna = 0.0f;
    public float x = 2100.0f;
    public float z = 1900.0f;
    public const float predkoscMax = 21.0f;
    public const float wychylenieMax = 35.0f;
    public float ROT = 0.0f;
    public float COG = 0.0f;


    public float K = 0.07f;


    void Update()
    {
        predkosc.text = predkoscNastawa.value.ToString() + " %";
        pletwa.text = pletwaNastawa.value.ToString() + " °";
        

        if (predkoscAktualna != predkoscNastawa.value * predkoscMax / 100)
        {
            predkoscAktualna = Mathf.Lerp(predkoscAktualna, (predkoscMax * predkoscNastawa.value) / 100, 0.05f * Time.deltaTime);
            predkoscAktHUD.text = predkoscAktualna.ToString("0.0") + " kn";
        }
        pletwaAkt = Mathf.MoveTowards(pletwaAkt, pletwaNastawa.value, 1f * Time.deltaTime);
        predkoscAktualna = Mathf.MoveTowards(predkoscAktualna, (predkoscMax * predkoscNastawa.value) / 100, 0.05f * Time.deltaTime);
        if (predkoscAktualna != 0)
        {
            ROT += (K * (pletwaAkt) - ROT) * Time.deltaTime;
            ROTAktHUD.text = ROT.ToString("0.0") + "  °/s";
            
            COG += ROT * Time.deltaTime;
            COGAktHUD.text = COG.ToString("0.0") + "  °";
            {

                x += predkoscAktualna * Time.deltaTime * Mathf.Sin(COG * Mathf.PI / 180);
                z += predkoscAktualna * Time.deltaTime * Mathf.Cos(COG * Mathf.PI / 180);
                transform.position = new Vector3(2100+z, 500, 1900-x);
                transform.rotation = Quaternion.Euler(0, COG, 0);

                GameObject.Find("Canvas/RawImage").transform.position = new Vector3(536.4315f-(COG*8), (453.55835f), 0);

                wychylenieAktHUD.text = pletwaAkt.ToString("0") + " °";
            }
        }
    }
}






