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

    private Speise speise;
    public Speise Speise
    {
        get
        {
            return speise;
        }
        set
        {
            speise = value;

            ShowSpeiseVonKarte();
        }
    }
    private Texture2D txt;
    private Sprite sprite;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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

    public void ShowSpeiseVonKarte()
    {
        if (speise.Bild.BildRaw != null)
        {
            txt = new Texture2D(speise.Bild.BildWidth, speise.Bild.BildHight, TextureFormat.RGB24, false);

            txt.LoadRawTextureData(speise.Bild.BildRaw);

            txt.Apply();

            sprite = Sprite.Create(txt, new Rect(0, 0, txt.width, txt.height), new Vector2(0.5f, 0.5f), 1f);

            image.GetComponent<Image>().sprite = sprite;

            VorschaubildSeitenverhältnis();
        }

        titel.text = speise.Titel;

        beschreibung.text = speise.Beschreibung;

        preis.text = speise.Preis + " €";
    }

    public void Close()
    {
        StartCoroutine(CloseCoro());
    }

    private IEnumerator CloseCoro()
    {
        animator.SetTrigger("Close");

        yield return new WaitForSeconds(1);

        GameObject.Destroy(this.gameObject);
    }
}
