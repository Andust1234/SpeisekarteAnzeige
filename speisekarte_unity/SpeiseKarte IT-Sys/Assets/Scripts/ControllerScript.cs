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
            if(!createSpeise.activeSelf)
                createSpeise.SetActive(true);
        }
        else
        {
            if (createSpeise.activeSelf)
                createSpeise.SetActive(false);
        }
    }

    public void LoadButton()
    {
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
        foreach(Transform child in anzeige.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        GameObject karte;

        karte = Instantiate(showKartePrefab, anzeige.transform);

        karte.GetComponent<KartenControll>().Speisen = read.ReadSpeisen(id);
    }

    public static void SetAdminMode(bool b)
    {
        adminMode = b;
    }

    public static bool GetAdminMode()
    {
        return adminMode;
    }

    public void LoadCreatSpeise()
    {
        foreach (Transform child in anzeige.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        GameObject createSpeise;

        createSpeise =  Instantiate(createSpeisePrefab, anzeige.transform);

        createSpeise.GetComponent<CreateControll>().SpeiseArten = speiseArten;
    }

    public void LoadEditSpeise(Speise speise)
    {
        foreach (Transform child in anzeige.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        GameObject createSpeise;

        createSpeise = Instantiate(createSpeisePrefab, anzeige.transform);

        createSpeise.GetComponent<CreateControll>().SpeiseArten = speiseArten;

        createSpeise.GetComponent<CreateControll>().Speise = speise;

        createSpeise.GetComponent<CreateControll>().editMode = true;
    }
}
