using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SavePath : MonoBehaviour
{
    void Start()
    {
        this.gameObject.GetComponent<TMP_Text>().text = Application.persistentDataPath;
    }
}
