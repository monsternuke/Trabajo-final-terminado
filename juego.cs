using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class juego : MonoBehaviour
{
    public GameObject Quad;//Verificador de color en x ó y
    public GameObject AvisoDeWin;//Aviso de ganador
    private bool primerturno;//booleano 
    int ancho = 10;
    int alto = 10;//Tamaño de la cuadricula
    public GameObject pieza;//pieza que conforma el tablero
    private GameObject[,] sphe;//objeto sphere
    public Color colorfondo;
    public Color colorJugaUno;
    
    public Color colorJugaDos;
//Variables visibles en el inspector que se encargan de los colores del Jugador1, Jugador2 y el color de las esferas
    public void Start()
    {
        sphe = new GameObject[ancho, alto];
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                GameObject sphere = GameObject.Instantiate(pieza) as GameObject;
                Vector3 position = new Vector3(i, j, 0);
                sphere.transform.position = position;                                    //utilizan la variable publica sphe para generar las bolas con su respectivo tamaño

                sphere.GetComponent<Renderer>().material.color = colorfondo;

                sphe[i, j] = sphere;
            }
        }
    }

    public void Update()
    {
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Declaración para convertir el mouse position en un punto en el espacio cerca de las piezas del tablero
        SelecSphere(mPosition);

    }

    public void SelecSphere(Vector3 position)
    {
        int i = (int)(position.x + 0.5f);//bucle que itera cada posición a lo largo de x
        int j = (int)(position.y + 0.5f);//bucle que itera cada posciÓn a lo largo de y

        if (Input.GetButtonDown("Fire1"))
        {
            if (i >= 0 && j >= 0 && i < ancho && j < alto)
            {
                GameObject sphere = sphe[i, j];
                if (sphere.GetComponent<Renderer>().material.color == colorfondo)
                {
                    Color colorAUsar = Color.clear;
                    if (primerturno)               //Declara el primer turno para el jugador uno y identifica su color
                        colorAUsar = colorJugaUno;

                    else
                        colorAUsar = colorJugaDos;//color que representa al jugador dos en el segundo turno 

                    sphere.GetComponent<Renderer>().material.color = colorAUsar;
                    primerturno = !primerturno;
                    VerificadorX(i, j, colorAUsar);
                    VerificadorY(i, j, colorAUsar);         //Verificador de color de las bolas en x,y,diagonales
                    DiagoPositiva(i, j, colorAUsar);
                    DiagoNegativa(i, j, colorAUsar);
    
                }
            }
        }

        
    }


    public void VerificadorX(int x, int y, Color colorAVerificando)//llama el verificador de x 
    {
        int contador = 0;
        for (int i = x-3; i <= x+3; i++)
        {
            if (i < 0 || i >= ancho)
                continue;

            GameObject sphere = sphe[i, y];

            if (sphere.GetComponent<Renderer>().material.color == colorAVerificando)
            {
                contador++;
                if (contador == 4)//contador de las bolas necesarias para ganar
                {
                    Debug.Log("Ganaste en X"); //Lo que aparece en la consola al haber un ganadar en x
                    AvisoDeWin.SetActive(true); //llama al aviso de que hay un ganador
                    Quad.SetActive(true); 


                }
            }
            else
                contador = 0;
        }
    }

    public void VerificadorY(int x, int y, Color colorAVerificando)    //llama verificador de y 
    {
        int contador = 0;
        for (int j = y - 3; j <= y + 3; j++)
        {
            if (j < 0 || j >= alto)
                continue;

            GameObject sphere = sphe[x, j];

            if (sphere.GetComponent<Renderer>().material.color == colorAVerificando)  //verificador
            {
                contador++;
                if (contador == 4)     //bolas necesarias para ganar
                {
                    Debug.Log("Ganaste en Y");  //aviso que aparece en consola 
                    AvisoDeWin.SetActive(true); //llama a aviso de win al colocar 4 en linea
                    Quad.SetActive(true);

                }
            }
            else
                contador = 0;
        }
    }

     public void DiagoPositiva(int x, int y, Color colorAVerificando) //llama verificador de Diagonales positivas
    {
        int contador = 0;
        int j = y - 3;


        for (int i = x - 3; i <= x + 3; i++)
        {
            if (j < 0 || j >= alto || i < 0 || i >= ancho)
                continue;

                GameObject sphere = sphe[i, j];

                if (sphere.GetComponent<Renderer>().material.color == colorAVerificando)
                {
                    contador++;
                    j++;

                    if (contador == 4)
                    {
                        Debug.Log("digonal +"); //aviso que aparece en consola
                         AvisoDeWin.SetActive(true); //llama a aviso de win al colocar 4 en linea
                         Quad.SetActive(true);
                }
                }
                else
                    contador = 0;
            
        }
    }


    public void DiagoNegativa(int x, int y, Color colorAVerificando) //llama verificador de diagonales negativa
    {
        int contador = 0;
        int j = y + 3;


        for (int i = x - 3; i <= x + 3; i++)
        {
            if (j < 0 || j >= alto || i < 0 || i >= ancho)
                continue;

            GameObject sphere = sphe[i, j];

            if (sphere.GetComponent<Renderer>().material.color == colorAVerificando)
            {
                contador++;
                j--;

                if (contador == 4)
                {
                    Debug.Log("diagonal-"); //aviso que aparece en consola
                    AvisoDeWin.SetActive(true); //llama a aviso de win al colocar 4 en linea
                    Quad.SetActive(true);


                }
            }
            else
                contador = 0;

        }
    }
}