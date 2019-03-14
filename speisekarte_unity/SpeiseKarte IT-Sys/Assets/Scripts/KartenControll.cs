using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KartenControll : MonoBehaviour
{
    public GameObject speisePrefab;

    private Read.SpeisenTable[] speisenTable;
    private int breite;
    private int höhe;

    private void Start()
    {
        this.gameObject.GetComponent<ScrollRect>().viewport = this.gameObject.transform.parent.GetComponent<RectTransform>();
    }

    public void SetSpeisenTable(Read.SpeisenTable[] table)
    {
        speisenTable = table;

        SetGröße();

        SetSpeisen();
    }

    private void SetGröße()
    {
        //if (speisenTable.Length <= 4)
        //    breite = 20 + (270 * speisenTable.Length);
        //else
        //    breite = 1100;

        breite = 1100;

        höhe = 20 + (320 * (speisenTable.Length/4));

        if (speisenTable.Length % 4 != 0)
            höhe += 320;

        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(breite, höhe);

        if (höhe < Screen.height)
            this.gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
    }

    private void SetSpeisen()
    {
        GameObject speise;
        int x = 0;
        int y = 0;

        for(int i = 0; i < speisenTable.Length; i++)
        {
            speise = Instantiate(speisePrefab, this.transform);

            speise.GetComponent<SpeiseControll>().speisenTable = speisenTable[i];

            x = 20 + ((i % 4) * 270);

            y = -1 * (20 + ((i / 4) * 320));

            speise.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        }
    }
}
