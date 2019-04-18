using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using System.IO;


public class CreateControll : MonoBehaviour
{
    public InputField titel;
    public InputField beschreibung;
    public InputField preis;
    public Dropdown dropdown;

    public SpeiseControll vorschauSpeise;

    private SpeiseArt[] speiseArten;
    public SpeiseArt[] SpeiseArten
    {
        set
        {
            speiseArten = value;

            SetupDropDown();
        }
    }

    private Speise speise;
    public Speise Speise
    {
        set
        {
            speise = value;

            SetupForm();
        }
    }

    private Texture2D txt;
    private Insert insert;
    private InsertBild insertBild;

    [DllImport("user32.dll")]
    private static extern void OpenFileDialog();

    public void UploadImage()
    {
        System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();

        ofd.Filter = "Image files (*.jpg;*.png)|*.jpg;*png";

        ofd.ShowDialog();

        StartCoroutine(LoadImage(ofd.FileName));
    }

    private IEnumerator LoadImage(string str)
    {
        using ( UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(str))
        {
            yield return uwr.SendWebRequest();

            if(uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                txt = new Texture2D(2, 2, TextureFormat.RGB24, false);

                txt = DownloadHandlerTexture.GetContent(uwr);

                BildRawData brd = new BildRawData();

                brd.BildHight = txt.height;
                brd.BildWidth = txt.width;
                brd.BildRaw = txt.GetRawTextureData();

                speise.Bild = brd;

                UpdateInput();
            }
        }
    }

    private void SetupDropDown()
    {
        List<string> dropdownOptions = new List<string>();

        foreach(SpeiseArt art in speiseArten)
        {
            dropdownOptions.Add(art.SpeisenArt);
        }

        dropdown.ClearOptions();

        dropdown.AddOptions(dropdownOptions);
    }

    private void SetupForm()
    {
        titel.text = speise.Titel;
        beschreibung.text = speise.Beschreibung;
        preis.text = speise.Preis.ToString();
        dropdown.value = speise.SpeisenArt_ID + 1;
    }

    public void UpdateInput()
    {
        StartCoroutine(UpdateInputCoro());
    }

    private IEnumerator UpdateInputCoro()
    {
        yield return null;

        speise.Titel = titel.text;
        speise.Beschreibung = beschreibung.text;
        speise.Preis = preis.text;

        vorschauSpeise.GetComponent<SpeiseControll>().Speise = speise;
    }

    public void Save()
    {
        insert = this.gameObject.AddComponent<Insert>();
        insertBild = this.gameObject.AddComponent<InsertBild>();

        //string sqlText = "INSERT INTO speisekarte(titel, preis, beschreibung, speiseart_id) " +
        //    "VALUES('" + speise.Titel + "', '" + speise.Preis + "', '" + speise.Beschreibung + "', '" + speise.SpeisenArt_ID + "')";

        //insert.InsertToDatabase(sqlText);

        //string sqlTextBild = "UPDATE speisekarte SET bild = @img WHERE id = " + insert.GetLastInsertedId();

        //insertBild.InsertBildInDatenbank(sqlTextBild, speise.Bild.BildRaw);

        insert.InsertSpeiseInDatabase(speise);
    }
}
