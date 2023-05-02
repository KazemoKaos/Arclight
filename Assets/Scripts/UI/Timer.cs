using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timer;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    public string GetTimer => timerText.text;

    void Update()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
