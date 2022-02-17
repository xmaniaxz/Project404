using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NavGroup : MonoBehaviour
{
    [SerializeField] private Color SelectionColor = new Color32(0, 255, 0, 255);
    [SerializeField] private Color DeselectColor = new Color32(255, 138, 0, 255);
    [SerializeField] private GameObject NavLogoBig;
    [SerializeField] private GameObject NavLogoSmall;
    [SerializeField] private GameObject NavText;
    [SerializeField] private Vector3 TeleportLocation;
    [SerializeField] private Vector3 TeleportRotation;

    public void TeleportToLocation(GameObject _Player)
    {
        _Player.transform.position = TeleportLocation;
        _Player.transform.eulerAngles = TeleportRotation;
        //_Player.GetComponentInChildren<Camera>().transform.eulerAngles = Vector3.zero;
    }

    public void Select()
    {
        if (NavLogoBig != null && NavLogoSmall != null && NavText != null)
        {
            if (NavLogoBig.GetComponent<Image>())
                NavLogoBig.GetComponent<Image>().color = SelectionColor;
            if (NavLogoBig.GetComponent<TMP_Text>())
            {
                NavLogoBig.GetComponent<TMP_Text>().color = SelectionColor;
            }
            NavLogoSmall.GetComponent<Image>().color = SelectionColor;
            NavText.GetComponent<TMP_Text>().color = SelectionColor;
        }
    }

    public void DeSelect()
    {
        if (NavLogoBig != null && NavLogoSmall != null && NavText != null)
        {
            if (NavLogoBig.GetComponent<Image>())
                NavLogoBig.GetComponent<Image>().color = Color.black;
            if (NavLogoBig.GetComponent<TMP_Text>())
            {
                NavLogoBig.GetComponent<TMP_Text>().color = Color.black;
            }
            NavLogoSmall.GetComponent<Image>().color = Color.black;
            NavText.GetComponent<TMP_Text>().color = DeselectColor;
        }
    }
}