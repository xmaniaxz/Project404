using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;
public enum ButtonInteraction
{
    None,
    Pause,
    Play,
}

[RequireComponent(typeof(AudioSource))]
public class CustomVideoPlayer : MonoBehaviour
{
    [Header("Settings")]   
    public float TimeToWait = 1;
    public Sprite sprite;
    [Tooltip("Don't forget the extension")] public string VideoAsssetName;


    [Header("Assignments")]
    public Image image;
    public TMP_Text timer;    
    private string StreamingAssetPath;  
    private AudioSource Audio;
    private VideoPlayer Video;
    private MeshRenderer MR;

    public void Start()
    {
        //Set private variables to reference
        Audio = GetComponent<AudioSource>();
        Video = GetComponent<VideoPlayer>();
        MR = GetComponent<MeshRenderer>();
        //Set path to Video to play
        if(VideoAsssetName != null)
        StreamingAssetPath = System.IO.Path.Combine(Application.streamingAssetsPath, VideoAsssetName);
        //Set Video as playable
        Video.url = StreamingAssetPath;
        //Set Desired Image for show
        if(sprite)
        image.sprite = sprite;
    }

    public void Update()
    {
        if (timer) 
        timer.text = Timer(Audio.time);
    }

    private string Timer(float time)
    {
        int Seconds = 0;
        int Minutes = 0;
        int Hours = 0;
        Hours = Mathf.RoundToInt(time / 3600f);
        Seconds = Mathf.RoundToInt(time - time / 3600f - time / 60f);
        Minutes = Mathf.RoundToInt(time / 60f - time / 3600f);
        //Tidying up text with a double 00;
        string stringsec = Seconds.ToString();
        string stringmin = Minutes.ToString();
        string stringhour = Hours.ToString();
        if (Seconds < 10)
        {
            stringsec = string.Concat("0", Seconds);
        }
        if (Minutes < 10)
        {
            stringmin = string.Concat("0", Minutes);
        }
        if (Hours < 10)
        {
           stringhour =string.Concat("0", Seconds);
        }

        return string.Concat(stringhour, ":", stringmin, ":", stringsec);
    }

    public void PlayFunction()
    {
        MR.enabled = true;
        Audio.Play();
        Video.Play();
        if(image)
        image.gameObject.SetActive(true);
    }
    public void PauseFunction()
    {
        MR.enabled = false;
        Audio.Pause();
        Video.Pause();
        if(image)
        image.gameObject.SetActive(false);
    }
}