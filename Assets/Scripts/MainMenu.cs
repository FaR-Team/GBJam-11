using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator blackFade;
    [SerializeField] private SpriteRenderer controles;

    private int value = 0;
    bool IsAlreadyChangingScene;
    void Update()
    {
        if (Input.anyKeyDown && !IsAlreadyChangingScene)
        {
            value++;
            switch (value)
            {
                case 0:
                    ActivateControllerScreen(false);
                    break;

                case 1:
                    ActivateControllerScreen(true);
                    break;
                case 2:
                    ActivateControllerScreen(false);
                    break;
                default:
                    blackFade.SetTrigger("StartBlackFade");
                    Invoke("LoadGame", 1.5f);
                    IsAlreadyChangingScene = true;
                    break;
            }
        }
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    private void ActivateControllerScreen(bool setActive)
    {
        controles.enabled = setActive;
    }
}
