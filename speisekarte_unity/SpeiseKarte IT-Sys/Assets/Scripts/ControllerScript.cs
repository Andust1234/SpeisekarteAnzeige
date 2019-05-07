using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerScript : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject createSpeisePrefab;
    public GameObject createSpeisenArtPrefab;
    public GameObject showKartePrefab;
    public GameObject showSpeisePrefab;
    public GameObject showStartPrefab;

    public GameObject buttons;
    public GameObject anzeige;

    public GameObject editSpeiseArten;
    public GameObject createSpeise;

    private static bool adminMode = false;

    private Read read;
    private RandomControll randomControll;

    private SpeiseArt[] speiseArten;

    private void Awake()
    {
        LoadButton();

        randomControll = gameObject.AddComponent<RandomControll>();

        Instantiate(showStartPrefab, anzeige.transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            ControllerScript.SetAdminMode(!ControllerScript.GetAdminMode());
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (adminMode)
        {
            if (!createSpeise.activeSelf)
                createSpeise.SetActive(true);

            if (!editSpeiseArten.activeSelf)
                editSpeiseArten.SetActive(true);
        }
        else
        {
            if (createSpeise.activeSelf)
                createSpeise.SetActive(false);

            if (editSpeiseArten.activeSelf)
                editSpeiseArten.SetActive(false);
        }
    }

    public void LoadButton()
    {
        foreach (Transform child in buttons.transform)
        {
            if(child.CompareTag("SpeiseArt"))
                GameObject.Destroy(child.gameObject);
        }

        read = this.gameObject.AddComponent<Read>();

        speiseArten = read.ReadSpeisenArtTable("SELECT * FROM speisenart");

        for (int i = 0; i < speiseArten.Length; i++)
        {
            GameObject button;

            button = Instantiate(buttonPrefab, buttons.transform);

            button.GetComponent<ButtonScript>().controllerScript = this;
            button.GetComponent<ButtonScript>().SpeiseArt = speiseArten[i];

            RectTransform buttonRect = button.GetComponent<RectTransform>();
            buttonRect.anchoredPosition = new Vector2(buttonRect.anchoredPosition.x, -12.5f + (-42.5f * i));
        }
    }

    public void LoadAnzeige(int id)
    {
        ClaerAnzeige();

        GameObject karte;

        karte = Instantiate(showKartePrefab, anzeige.transform);

        karte.GetComponent<KartenControll>().Speisen = read.ReadSpeisen(id);
    }

    private static void SetAdminMode(bool b)
    {
        adminMode = b;
    }

    public static bool GetAdminMode()
    {
        return adminMode;
    }

    public void LoadCreatSpeise()
    {
        ClaerAnzeige();

        GameObject createSpeise;

        createSpeise =  Instantiate(createSpeisePrefab, anzeige.transform);

        createSpeise.GetComponent<CreateControll>().SpeiseArten = speiseArten;
    }

    public void LoadEditSpeise(Speise speise)
    {
        ClaerAnzeige();

        GameObject createSpeise;

        createSpeise = Instantiate(createSpeisePrefab, anzeige.transform);

        createSpeise.GetComponent<CreateControll>().SpeiseArten = speiseArten;

        createSpeise.GetComponent<CreateControll>().Speise = speise;

        createSpeise.GetComponent<CreateControll>().editMode = true;
    }

    public void LoadEditSpeiseArt()
    {
        ClaerAnzeige();

        Instantiate(createSpeisenArtPrefab, anzeige.transform);
    }

    private void ClaerAnzeige()
    {
        foreach (Transform child in anzeige.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
