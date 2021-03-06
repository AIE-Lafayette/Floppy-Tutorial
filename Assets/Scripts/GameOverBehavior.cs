using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverBehavior : MonoBehaviour
{
    //A reference to a Text object
    public Text GameOverText;

    // OnCollisionEnter is called when the object's collider makes contact with another collider
    void OnCollisionEnter(Collision collision)
    {
        //Display the text "Game Over!"
        GameOverText.text = "Game Over!";
    }
}
