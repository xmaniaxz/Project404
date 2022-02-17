using System.Collections;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    public GameObject DoorLeft;
    public Vector3 DoorLeftRotation;
    public GameObject DoorRight;
    public Vector3 DoorRightRotation;
    public float Duration;
    [Range(0f, 20f)] public float MaxDistance = 10f;

    private bool isOpen;
    private GameObject Player;
    private Vector3 StartPosLeft;
    private Vector3 StartPosRight;
    private Vector3 EndPosLeft;
    private Vector3 EndPosRight;

    public void Start()
    {
        StartPosLeft = DoorLeft.transform.eulerAngles;
        StartPosRight = DoorRight.transform.eulerAngles;
        EndPosLeft = DoorLeft.transform.eulerAngles + DoorLeftRotation;
        EndPosRight = DoorRight.transform.eulerAngles + DoorRightRotation;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        if (GetPlayerDistance() < MaxDistance && !isOpen)
        {
            isOpen = true;
            StartCoroutine(RotateDoor(DoorLeft,StartPosLeft,EndPosLeft));
            StartCoroutine(RotateDoor(DoorRight, StartPosRight, EndPosRight));
        }
        else if (GetPlayerDistance() > MaxDistance && isOpen)
        {
            isOpen = false;
            StartCoroutine(RotateDoor(DoorLeft, EndPosLeft, StartPosLeft));
            StartCoroutine(RotateDoor(DoorRight, EndPosRight, StartPosRight));
        }
    }

    public float GetPlayerDistance() { return Vector3.Distance(DoorLeft.transform.position, Player.transform.position);}

    public IEnumerator RotateDoor(GameObject _Door, Vector3 _StartPos, Vector3 _EndPos)
    {
        float time = 0;
        while (time <= Duration)
        {
            _Door.transform.eulerAngles = Vector3.Lerp(_StartPos, _EndPos, time / Duration);
            time += Time.fixedDeltaTime;
            yield return null;
        }
    }
}