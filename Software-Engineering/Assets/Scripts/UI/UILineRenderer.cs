using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : Graphic
{
    //damit der StatsManager das unterscheiden kann
    public string diagramName;

    public DiagramManager diagramManager;
    public Vector2Int gridSize;
    public List<Vector2> points;

    //axis
    public int scale = 1;
    public int min = 1;
    public int mid = 5;
    public int max = 10;

    [SerializeField] float thickness = 10f;

    float width;
    float height;
    float unitWidth;
    float unitHeight;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        unitWidth = width / (float)gridSize.x;
        unitHeight = height / (float)gridSize.y;

        if(points.Count < 2)
        {
            return;
        }

        float angle = 0;


        for(int i = 0; i < points.Count; i++)
        {
            Vector2 point = points[i];
            if (i < points.Count - 1)
            {
                angle = getAngle(points[i], points[i + 1]) + 45f;
            }

            drawVerticesForPoint(point, vh, angle);
        }

        for(int i = 0; i<points.Count-1; i++)
        {
            int index = i * 2;
            vh.AddTriangle(index + 0, index + 1, index + 3);
            vh.AddTriangle(index + 3, index + 2, index + 0);
        }
    }
    private void drawVerticesForPoint(Vector2 point, VertexHelper vh, float angle)
    {
        point.y /= scale; // set point corresponding to scale

        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = Quaternion.Euler(0,0,angle) * new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x , unitHeight * point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
        vh.AddVert(vertex);
    }

    private float getAngle(Vector2 me, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - me.y, target.x - me.x) * (180 / Mathf.PI));
    }

    public void updateScale()
    {
        bool shouldUpdateScale = false;

        float newPoint = points[points.Count - 1].y;

        if (newPoint > max)
        {
            scale = Mathf.CeilToInt(newPoint / 10);
            shouldUpdateScale = true;
        }

        if (shouldUpdateScale)
        {
            min = 1 * scale;
            mid = 5 * scale;
            max = 10 * scale;
            Debug.Log("updated Scale, new Scale = " + scale);
            diagramManager.setAxis(this);
        }
    }
} 