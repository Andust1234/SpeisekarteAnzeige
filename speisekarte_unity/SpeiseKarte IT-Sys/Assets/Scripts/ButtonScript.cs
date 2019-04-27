using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private SpeiseArt speiseArt;
    public SpeiseArt SpeiseArt
    {
        set
        {
            speiseArt = value;

            SetupButton();
        }
    }

    public ControllerScript controllerScript;

    private Button selfButton;

    private void SetupButton()
    {
        transform.GetChild(0).GetComponent<Text>().text = speiseArt.SpeisenArt;

        selfButton = GetComponent<Button>();
        selfButton.onClick.AddListener(() => controllerScript.LoadAnzeige(speiseArt.ID));
    }
}
