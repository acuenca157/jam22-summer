using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Slider slider;

    public void addPoints(int points)
    {
        slider.value += points;

        if (slider.value >= slider.maxValue) {
            SceneManager.LoadScene("Muerte");
        }
    }
}
