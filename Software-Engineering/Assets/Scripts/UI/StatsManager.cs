using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] List<UILineRenderer> uiLineRenderer;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        //initStats();
    }
    //Subscribe to Event Methods
    private void OnEnable()
    {
        GameManager.onStatTracking += getAverageFoxSpeed;
        GameManager.onStatTracking += getAverageHareSpeed;
        GameManager.onStatTracking += getAverageFoxSight;
        GameManager.onStatTracking += getAverageHareSight;
        GameManager.onStatTracking += getFoxGeneration;
        GameManager.onStatTracking += getHareGeneration;
        GameManager.onStatTracking += getFoxMalesAlive;
        GameManager.onStatTracking += getHareMalesAlive;
        GameManager.onStatTracking += getFoxFemalesAlive;
        GameManager.onStatTracking += getHareFemalesAlive;
        GameManager.onStatTracking += getHaresAlive;
        GameManager.onStatTracking += getFoxesAlive;
        GameManager.onStatTracking += getHaresKilled;
        GameManager.onStatTracking += getHaresKilled;
        GameManager.onStatTracking += getFoxesStarved;
        GameManager.onStatTracking += getHaresStarved;
        GameManager.onStatTracking += getAnimalsAlive;


    }

    private void OnDisable()
    {
        GameManager.onStatTracking -= getAverageFoxSpeed;
        GameManager.onStatTracking -= getAverageHareSpeed;
        GameManager.onStatTracking -= getAverageFoxSight;
        GameManager.onStatTracking -= getAverageHareSight;
        GameManager.onStatTracking -= getFoxGeneration;
        GameManager.onStatTracking -= getHareGeneration;
        GameManager.onStatTracking -= getFoxMalesAlive;
        GameManager.onStatTracking -= getHareMalesAlive;
        GameManager.onStatTracking -= getFoxFemalesAlive;
        GameManager.onStatTracking -= getHareFemalesAlive;
        GameManager.onStatTracking -= getHaresAlive;
        GameManager.onStatTracking -= getFoxesAlive;
        GameManager.onStatTracking -= getHaresKilled;
        GameManager.onStatTracking -= getHaresKilled;
        GameManager.onStatTracking -= getFoxesStarved;
        GameManager.onStatTracking -= getHaresStarved;
        GameManager.onStatTracking -= getAnimalsAlive;

    }

    private void getAverageFoxSpeed() 
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("FoxSpeed"));
        drawPoint(GameManager.averageFoxSpeed, line);
    }
    private void getAverageHareSpeed()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("HareSpeed"));
        drawPoint(GameManager.averageHareSpeed, line);
    }

    private void getAverageFoxSight()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("FoxSight"));
        drawPoint(GameManager.averageFoxSight, line);
    }

    private void getAverageHareSight()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("HareSight"));
        drawPoint(GameManager.averageHareSight, line);
    }

    private void getFoxGeneration()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("FoxGeneration"));
        drawPoint(GameManager.foxGeneration, line);
    }

    private void getHareGeneration()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("HareGeneration"));
        drawPoint(GameManager.hareGeneration, line);
    }

    private void getFoxMalesAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("FoxMalesAlive"));
        drawPoint(GameManager.foxMalesAlive, line);
    }

    private void getHareMalesAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("HareMalesAlive"));
        drawPoint(GameManager.hareMalesAlive, line);
    }

    private void getFoxFemalesAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("FoxFemalesAlive"));
        drawPoint(GameManager.foxFemalesAlive, line);
    }

    private void getHareFemalesAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("HareFemalesAlive"));
        drawPoint(GameManager.hareFemalesAlive, line);
    }

    private void getFoxesAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("foxesAlive"));
        drawPoint(GameManager.foxesAlive, line);
    }

    private void getHaresAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("haresAlive"));
        drawPoint(GameManager.haresAlive, line);
    }

    private void getFoxesStarved()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("FoxesStarved"));
        drawPoint(GameManager.foxesStarved, line);
    }

    private void getHaresStarved()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("HaresStarved"));
        drawPoint(GameManager.haresStarved, line);
    }

    private void getHaresKilled()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("HaresKilled"));
        drawPoint(GameManager.haresKilled, line);
    }
    

    private void getAnimalsAlive()
    {
        UILineRenderer line = uiLineRenderer.Find(renderer => renderer.diagramName.Equals("animalsAlive"));
        drawPoint(GameManager.animalsAlive, line);
    }
    
    //Jons alter Code, für den ich einheitliche neue Methoden geschrieben haben ~Vinzent
    //add macht mehr sind als get, weil man die Punkte dem Renderer hinzufügt, falls jemand lust hat das alles umzuschreiben - Jon
    private void drawPoint(float value, UILineRenderer lineRenderer)
    {
        if(lineRenderer == null)
        {
            throw new System.Exception("LineRenderer not Found");
        }
        lineRenderer.points.Add(new Vector2(gameManager.currentStatsTrackingIntervall -1, value));
        lineRenderer.updateScale();
    }

}

