using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraController : MonoBehaviour
{
    // rychlost pohybu kamery
    public float speed = 10f;
    // rychlost prblizeni
    public float scrollspeed = 1f;
    // rycholst rotace
    public float rotspeed = 60f;
    //hraniceobrazovky
    public float SCborder = 10f;
    // limit kam se kamera muze pohybovat
    public Vector2 screenLimit;

    public float team;


   


    float rotY = 0f;
   public float rotX = 30f;

    private void Start()
    {
        foreach (var item in FindObjectsOfType<Unit>())
        {
            if (team == item.Team)
            {
                item.GetComponentInChildren<HPbar>().enabled = true;
                
                
            }
        }

        

    }

    void Update()
    {

        
        // pozice kamery
        Vector3 pos = transform.position;
        

        // pohyb kamery pri stisku tlacitek wsad
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - SCborder)
        {
            pos.z += speed * Time.deltaTime;
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= SCborder)
        {
            pos.z -= speed * Time.deltaTime;
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - SCborder)
        {
            pos.x += speed * Time.deltaTime;
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= SCborder)
        {
            pos.x -= speed * Time.deltaTime;
        }

        
        //priblizeni oddaleni
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y += scroll * scrollspeed * 150f * Time.deltaTime;

        //maximalni pohyb kamery
        pos.x = Mathf.Clamp(pos.x, 6, screenLimit.x);
        pos.z = Mathf.Clamp(pos.z, 0,200);
        pos.y = Mathf.Clamp(pos.y, 15, 25);

        // meni polohu kamery v zavislosti na stisknuti tlacitek
        transform.position = pos;
        transform.eulerAngles = new Vector3(rotX, rotY, 0f);
       
    }
   
}
