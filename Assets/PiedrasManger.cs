using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PiedrasManger : MonoBehaviour
{
    [SerializeField] GameObject[] piedrasPoints;
    [SerializeField] GameObject piedra;

    List<GameObject> piedrasPointsList;
    string word = "";

    private void Start()
    {
        if (PlayerPrefs.GetInt("wordle") == 1)
        {
            StartCoroutine(getWord());
            PlayerPrefs.SetString("discovered-letters", "");
            PlayerPrefs.Save();
        }
        else {
            PlayerPrefs.SetInt("photos", 0);
            PlayerPrefs.Save();
            word = "12345678";
            StartGenerate();
        }
    }

    private IEnumerator getWord()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://blooming-headland-11418.herokuapp.com/api/get-word/" + PlayerPrefs.GetString("game-reference"));
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(www.downloadHandler.text);
            word = values["word"];
            StartGenerate();
        }
    }

    void StartGenerate()
    {
        piedrasPointsList = piedrasPoints.ToList();
        for (int i = 0; i < word.Length ; i++) {
            generateRock(i);
        }
    }

    void generateRock(int i) {
        int index = Random.Range(0, piedrasPointsList.Count - 1);
        GameObject piedraObj = Instantiate(piedra, piedrasPointsList[index].transform.position, Quaternion.identity);

        if(PlayerPrefs.GetInt("wordle") == 0)
            piedraObj.GetComponent<RockController>().damagePoints = 5;

        piedraObj.GetComponent<RockController>().letter = word.ToCharArray()[i].ToString();
        piedrasPointsList.RemoveAt(index);
    }
}


