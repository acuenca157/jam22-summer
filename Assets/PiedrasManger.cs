using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PiedrasManger : MonoBehaviour
{
    [SerializeField] GameObject[] piedrasPoints;
    [SerializeField] GameObject piedra;

    List<GameObject> piedrasPointsList;
    string word = "ejemplo";
    // Start is called before the first frame update
    void Start()
    {
        piedrasPointsList = piedrasPoints.ToList();
        for (int i = 0; i < word.Length ; i++) {
            generateRock();
        }
    }

    void generateRock() {
        int index = Random.Range(0, piedrasPointsList.Count - 1);
        Instantiate(piedra, piedrasPointsList[index].transform.position, Quaternion.identity);
        piedrasPointsList.RemoveAt(index);
    }
}
