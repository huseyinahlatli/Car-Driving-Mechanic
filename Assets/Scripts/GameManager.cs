using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private GameObject canvasPanel;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject carDrivingText;
    
    private float countDownTo = 0.0f;

    private void Awake()
    {
        AwakeFunctions();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        }
    }

    void FixedUpdate()
    {
        countDownTo += Time.deltaTime;
        if (countDownTo > 0)
            countDownText.text = Mathf.Round(countDownTo) + " s";
        timerText.text = "Time: " + DateTime.Now.ToString("hh:MM:ss");
    }

    public void StartButton()
    {
        Time.timeScale = 1f;
        canvasPanel.SetActive(false);
        startButton.SetActive(false);
        carDrivingText.SetActive(false);
    }

    private void AwakeFunctions()
    {
        startButton.SetActive(true);
        canvasPanel.SetActive(true);
        carDrivingText.SetActive(true);
        Time.timeScale = 0F;
    }
}
