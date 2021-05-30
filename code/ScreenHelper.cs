using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenHelper : MonoBehaviour
{


    static Texture2D whiteTexture;

    public static Texture2D WhiteTexture
    {
        get
        {
            if (whiteTexture == null)
            {
                whiteTexture = new Texture2D(1,1);
                whiteTexture.SetPixel(0, 0, Color.white);
                whiteTexture.Apply();
            }

            return whiteTexture;
        }
    }

    public static void DrawScreenReact(Rect rect, Color color)
    {
        
        GUI.color = color;
        GUI.DrawTexture(rect, WhiteTexture);
    }

    public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        //horni okraj
        DrawScreenReact(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        //levy roh
        DrawScreenReact(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        //pravy roh
        DrawScreenReact(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        //dolni roh
        DrawScreenReact(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }

    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;

        Vector3 topLeft = Vector3.Min(screenPosition1, screenPosition2);
        Vector3 bottomRight = Vector3.Max(screenPosition1, screenPosition2);


        // vytvori obdelnik
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);

    }

    public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2)
    {
        Vector3 v1 = camera.ScreenToViewportPoint(screenPosition1);
        Vector3 v2 = camera.ScreenToViewportPoint(screenPosition2);
        Vector3 min = Vector3.Min(v1, v2);
        Vector3 max = Vector3.Max(v1, v2);

        min.z = camera.nearClipPlane;
        max.z = camera.farClipPlane;

        Bounds bounds = new Bounds();

        bounds.SetMinMax(min, max);
        return bounds;
    }












    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}
