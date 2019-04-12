using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SpeiseControll : MonoBehaviour
{
    public Speise speisenTable { get; set; }
    public GameObject speiseAnzeige;
    public GameObject image;
    public Text titel;
    public Text preis;

    public GameObject editButton;

    private Texture2D txt;
    private Sprite sprite;

    private Thread loadImageThread;

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

    private void LoadImage()
    {

    }

    private void Update()
    {
        editButton.SetActive(ControllerScript.GetAdminMode());
    }

    private void VorschaubildSeitenverhältnis()
    {
        float width = image.GetComponent<RectTransform>().sizeDelta.x;
        float height = image.GetComponent<RectTransform>().sizeDelta.x;
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

    public void ShowSpeise()
    {
        GameObject showSpeise;

        if(this.gameObject.transform.parent.name.Equals("Vorschau"))
            showSpeise = Instantiate(speiseAnzeige, this.transform.parent.transform);
        else
            showSpeise = Instantiate(speiseAnzeige, this.transform.parent.transform.parent.transform);

        showSpeise.GetComponent<ShowSpeiseControll>().ShowSpeiseVonKarte(speisenTable);
    }
}
