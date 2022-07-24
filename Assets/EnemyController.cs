using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Slider slider;

    public void addPoints(int points)
    {
        slider.value += points;

        if (slider.value >= slider.maxValue) {
            StartCoroutine(muerePuto());
        }
    }

    IEnumerator muerePuto() {

        WWWForm form = new WWWForm();
        if (PlayerPrefs.GetInt("wordle") == 1)
        {
            form.AddField("name", "wordle");
            form.AddField("reference", PlayerPrefs.GetString("game-reference"));
            form.AddField("data", PlayerPrefs.GetString("discovered-letters"));
        }
        else
        {
            form.AddField("name", "puzzle");
            form.AddField("reference", PlayerPrefs.GetString("game-reference"));
            form.AddField("data", PlayerPrefs.GetInt("photos"));
        }

        using (UnityWebRequest www = UnityWebRequest.Post("https://blooming-headland-11418.herokuapp.com/api/update-game-status", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                PlayerPrefs.SetString("play-url", www.downloadHandler.text);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Muerte");

            }
        }

    }
}
