using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public int speisenArtID;

    private ControllerScript controllerScript;
    private Button selfButton;

    private void Start()
    {
        controllerScript = GameObject.Find("Controller").GetComponent<ControllerScript>();

        selfButton = GetComponent<Button>();
        selfButton.onClick.AddListener(() => controllerScript.LoadAnzeige(speisenArtID));
    }

}
