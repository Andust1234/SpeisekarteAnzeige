using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeiseControll : MonoBehaviour
{
    public Read.SpeisenTable speisenTable { get; set; }
    public GameObject speiseAnzeige;
    public GameObject image;
    public Text titel;
    public Text preis;

    private Texture2D txt;
    private Sprite sprite;

    private void Start()
    {
        txt = new Texture2D(2, 2);

        txt.LoadImage(speisenTable.Bild);

        sprite = Sprite.Create(txt, new Rect(0, 0, txt.width, txt.height), new Vector2(0.5f, 0.5f), 1f);

        image.GetComponent<Image>().sprite = sprite;

        VorschaubildSeitenverhältnis();

        titel.text = speisenTable.Titel;

        preis.text = speisenTable.Preis + " €";
    }

    private void VorschaubildSeitenverhältnis()
    {
        float width = 200;
        float height = 200;
        float einsEntspricht = 0;

        if (txt.width > txt.height)
        {
            einsEntspricht = (float)200 / txt.width;
            height = txt.height * einsEntspricht;
        }
        else if (txt.height > txt.width)
        {
            einsEntspricht = (float)200 / txt.height;
            width = txt.width * einsEntspricht;
        }

        image.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }

    public void ShowSpeise()
    {
        GameObject showSpeise;

        showSpeise = Instantiate(speiseAnzeige, this.transform.parent.transform.parent.transform);

        showSpeise.GetComponent<ShowSpeiseControll>().speisenTable = speisenTable;
    }
}
