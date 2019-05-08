using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateSpeiseArt : MonoBehaviour
{
    public InputField speiseArtName;
    public Button create;

    private EditSpeiseArtControll editSpeiseArtControll;
    private SpeiseArt speiseArt;

    private void Awake()
    {
        speiseArt = new SpeiseArt();

        create.interactable = false;
    }

    public void SetUp(EditSpeiseArtControll eSC)
    {
        editSpeiseArtControll = eSC;

        create.onClick.AddListener(() => editSpeiseArtControll.Create(speiseArt));
    }

    public void ChangeText()
    {
        speiseArt.SpeisenArt = speiseArtName.text;

        if (!speiseArtName.text.Equals(""))
            create.interactable = true;
        else
            create.interactable = false;
    }
}
