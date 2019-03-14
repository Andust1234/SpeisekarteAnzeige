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

    private Read read;

    private void Awake()
    {
        read = this.gameObject.AddComponent<Read>();

        Read.SpeisenArtTable[] table = read.ReadSpeisenArtTable("SELECT * FROM speisenart");

        for (int i = 0; i < table.Length; i++)
        {
            GameObject button;
            Read.SpeisenArtTable speisenArt = new Read.SpeisenArtTable();

            button = Instantiate(buttonPrefab, buttons.transform);
            speisenArt = table[i];

            button.transform.GetChild(0).GetComponent<Text>().text = speisenArt.SpeisenArt;
            button.GetComponent<ButtonScript>().speisenArtID = speisenArt.ID;

            RectTransform buttonRect = button.GetComponent<RectTransform>();
            buttonRect.anchoredPosition = new Vector2(buttonRect.anchoredPosition.x, -12.5f + (-42.5f * i));
        }

        Instantiate(showStartPrefab, anzeige.transform);
    }

    public void LoadAnzeige(int id)
    {
        foreach(Transform child in anzeige.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        GameObject karte;

        karte = Instantiate(showKartePrefab, anzeige.transform);

        karte.GetComponent<KartenControll>().SetSpeisenTable(read.ReadSpeiseTable("SELECT speisekarte.ID, speisekarte.Titel, speisekarte.Bild, speisekarte.Preis, speisekarte.Beschreibung, speisenart.SpeisenArtName FROM speisekarte INNER JOIN speisenart ON " + id + " = speisenart.ID WHERE speisekarte.SpeisenArt_ID=" + id));
    }
}
