using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void quitApplication()
    {
        Application.Quit();
    }
    
    public void resume(GameObject pausePanel)
    {
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FreeFlyCamera>().enabled = true;
        pausePanel.SetActive(false);
    }

    public void diagrammeAnzeigen(GameObject diagramme)
    {
        GameObject.FindGameObjectWithTag("EndScreen").SetActive(false);
        diagramme.SetActive(true);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("Menü");
    }
}
