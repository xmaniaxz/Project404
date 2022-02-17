using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
    public GameObject Screen;
    public GameObject Player;

    public void FixedUpdate()
    {
        RotateScreen();
    }

    public void RotateScreen()
    {
        Vector3 targetPostition = new Vector3(Player.transform.position.x,
                                        Screen.transform.position.y,
                                        Player.transform.position.z);
        Screen.transform.LookAt(targetPostition);
    }
}
