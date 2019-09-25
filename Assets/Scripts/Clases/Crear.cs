using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPC.Enemy;
using NPC.Ally;

public class Crear : MonoBehaviour
{

    public static string imprimirMensaje;
    public Text mensajeUI;
    public Text ZombieT; //textos que se mostraran en la UI
    public Text CitiT;
    public static int citizenNum= 0;
    public static int zombieNum= 0;
    readonly int quantityCubo;
    public static bool alive= true; //variable para verificar que el heroe está vivo
    public static GameObject gameOver;
    public Crear()
    {
        //se declara el valor random de los cubos
        System.Random rnd = new System.Random();
        quantityCubo = rnd.Next(5, 16);
    }
    public static float heroSpeed;
    public void Awake()
    {
        gameOver= GameObject.Find("imagen");
        gameOver.SetActive(false);
        heroSpeed = Random.Range(0.1f, 0.5f);//se declara el valor random de la velocidad del heroe
        for (int i = 0; i < quantityCubo; i++) //este for crea los cubos y mediante un if se decide crear entre el heroe, zombies y ciudadanos llamando a sus clases
        {
            int s = Random.Range(1, 3);
            GameObject Mobs = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Mobs.AddComponent<Rigidbody>();
            Vector3 v = new Vector3();
            v.x = Random.Range(5, 30);
            v.z = Random.Range(5, 30);
            Mobs.transform.position = v;
            if (i == 0)
            {
                Mobs.AddComponent(typeof(Hero));
            }

            else
            {
                switch (s)
                {
                    case 1:
                        Mobs.AddComponent(typeof(Zombie));
                        break;
                    case 2:
                        Mobs.AddComponent(typeof(Citizen));
                        break;
                }
            }
        }
        foreach (Zombie zombie in Transform.FindObjectsOfType<Zombie>())
        {
            zombieNum = zombieNum + 1;
        }
        foreach (Citizen ciudadanos in Transform.FindObjectsOfType<Citizen>())
        {
            citizenNum = citizenNum + 1;
        }

    }
    void Update()
    {
        ZombieT.text = "Zombies: " + zombieNum;
        CitiT.text = "Ciudadanos:" + citizenNum;
        mensajeUI.text = imprimirMensaje;
    }
}
    

