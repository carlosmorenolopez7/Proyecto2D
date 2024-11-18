using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // Referencia al transform del jugador
    //necesito que la camara cambie de distancia dependiendo de la caida del jugador
    public float fallThreshold = -10f;
    public float cameraDistance = 10f;
    public float cameraDistanceFall = 20f;
    
    void Update()
    {
        if (player.transform.position.y < fallThreshold)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -cameraDistanceFall);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -cameraDistance);
        }
    }

    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -cameraDistance);
    }

    public void SetCameraDistance(float distance)
    {
        cameraDistance = distance;
    }

    public void SetCameraDistanceFall(float distance)
    {
        cameraDistanceFall = distance;
    }

    public void SetFallThreshold(float threshold)
    {
        fallThreshold = threshold;
    }

    public float GetCameraDistance()
    {
        return cameraDistance;
    }

    public float GetCameraDistanceFall()
    {
        return cameraDistanceFall;
    }

    
}
