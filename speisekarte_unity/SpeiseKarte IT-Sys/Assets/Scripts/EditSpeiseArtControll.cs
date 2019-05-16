using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditSpeiseArtControll : MonoBehaviour
{
    private SpeiseArt[] speiseArten;

    private Read read;
    private Insert insert;
    private Delete delete;
    private ControllerScript controllerScript;

    public GameObject editSpeiseArtPrefab;
    public GameObject createSpeiseArtPrefab;

    private void Awake()
    {
        read = this.gameObject.AddComponent<Read>();
        insert = this.gameObject.AddComponent<Insert>();

        controllerScript = GameObject.Find("Controller").GetComponent<ControllerScript>();

        speiseArten = ReadSpeiseArten();

        SetEditButtons(speiseArten);
    }

    private SpeiseArt[] ReadSpeiseArten()
    {
        return read.ReadSpeisenArtTable("SELECT * FROM speisenart");
    }

    private void SetEditButtons(SpeiseArt[] sA)
    {
        GameObject gameObj;
        RectTransform rect;

        for (int i = 0; i < sA.Length; i++)
        {
            gameObj = Instantiate(editSpeiseArtPrefab, this.transform);

            gameObj.GetComponent<EditSpeiseArt>().SetUp(sA[i], read.CountRowsWhitID(sA[i].ID), this);

            rect = gameObj.GetComponent<RectTransform>();

            rect.anchoredPosition = new Vector2(15f, -60f -(rect.sizeDelta.y * i));
        }

        gameObj = Instantiate(createSpeiseArtPrefab, this.transform);

        gameObj.GetComponent<CreateSpeiseArt>().SetUp(this);

        rect = gameObj.GetComponent<RectTransform>();

        rect.anchoredPosition = new Vector2(15f, -60f - (rect.sizeDelta.y * sA.Length));
    }

    public void Rename(SpeiseArt sA)
    {
        insert = gameObject.AddComponent<Insert>();

        insert.UpdateSpeiseArtInDatebase(sA);

        controllerScript.LoadButton();
        controllerScript.LoadEditSpeiseArt();
    }

    public void Delete(SpeiseArt sA)
    {
        delete = gameObject.AddComponent<Delete>();

        delete.DeleteSpeiseArt(sA);

        controllerScript.LoadButton();
        controllerScript.LoadEditSpeiseArt();
    }

    public void Create(SpeiseArt sA)
    {
        insert = gameObject.AddComponent<Insert>();

        insert.InsertSpeiseArtInDataBase(sA);

        controllerScript.LoadButton();
        controllerScript.LoadEditSpeiseArt();
    }
}
