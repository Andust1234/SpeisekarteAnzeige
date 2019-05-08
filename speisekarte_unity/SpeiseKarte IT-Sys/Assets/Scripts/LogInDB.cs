using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LogInDB : MonoBehaviour
{
    private ControllerScript controllerScript;
    public ControllerScript ControllerScript
    {
        set
        {
            controllerScript = value;

            StartLogin();
        }
    }
    public InputField serverInput;
    public InputField databaseInput;
    public InputField userInput;
    public InputField passwordInput;
    public GameObject fehlschlagText;

    private Connect connect;
    private MySQLConfig mySQLConfig;
    private string pathMySQLConfig;

    private void StartLogin()
    {
        pathMySQLConfig = Application.dataPath + "/Resources/MySQL.json";

        if (!File.Exists(pathMySQLConfig))
        {
            CreateMySQLConfig();
        }

        CheckLoginDaten();
    }

    private void CreateMySQLConfig()
    {
        mySQLConfig = new MySQLConfig();

        string dataAsJson = JsonUtility.ToJson(mySQLConfig);

        File.WriteAllText(pathMySQLConfig, dataAsJson);
    }

    private void WriteMySQLConfig(MySQLConfig mySQLC)
    {
        string dataAsJson = JsonUtility.ToJson(mySQLC);

        File.WriteAllText(pathMySQLConfig, dataAsJson);
    }

    private bool CheckLoginDaten()
    {
        connect = this.gameObject.AddComponent<Connect>();

        if (connect.TryConnection())
        {
            controllerScript.logedIn = true;
            controllerScript.StartSpeisekarte();
            return true;
        }
        else
        {
            Destroy(connect);

            return false;
        }
    }

    public void EnterLoginInput()
    {
        mySQLConfig = new MySQLConfig();

        mySQLConfig.host = serverInput.text;
        mySQLConfig.database = databaseInput.text;
        mySQLConfig.user = userInput.text;
        mySQLConfig.password = passwordInput.text;

        WriteMySQLConfig(mySQLConfig);

        if (!CheckLoginDaten())
        {
            if(!fehlschlagText.activeSelf)
                fehlschlagText.SetActive(true);
        }
    }
}
