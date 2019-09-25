using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regulador : MonoBehaviour
{
    public int direccion;
    public float rotar = 3f;
    public float speed = 3f;
    public GameObject hero;
    public GameObject citizen;
    public GameObject zombie;
    public static float radioVision = 5f;
    Comportamientos cz;

    public IEnumerator Wait()
    {
        while (true)
        {
            //en esta corrutina se utiliza un while para verificar si mueve o deja quieto a los NPC
            int estado = Random.Range(0, 2);
            switch (estado)
            {
                case 0:
                    cz = Comportamientos.Idle;
                    break;
                case 1:
                    cz = Comportamientos.Moving;
                    direccion = Random.Range(0, 4);
                    break;
            }
            yield return new WaitForSeconds(3f);
        }

    }
    public void ComportarseNormal()
    {
        if (cz == Comportamientos.Moving)
        {
            switch (direccion)
            {
                case 0:
                    transform.position += transform.forward * speed * Time.deltaTime;
                    break;
                case 1:
                    transform.position -= transform.forward * speed * Time.deltaTime;
                    break;
                case 2:
                    transform.position += transform.right * speed * Time.deltaTime;
                    break;
                case 3:
                    transform.position -= transform.right * speed * Time.deltaTime;
                    break;
            }

        }
        else if (cz == Comportamientos.Rotating)
        {

            transform.Rotate(transform.up * rotar * Time.deltaTime);
        }
    }
}
