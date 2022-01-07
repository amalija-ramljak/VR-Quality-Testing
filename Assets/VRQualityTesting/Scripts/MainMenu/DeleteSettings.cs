using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSettings : MonoBehaviour
{
    public void deleteSettings()
    {
        PlayerPrefs.DeleteAll();
    }
}
