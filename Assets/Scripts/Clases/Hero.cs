using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Ally;
using NPC.Enemy;

public class Hero : MonoBehaviour
{
    float distanTemp;
    private void Awake()
    {
        gameObject.transform.tag = "HeroeNya"; //aca se le da el tag al gameobject del heroe

    }
    void Start()
    {
        Zombie.mensaje = Zombie.gustito;
        gameObject.AddComponent(typeof(Movement));
        GameObject Cabeza = new GameObject();
        Cabeza.AddComponent(typeof(Camera)); //este añade la camara
        Cabeza.AddComponent(typeof(CamaraH));//este añade el codigo de camara

        gameObject.GetComponent<Movement>().rotar = Cabeza.GetComponent<CamaraH>(); //aca se pone como hijo del heroe a la camara
        Cabeza.transform.SetParent(gameObject.transform);
        Cabeza.transform.localPosition = new Vector3(0, 0.5f, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "AldeanoNya")
            Debug.Log(collision.transform.GetComponent<Citizen>().meLlamo);
    }
    public void Update()
    {
        GameObject referenciaZ = null;
        foreach (Zombie zombie in Transform.FindObjectsOfType<Zombie>())
        {
             distanTemp = Vector3.Distance(zombie.transform.position, transform.position);
            if (distanTemp <= Regulador.radioVision)
                referenciaZ = zombie.gameObject;
        }
        if (referenciaZ != null)
            Crear.imprimirMensaje = Zombie.gustito;
        else
            Crear.imprimirMensaje = "";
    }
}