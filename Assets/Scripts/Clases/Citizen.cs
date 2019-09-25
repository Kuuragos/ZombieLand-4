using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Enemy;

namespace NPC
{
    namespace Ally
    {
        public class Citizen : Regulador
        {//aca se declaran las variables a usar y se almacena los datos a mostrar para que no se cambien
            public DatosCitizen datoC;
            public string meLlamo;
            float distanciaTemp;
            float rangoVision = 5f;
            Vector3 direccionZombie;
            void Start()
            {
                rotar = Random.Range(35, 95);
                gameObject.transform.tag = "AldeanoNya";
                meLlamo = eligeName();// meLlamo llama la funcion mensaje, tambien se le aplica el tag al gameobject para que se pueda reconcer
                if (datoC.age > 15)
                    speed = (float)(15 * 3) / datoC.age;
                StartCoroutine(Wait());
            }
            public string eligeName()
            {//esta es la funcion que se encarga de imprimir el mensaje del nombre y la edad del ciudadano
                datoC.age = Random.Range(15, 101);
                int randomN = Random.Range(0, 20);
                datoC.NombresCiti = (Names)randomN;
                return "Hola, me llamo " + datoC.NombresCiti + "y tengo " + datoC.age + "años";
            }
            public void Update()
            {
                GameObject zombieScene = null; // GameObject que almacena a todos los Zombis en la esena
                foreach (Zombie zombie in Transform.FindObjectsOfType<Zombie>())
                {

                    distanciaTemp = Vector3.Distance(zombie.transform.position, transform.position);
                    if (distanciaTemp < rangoVision)
                    {
                        rangoVision = distanciaTemp;
                        zombieScene = zombie.gameObject; //remplasa el null por el Zombi mas sercano
                    }
                }
                //hace que los ciudadanos corran de los Zombis mas cercanos 
                if (zombieScene != null)
                {
                    direccionZombie = Vector3.Normalize(zombieScene.transform.position - transform.position);
                    transform.position += direccionZombie * -0.1f;
                }
                else
                {
                    rangoVision = 5f;
                    ComportarseNormal();
                }
            }
            private void OnCollisionEnter(Collision collision)
            {

                if (collision.transform.tag == "ZombieNya")
                {
                    Zombie transform = gameObject.AddComponent<Zombie>();
                    transform.dato = (DatosZombie)gameObject.GetComponent<Citizen>().datoC;
                    Destroy(gameObject.GetComponent<Citizen>());
                    Crear.zombieNum++;
                    Crear.citizenNum--;

                }
            }
        }

        public struct DatosCitizen
        {//en este struct se almacenan los datos de los nombres y la edad del ciudadano
            public Names NombresCiti;
            public int age;
            public Comportamientos cz;

            static public explicit operator DatosZombie(DatosCitizen cambio)
            {
                DatosZombie z = new DatosZombie();
                z.edad = cambio.age;
                return z;
            }
        }
        //en este enum se encuentra los nombres de los ciudadanos
        public enum Names { Aydan, Chindler, Tann, Erock, Aerav, Daviron, Leviye, Tobis, Patrock, Abbrahan, Alaysia, Reegan, Catlea, Emiliye, Emilyse, Charleagh, Claissa, Belenne, Aebby, Allany }
    }
}