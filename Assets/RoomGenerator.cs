using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    public void createGameCode() {
        string game = PlayerPrefs.GetString("game-reference");
        if (game == "")
        {
            StartCoroutine(GetCode());
        }
        else {
            StartCoroutine(getScene());
        }
    
    }

    IEnumerator GetCode()
    {
        UnityWebRequest www = UnityWebRequest.Post("https://blooming-headland-11418.herokuapp.com/api/create-game", "");
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(www.downloadHandler.text);
            string game = values["game-reference"];
            PlayerPrefs.SetString("game-reference", game);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Rocas01");
        }
    }

    IEnumerator getScene() {
        string url = "https://blooming-headland-11418.herokuapp.com/api/get-game-data/" + PlayerPrefs.GetString("game-reference");
        Debug.Log(url);
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // 0 = New, 1 = Wordle, 2 = Puzzle
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(www.downloadHandler.text);
            string actualLVL = values["solved"];
            Debug.Log(actualLVL);
            switch (actualLVL) {
                case "0":
                    PlayerPrefs.SetInt("wordle", 1);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("Rocas01");
                    break;
                case "1":
                    PlayerPrefs.SetInt("wordle", 0);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("Rocas01");
                    break;
                case "2":
                    SceneManager.LoadScene("Cuphead");
                    break;

            }
        }
    }
}
