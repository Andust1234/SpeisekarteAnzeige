using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSpeiseControll : MonoBehaviour
{
    public GameObject image;
    public Text titel;
    public Text beschreibung;
    public Text preis;

    private Speise speisenTable;
    private Texture2D txt;
    private Sprite sprite;

    private void VorschaubildSeitenverhältnis()
    {
        float width = image.GetComponent<RectTransform>().sizeDelta.x;
        float height = image.GetComponent<RectTransform>().sizeDelta.y;
        float einsEntspricht = 0;

        if (txt.width > txt.height)
        {
            einsEntspricht = (float)width / txt.width;
            height = txt.height * einsEntspricht;
        }
        else if (txt.height > txt.width)
        {
            einsEntspricht = (float)height / txt.height;
            width = txt.width * einsEntspricht;
        }

        image.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }

    public void ShowSpeiseVonKarte(Speise sT)
    {
        speisenTable = sT;

        txt = new Texture2D(2, 2);

        //txt.SetPixels(speisenTable.PixelsString);// Muss noch in color convertiert werden

        sprite = Sprite.Create(txt, new Rect(0, 0, txt.width, txt.height), new Vector2(0.5f, 0.5f), 1f);

        image.GetComponent<Image>().sprite = sprite;

        VorschaubildSeitenverhältnis();

        titel.text = speisenTable.Titel;

        beschreibung.text = speisenTable.Beschreibung;

        preis.text = speisenTable.Preis + " €";
    }

    public void Close()
    {
        GameObject.Destroy(this.gameObject);
    }
}
