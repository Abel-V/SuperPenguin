using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int NUM_MAX_LIVES = 3;

    private static int points = 0; //static: reserva un espacio en memoria 
    private static int level = 1;
    private static int lives = NUM_MAX_LIVES;

    public static int Points { get => points; set => points = value; }
    public static int Level { get => level; set => level = value; }
    public static int Lives { get => lives; set => lives = value; }

    public static void AddPoints(int _points)
    {
        points = points + _points;
    }

    public static void AddLife(int _lives)
    {
        if(lives < NUM_MAX_LIVES) {
            lives = lives + _lives;
        }
    }

    public static void SubstractLive()
    {
        lives = lives - 1;
    }

    public static void ResetLivesAndPoints()
    {
        lives = NUM_MAX_LIVES;
        points = 0;
    }
}
