using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject diagramPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject aimDot;
    [SerializeField] GameObject endScreen;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool isActive = diagramPanel.activeInHierarchy;
            diagramPanel.SetActive(!isActive);
            if (gameManager.gameOver)
            {
                endScreen.SetActive(!endScreen.activeSelf);
            }
            else
            {
                aimDot.SetActive(!aimDot.activeSelf);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.timeScale > 0)
            {
                aimDot.SetActive(false);
                Time.timeScale = 0;
                pausePanel.SetActive(true);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FreeFlyCamera>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                pausePanel.SetActive(false);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FreeFlyCamera>().enabled = true;
                Time.timeScale = 1; 
                aimDot.SetActive(true);
            }
        }

    }
}
