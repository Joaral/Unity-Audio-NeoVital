using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    private Vector2 areaCenter;
    private Vector2 targetPosition;
    private Vector2 currentPosition;

    public float rightMax;
    public float leftMax;
    public float upMax;
    public float downMax;

    public float speed = 5f;
    public float alpha = 7.0f;

    public bool isTransitioning = false; // Avisa si esta siguiendo al neuvo trigger.
    public float timeTransition = 0f;

    void Awake()
    {
        if (target != null)
        {
            currentPosition.x = target.transform.position.x;
            currentPosition.y = target.transform.position.y;
            transform.position = new Vector3(currentPosition.x, currentPosition.y, -1);
        }
    }

    void Move_Cam()
    {
        if (isTransitioning)
        {
            timeTransition -= Time.unscaledDeltaTime;
            if (timeTransition <= 0f)
            {
                // Una vez que el juego se reanuda, ya no necesitamos esta bandera
                isTransitioning = false;
                Time.timeScale = 1f;  // Reanudar el juego inmediatamente después de actualizar los límites
                timeTransition = 0f;
            }
        }

        if (target)
        {
            targetPosition.x = target.transform.position.x;
            targetPosition.y = target.transform.position.y;
            //targetPosition.y = areaCenter.y; // Mantener Y en el centro del área

            // Restringimos X e Y dentro de los nuevos límites
            float clampedX = Mathf.Clamp(targetPosition.x, leftMax, rightMax);
            float clampedY = Mathf.Clamp(targetPosition.y, downMax, upMax);
            currentPosition.x = clampedX;
            currentPosition.y = clampedY;

            // Movemos la cámara hacia la posición del jugador sin transiciones forzadas
            transform.position = Vector3.Lerp(transform.position, new Vector3(currentPosition.x, currentPosition.y, -1), speed * Time.unscaledDeltaTime);
        }
    }

    public void SetNewArea(Vector2 center, float newLeftMax, float newRightMax, float newUpMax, float newDownMax)
    {
        isTransitioning = true; // Indica que estamos cambiando de área
        leftMax = newLeftMax;
        rightMax = newRightMax;
        upMax = newUpMax;
        downMax = newDownMax;
        areaCenter = center;

        Time.timeScale = 0f; // Pausar el juego durante el cambio de trigger
        timeTransition = 0.5f;

        Debug.Log("Nueva Área asignada: " + areaCenter.x + ", " + areaCenter.y);
    }

    void Update()
    {
        Move_Cam();
    }
}