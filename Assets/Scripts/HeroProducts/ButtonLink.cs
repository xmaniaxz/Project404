using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLink : MonoBehaviour
{
    public void OpenLink(string Link)
    {
        Application.OpenURL(Link);
    }
}
