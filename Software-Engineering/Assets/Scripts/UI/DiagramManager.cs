using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiagramManager : MonoBehaviour
{
    [SerializeField] List<GameObject> diagrams;
    [SerializeField] GameObject y_Axis;
    [SerializeField] GameObject x_Axis;
    private int activeDiagramIndex = 0; // muss default m‰ﬂig dass diagram sein, was als erstes an ist

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            activeDiagramIndex++;
            setDiagram(true);
            setAxis(diagrams[activeDiagramIndex].GetComponentInChildren<UILineRenderer>());

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            activeDiagramIndex--;
            setDiagram(false);
            setAxis(diagrams[activeDiagramIndex].GetComponentInChildren<UILineRenderer>());

        }
    }

    private void setDiagram(bool direction)
    {
        int maxIndex = diagrams.Count - 1;
        if(activeDiagramIndex > maxIndex)
        {
            activeDiagramIndex = 0;
            diagrams[maxIndex].SetActive(false);
            diagrams[activeDiagramIndex].SetActive(true);
        }
        else if(activeDiagramIndex < 0)
        {
            activeDiagramIndex = maxIndex;

            diagrams[0].SetActive(false);
            diagrams[maxIndex].SetActive(true);
        }
        else
        {
            if (direction)
            {
                diagrams[activeDiagramIndex - 1].SetActive(false);
                diagrams[activeDiagramIndex].SetActive(true);
            }
            else
            {
                diagrams[activeDiagramIndex + 1].SetActive(false);
                diagrams[activeDiagramIndex].SetActive(true);
            }
           
        }
    }

    public void setAxis(UILineRenderer lineRenderer)
    {
        if (lineRenderer.transform.parent.gameObject.activeSelf)
        {
            int[] numbers = new int[] { lineRenderer.min, lineRenderer.mid, lineRenderer.max };
            for (int i = 0; i < y_Axis.transform.childCount; i++)
            {
                y_Axis.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = numbers[i].ToString();
            }
        }
    }

        
}
