using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseManager : MonoBehaviour
{
    public static LoseManager Instance;
    public TextMeshProUGUI scoreText;
    public GameObject GameOverScreen;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Lose()
    {
        StateManager.SwitchGameOverMode();
        GameOverScreen.SetActive(true);
        scoreText.text = House.instance.Score.ToString();
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }
    }
}