using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RandomControll : MonoBehaviour
{
    public int zeitBisSchoner = 10;
    public int zeitBisShowSpeise = 1;
    public int zeitShowSpeise = 5;
    public bool random;

    private List<GameObject> speisearten;
    private List<GameObject> speisen;

    private bool isActive = false;

    private Stopwatch stopwatch;

    private Coroutine lastCoro;
    private GameObject showSpeise;

    private void Awake()
    {
        speisearten = new List<GameObject>();
        speisen = new List<GameObject>();
        stopwatch = new Stopwatch();

        Invoke("StartStopwatch", 0);
    }

    private void StartStopwatch()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 || ControllerScript.GetAdminMode() || !ControllerScript.schoner )
        {
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
                stopwatch.Reset();
            }
        }
        else
        {
            if(!stopwatch.IsRunning)
                stopwatch.Start();
        }

        if(stopwatch.Elapsed.Seconds >= zeitBisSchoner)
        {
            if (!isActive)
            {
                isActive = true;

                lastCoro = StartCoroutine(StartSchoner());
            }
        }
        else
        {
            if (isActive)
            {
                isActive = false;

                StopCoroutine(lastCoro);

                speisearten.Clear();
                speisen.Clear();
            }
        }

        Invoke("StartStopwatch", 0);
    }

    private IEnumerator StartSchoner()
    {
        GetSpeiseArten();

        while (true)
        {
            if (speisen.Count == 0)
            {
                LoadKarte();

                yield return null;

                GetSpeisen();
            }

            yield return new WaitForSeconds(zeitBisShowSpeise);

            if (speisen.Count != 0)
            {
                SpeisenDurchblättern();

                yield return new WaitForSeconds(zeitShowSpeise);

                showSpeise.GetComponent<ShowSpeiseControll>().Close();
            }
        }
    }

    private void LoadKarte()
    {
        if (speisearten.Count > 0)
        {
            GameObject gameObj;

            if (random)
            {
                int i = GetZufallsZahl(speisearten.Count);

                gameObj = speisearten[i];

                speisearten.RemoveAt(i);
            }
            else
            {
                gameObj = speisearten[0];

                speisearten.RemoveAt(0);
            }

            int gameObjInt = gameObj.GetComponent<ButtonScript>().GetSpeiseArtID();

            gameObj.GetComponent<ButtonScript>().controllerScript.LoadAnzeige(gameObjInt);
        }
        else
        {
            StopCoroutine(lastCoro);
            isActive = false;
        }
    }

    private void SpeisenDurchblättern()
    {
        GameObject gameObj;

        if (random)
        {
            int i = GetZufallsZahl(speisen.Count);

            gameObj = speisen[i];

            speisen.RemoveAt(i);
        }
        else
        {
            gameObj = speisen[0];

            speisen.RemoveAt(0);
        }

        showSpeise = gameObj.GetComponent<SpeiseControll>().ShowSpeiseSchoner();
    }

    private void GetSpeiseArten()
    {
        GameObject[] speiseArtenArr = GameObject.FindGameObjectsWithTag("SpeiseArt");

        foreach (GameObject gameObj in speiseArtenArr)
        {
            speisearten.Add(gameObj);
        }
    }

    private void GetSpeisen()
    {
        GameObject[] speisenArr = GameObject.FindGameObjectsWithTag("Speise");
        
        foreach (GameObject gameObj in speisenArr)
        {
            if(gameObj != null)
                speisen.Add(gameObj);
        }
    }

    private int GetZufallsZahl(int anzahl)
    {
        return (int)Random.Range(0f, (float)anzahl);
    }
}
