using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Ally;
using UnityEngine.UI;

namespace NPC
{
    namespace Enemy
    {
        public class Zombie : Regulador
        {
            //aca se declaran las variables a usar y se  almacena los datos a mostrar para que no se cambien
            public DatosZombie dato;
            public static string gustito;
            Vector3 direccionC;
            Vector3 direccionH;
            float distanciaTemp;
            public static float distanciaTempH;
            GameObject Ali;
            float rangoVision;
            public static string mensaje;

            void Start()
            {// gustito llama la funcion mensaje, tambien se le aplica el tag al gameobject para que se pueda reconocer
                gustito = Mensaje();
                dato.edad = Random.Range(15, 101);
                rotar = Random.Range(35, 95);
                dato.gusto = Random.Range(0, 5); //selecciona el gusto del zombie al azar
                gameObject.transform.tag = "ZombieNya";

                hero = GameObject.FindGameObjectWithTag("HeroeNya");

                if (dato.edad > 15)
                    speed = (float)(15 * 3) / dato.edad;

                int color = Random.Range(1, 4);
                switch (color) //este switch se usa para escoger de forma random el color del zombie
                {
                    case 1:
                        this.GetComponent<Renderer>().material.color = Color.cyan;
                        break;
                    case 2:
                        this.GetComponent<Renderer>().material.color = Color.green;
                        break;
                    case 3:
                        this.GetComponent<Renderer>().material.color = Color.magenta;
                        break;
                }
                mensaje = Mensaje();
                StartCoroutine(Wait());
            }
            
            
            public string Mensaje()
            {//esta es la funcion que se encarga de imprimir el mensaje del gusto del zombie
                dato.gz = (GustoZ)dato.gusto;
                return "RAWWRRRR!! quiero comer " + dato.gz;
            }
            public void Update()
            {
                if (Crear.alive == true)
                {
                    distanciaTempH = Vector3.Distance(hero.transform.position, transform.position);
                    GameObject closerCitizen= null;
                    foreach(Citizen ciudadano in Transform.FindObjectsOfType<Citizen>())
                    {
                        distanciaTemp = Vector3.Distance(ciudadano.transform.position, transform.position);// Distancia del Zombi al Aldeano mas sercano

                        if (distanciaTemp < rangoVision)
                        {
                            rangoVision = distanciaTemp;
                            closerCitizen = ciudadano.gameObject; //remplasa el null por el Aldeano mas sercano

                        }

                    }
                    if (closerCitizen != null)
                    {
                        direccionC = Vector3.Normalize(closerCitizen.transform.position - transform.position);
                        transform.position += direccionC * 0.1f;
                    }
                    else if (distanciaTempH <= rangoVision)
                    {
                        gustito = Mensaje();
                        direccionH = Vector3.Normalize(hero.transform.position - transform.position);
                        transform.position += direccionH * 0.1f;
                    }
                    else
                    {
                        rangoVision = 5f;
                        ComportarseNormal();
                    }
                }
                 
            }
            private void OnCollisionEnter(Collision collision)
            {
                if (collision.transform.tag == "HeroeNya")
                {
                    Crear.gameOver.SetActive(true);
                    Crear.alive = false;
                }
            }
        }

        public struct DatosZombie
        {//en este struct se almacenan los datos de los gustos y el comportamiento del zombie
            public GustoZ gz;
            public Comportamientos cz;
            public int gusto;
            public int edad;

        }

        public enum GustoZ
        {//en este enum se encuentra los gustos de los zombies
            Cerebro,
            Dedos,
            Cuello,
            Muslo,
            Orejas,
        }

    }
}
public enum Comportamientos
{ Idle, Moving, Rotating }//en este enum se encuentra los comportamientos que puede tomar los NPC. Idle para quedarse quieto, moving para moverse y rotating para rotar
