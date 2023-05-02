using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
public class FadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup myUIGroup;
    [SerializeField] public GameObject FadeObject;
    [SerializeField] private bool fadeOut = false;

    void Start()
    {
        /*
        InputManager.EnableInput();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        */
        Time.timeScale = 1f;
        //FadeObject.SetActive(true);
        HideUI();
    }
    
    public void HideUI()
    {
        //FadeObject.SetActive(true);
        myUIGroup.alpha = 1;
    }
    

    private void Update()
    {
        if (fadeOut)
        {
            if (myUIGroup.alpha >= 0)
            {
                myUIGroup.alpha -= Time.deltaTime * .35f;
                if (myUIGroup.alpha <= 0)
                {
                    fadeOut = false;
                    FadeObject.SetActive(false);
                }
            }
        }

    }
}
