using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraH : MonoBehaviour
{
    // se declararon las variables y el bool a usar ademas de un float para la sensibilidad del movimiento de la camara
    public float MouseX;
    float MouseY;
    float sensibilidad = 3.5f;
    float limitarvista;
    public bool invertedMouse;
    void Update()
    {// aca se registra el movimiento del mouse en x & y
        if (Crear.alive == true)
        {
            float rotarx = Input.GetAxisRaw("Mouse X");
            MouseX += rotarx * sensibilidad;

            float rotary = Input.GetAxisRaw("Mouse Y");
            float campo = rotary * sensibilidad;

            Vector3 mousePosition = Input.mousePosition;
            MouseX += Input.GetAxis("Mouse X");
            if (invertedMouse)
            {
                limitarvista += campo;
            }
            else
            {
                limitarvista -= campo;
            }
            transform.eulerAngles = new Vector3(limitarvista, MouseX, 0);
        }
    }
}
