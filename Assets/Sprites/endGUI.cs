using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGUI : MonoBehaviour
{
    public void quit() {
        string url = PlayerPrefs.GetString("play-url");
        Debug.Log(url);
        Application.OpenURL(PlayerPrefs.GetString("play-url").Replace("\\", ""));
        Application.Quit();
    }
}
