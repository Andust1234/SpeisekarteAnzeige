using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KartenControll : MonoBehaviour
{
    public GameObject speisePrefab;
    public int platzZwischenSpeisen = 20;

    private Speise[] speisen;
    public Speise[] Speisen
    {
        set
        {
            speisen = value;

            SetGröße();

            StartCoroutine(SetSpeisen());
        }
    }
    private float breite;
    private float höhe;
    private int speisenNebeneinander;
    private float speiseBreite;
    private float speiseHöhe;

    private void Start()
    {
        this.gameObject.GetComponent<ScrollRect>().viewport = this.gameObject.transform.parent.GetComponent<RectTransform>();
    }

    private void SetGröße()
    {
        speiseBreite = speisePrefab.GetComponent<RectTransform>().sizeDelta.x;
        speiseHöhe = speisePrefab.GetComponent<RectTransform>().sizeDelta.y;
        
        breite = this.gameObject.GetComponent<RectTransform>().rect.width;

        speisenNebeneinander = (int)(breite / speiseBreite);

        höhe = platzZwischenSpeisen + ((speiseHöhe + platzZwischenSpeisen) * (speisen.Length / speisenNebeneinander));

        if (speisen.Length % speisenNebeneinander != 0)
            höhe += speiseHöhe + platzZwischenSpeisen;
        
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f, höhe);
    }

    private IEnumerator SetSpeisen()
    {
        float x = 0;
        float y = 0;

        for(int i = 0; i < speisen.Length; i++)
        {
            GameObject speise = Instantiate(speisePrefab, this.transform) as GameObject;

            speise.GetComponent<SpeiseControll>().Speise = speisen[i];

            x = platzZwischenSpeisen + ((i % speisenNebeneinander) * (speiseBreite + (platzZwischenSpeisen)));

            y = -1 * (platzZwischenSpeisen + ((i / speisenNebeneinander) * (speiseHöhe + platzZwischenSpeisen)));

            speise.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

            yield return null;
        }
    }
}
