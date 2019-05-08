using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerScript : MonoBehaviour
{
    public GameObject loginPrefab;
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
    public static bool schoner = false;
    public bool logedIn = false;

    private Read read;

    private SpeiseArt[] speiseArten;

    private void Awake()
    {
        GameObject logIn = Instantiate(loginPrefab, anzeige.transform);

        logIn.GetComponent<LogInDB>().ControllerScript = this;
    }

    public void StartSpeisekarte()
    {
        ClearAnzeige();

        LoadButton();

        Instantiate(showStartPrefab, anzeige.transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12) && logedIn)
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

        if (speiseArten.Length != 0)
            schoner = true;
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
            buttonRect.anchoredPosition = new Vector2(0f , 0f -(buttonRect.sizeDelta.y * i));
        }
    }

    public void LoadAnzeige(int id)
    {
        ClearAnzeige();

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
        ClearAnzeige();

        GameObject createSpeise;

        createSpeise =  Instantiate(createSpeisePrefab, anzeige.transform);

        createSpeise.GetComponent<CreateControll>().SpeiseArten = speiseArten;
    }

    public void LoadEditSpeise(Speise speise)
    {
        ClearAnzeige();

        GameObject createSpeise;

        createSpeise = Instantiate(createSpeisePrefab, anzeige.transform);

        createSpeise.GetComponent<CreateControll>().SpeiseArten = speiseArten;

        createSpeise.GetComponent<CreateControll>().Speise = speise;

        createSpeise.GetComponent<CreateControll>().editMode = true;
    }

    public void LoadEditSpeiseArt()
    {
        ClearAnzeige();

        Instantiate(createSpeisenArtPrefab, anzeige.transform);
    }

    private void ClearAnzeige()
    {
        foreach (Transform child in anzeige.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
