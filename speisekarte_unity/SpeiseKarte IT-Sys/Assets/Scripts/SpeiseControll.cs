﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeiseControll : MonoBehaviour
{
    public int id;

    private Speise speise;
    public Speise Speise
    {
        set
        {
            speise = value;

            id = speise.ID;

            SetupSpeise();
            UpdateShowSpeise();
        }
    }
    public GameObject speiseAnzeige;
    public GameObject image;
    public Text titel;
    public Text preis;

    public GameObject deleteButton;
    public GameObject editButton;

    private Texture2D txt;
    private Sprite sprite;

    private GameObject showSpeise;

    private Delete delete;

    private void SetupSpeise()
    {
        if(speise.Bild.BildRaw != null)
        {
            txt = new Texture2D(speise.Bild.BildWidth, speise.Bild.BildHight, TextureFormat.RGB24, false);

            txt.LoadRawTextureData(speise.Bild.BildRaw);

            txt.Apply();

            sprite = Sprite.Create(txt, new Rect(0, 0, txt.width, txt.height), new Vector2(0.5f, 0.5f), 1f);

            image.GetComponent<Image>().sprite = sprite;

            VorschaubildSeitenverhältnis();
        }

        titel.text = speise.Titel;

        preis.text = speise.Preis + " €";
    }

    private void Update()
    {
        ShowButton();
    }

    private void ShowButton()
    {
        if (this.gameObject.transform.parent.name.Equals("ShowKarte(Clone)"))
        {
            if (ControllerScript.GetAdminMode())
            {
                if(!editButton.activeSelf)
                    editButton.SetActive(true);

                if(!deleteButton.activeSelf)
                    deleteButton.SetActive(true);
            }
            else
            {
                if (editButton.activeSelf)
                    editButton.SetActive(false);

                if (deleteButton.activeSelf)
                    deleteButton.SetActive(false);
            }

        }
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

    public GameObject ShowSpeiseSchoner()
    {
        if(this.gameObject.transform.parent.name.Equals("Vorschau"))
            showSpeise = Instantiate(speiseAnzeige, this.transform.parent.transform);
        else
            showSpeise = Instantiate(speiseAnzeige, this.transform.parent.transform.parent.transform);

        showSpeise.GetComponent<ShowSpeiseControll>().Speise = speise;

        return showSpeise;
    }

    public void ShowSpeise()
    {
        if (this.gameObject.transform.parent.name.Equals("Vorschau"))
            showSpeise = Instantiate(speiseAnzeige, this.transform.parent.transform);
        else
            showSpeise = Instantiate(speiseAnzeige, this.transform.parent.transform.parent.transform);

        showSpeise.GetComponent<ShowSpeiseControll>().Speise = speise;
    }

    public void UpdateShowSpeise()
    {
        showSpeise = GameObject.FindGameObjectWithTag("ShowSpeise");

        if (showSpeise != null)
        {
            showSpeise.GetComponent<ShowSpeiseControll>().Speise = speise;
        }
    }

    public void DeleteSpeise()
    {
        delete = gameObject.AddComponent<Delete>();

        delete.DeleteSpeise(speise);

        GameObject.Find("Controller").GetComponent<ControllerScript>().LoadAnzeige(speise.SpeisenArt_ID);
    }

    public void EditSpeise()
    {
        GameObject.Find("Controller").GetComponent<ControllerScript>().LoadEditSpeise(speise);
    }
}
