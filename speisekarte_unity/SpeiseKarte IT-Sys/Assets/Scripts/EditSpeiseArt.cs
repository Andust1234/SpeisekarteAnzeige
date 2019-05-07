using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditSpeiseArt : MonoBehaviour
{
    public InputField speiseArtName;
    public Text text;
    public Button rename;
    public Button delete;

    private EditSpeiseArtControll editSpeiseArtControll;
    private SpeiseArt speiseArt;
    private int anzahlSpeisen;

    public void SetUp(SpeiseArt sA, int aS, EditSpeiseArtControll eSC)
    {
        speiseArt = sA;
        anzahlSpeisen = aS;
        editSpeiseArtControll = eSC;

        speiseArtName.text = speiseArt.SpeisenArt;
        text.text = "Anzahl Speisen: " + anzahlSpeisen;

        if (anzahlSpeisen > 0)
            delete.interactable = false;

        SetUpButton();
    }

    private void SetUpButton()
    {
        rename.onClick.AddListener(() => editSpeiseArtControll.Rename(speiseArt));
        delete.onClick.AddListener(() => editSpeiseArtControll.Delete(speiseArt));
    }

    public void ChangeText()
    {
        speiseArt.SpeisenArt = speiseArtName.text;
    }
}
