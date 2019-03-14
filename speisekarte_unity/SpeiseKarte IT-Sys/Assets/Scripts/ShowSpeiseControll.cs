using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSpeiseControll : MonoBehaviour
{
    public Read.SpeisenTable speisenTable { get; set; }

    public void Close()
    {
        GameObject.Destroy(this.gameObject);
    }
}
