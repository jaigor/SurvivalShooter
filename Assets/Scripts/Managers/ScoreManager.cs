using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;	// affecting to the type Class

    Text text;

    void Awake ()
    {
        text = GetComponent <Text> ();	// modified the component itself of the class
        score = 0;
    }


    void Update ()
    {
        text.text = "Score: " + score;
    }
}
