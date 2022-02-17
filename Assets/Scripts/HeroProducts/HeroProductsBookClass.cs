using DG.Tweening;
using System.Collections;
using UnityEngine;

public class HeroProductsBookClass : MonoBehaviour
{
    public GameObject Positioner;
    public CoverHeader Cover;
    public BackCoverHeader BackCover;
    public CameraHeader Cam;
    private AudioSource Source;

    public void StartCoroutineTween() => StartCoroutine(TweenMovement());

    public void Start()
    {
        Source = GetComponent<AudioSource>();
        if (!Source.clip) Source.enabled = false;
        Cam.Obj = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>().gameObject;
        Cam.Parent = GameObject.FindGameObjectWithTag("Player");
        Cover.SetCoverHeader(Cover.Obj, Cover.Obj.transform.localPosition, Cover.Obj.transform.localEulerAngles, Cover.Obj.transform.localScale);
        BackCover.SetBackCoverHeader(BackCover.Obj, BackCover.Obj.transform.localPosition, BackCover.Obj.transform.localEulerAngles, BackCover.Obj.transform.localScale);
    }

    public IEnumerator TweenMovement()
    {
        //Unlock Camera
        CameraMovement.inMenu = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        float duration = 1f;
        Cam.SetCameraHeader(Cam.Obj, Cam.Parent, Cam.Obj.transform.localPosition, Cam.Obj.transform.localEulerAngles, Cam.Obj.transform.localScale);
        Cam.Obj.transform.DOMove(Positioner.transform.position, duration);
        Cam.Obj.transform.DORotate(Positioner.transform.eulerAngles, duration);
        yield return new WaitForSeconds(duration);
        //open book
        #region Whileloop
        float duration2 = 1f;
        var timer = 0f;
        var startpos = Cover.Obj.transform.eulerAngles;
        var endpos = startpos + new Vector3(0, 0, -180f);
        while (timer < duration2)
        {
            timer += Time.deltaTime;
            Cover.Obj.transform.eulerAngles = Vector3.Lerp(startpos,endpos, timer / duration2);
            yield return null;
        }
        #endregion
        Source.Play();
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Backspace));
        StartCoroutine(TweenMovementReversed(Cam,startpos,endpos));
    }

    public IEnumerator TweenMovementReversed(CameraHeader _Cam,Vector3 start,Vector3 end)
    {
        Source.Pause();
        float duration = 1f;
        _Cam.Obj.transform.DOLocalMove(Cam.Position, duration);
        _Cam.Obj.transform.DOLocalRotate(Cam.Rotation, duration);
        yield return new WaitForSeconds(0.3f);
        //close book
        #region Whileloop
        float duration2 = 0.6f;
        var timer = 0f;
        while (timer < duration2)
        {
            timer += Time.deltaTime;
            Cover.Obj.transform.eulerAngles = Vector3.Lerp(end, start, timer / duration2);
            yield return null;
        }
        #endregion
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CameraMovement.inMenu = false;
        yield return null;
    }
}

[System.Serializable]
public class CoverHeader
{
    public GameObject Obj;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;

    public void SetCoverHeader(GameObject _obj, Vector3 _pos, Vector3 _rot, Vector3 _scale)
    {
        Obj = _obj;
        Position = _pos;
        Rotation = _rot;
        Scale = _scale;
    }
}

[System.Serializable]
public class BackCoverHeader
{
    public GameObject Obj;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;

    public void SetBackCoverHeader(GameObject _obj, Vector3 _pos, Vector3 _rot, Vector3 _scale)
    {
        Obj = _obj;
        Position = _pos;
        Rotation = _rot;
        Scale = _scale;
    }
}

[System.Serializable]
public class CameraHeader
{
    public GameObject Obj;
    public GameObject Parent;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;

    public void SetCameraHeader(GameObject _obj, GameObject _parent, Vector3 _pos, Vector3 _rot, Vector3 _scale)
    {
        Obj = _obj;
        Parent = _parent;
        Position = _pos;
        Rotation = _rot;
        Scale = _scale;
    }
}