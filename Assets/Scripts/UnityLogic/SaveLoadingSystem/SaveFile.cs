using LitJson;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class SaveFile : MonoBehaviour
{
    [Tooltip("Details from where to get the personalID")]
    public LoginDetails Login;

    public DataTable Data;

    private string JSONLink;
    private JsonData jsonvale;
    private JsonData JsonSaveFile;
    private JsonData JsonLoadFile;

    public void Start()
    {
        StartCoroutine(Getlink());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(SendLink());
        }
    }

    /// <summary>
    /// Gets the Json from website.
    /// </summary>
    /// <returns>PlayerDetails(JSON)</returns>
    public IEnumerator Getlink()
    {
        UnityWebRequest client = UnityWebRequest.Get(Login.URL);
        client.SetRequestHeader("AUTHORIZATION", Login.Authenticate());
        yield return client.SendWebRequest();
        if (client.result == UnityWebRequest.Result.Success)
        {
            JSONLink = client.downloadHandler.text;
            Debug.Log(client.downloadHandler.text);
        }
        else
        {
            Debug.Log("Could not make a connection");            
        }
        if (JSONLink.Length > 0)
        {
            jsonvale = JsonMapper.ToObject(JSONLink);
            Data.isLoggedIn = (bool)jsonvale["loggedIn"];
            if (!Data.isLoggedIn) yield return null;
            Data.PlayerId = int.Parse(jsonvale["employeeID"].ToString());
            Debug.Log(string.Concat(Data.isLoggedIn, " ", Data.PlayerId));
        }
    }

    public IEnumerator SendLink()
    {
        Save();
        WWWForm form = new WWWForm();
        form.AddField("fileUpload", JsonSaveFile.ToString());

        using (UnityWebRequest client = UnityWebRequest.Post("https://jvz.kingdorp.com/project404/login/importsavefiles", form))
        {
            client.SetRequestHeader("AUTHORIZATION", Login.Authenticate());
            yield return client.SendWebRequest();
            if (client.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(client.error);
                yield return null;
            }
            Debug.Log(client.downloadHandler.text);
            Debug.Log("Json upload complete!");
        }
    }

    public void Load(string _Link)
    {
        string JsonString = File.ReadAllText(_Link);
        JsonLoadFile = JsonMapper.ToObject(JsonString);
        int i = 0;
        foreach (Rooms room in JsonLoadFile)
        {          
            Data.RoomArray[i] = room;
            i++;
        }
    }

    public void Save()
    {
        JsonSaveFile = JsonMapper.ToJson(Data);
    }
}

#region SaveData

[System.Serializable]
public class DataTable
{
    public bool isLoggedIn;
    public int PlayerId;
    public DataBaseData Data;
    public Rooms[] RoomArray = new Rooms[4];

    public void ConverData()
    {
        Data.Lobby_Tutorial_Played = false;
        Data.Digitalized_Podcast_Played = RoomArray[0].podcast.PodcastPlayed;
        Data.Digitalized_Radio_Played = false;
        Data.HeroProducts_Podcast_Played = RoomArray[1].podcast.PodcastPlayed;
        Data.HeroProducts_Radio_Played = false;
        Data.Investing_Podcast_Played = RoomArray[2].podcast.PodcastPlayed;
        Data.Investing_Radio_Played = false;
        Data.Customer_Podcast_Played = RoomArray[3].podcast.PodcastPlayed;
        Data.Customer_Radio_Played = false;
        Data.HeroProducts_Seen_Giga = false;
        Data.HeroProducts_Seen_Sport = false;
        Data.HeroProducts_Seen_GO = false;
        Data.HeroProducts_Seen_Analog = false;
        Data.HeroProducts_Seen_Smart = false;
        Data.HeroProducts_Seen_Next = false;
    }
}
[System.Serializable]
public class DataBaseData
{
    public bool Lobby_Tutorial_Played;
    public bool Digitalized_Podcast_Played;
    public bool Digitalized_Radio_Played;
    public bool HeroProducts_Podcast_Played;
    public bool HeroProducts_Radio_Played;
    public bool Investing_Podcast_Played;
    public bool Investing_Radio_Played;
    public bool Customer_Podcast_Played;
    public bool Customer_Radio_Played;
    public bool HeroProducts_Seen_Giga;
    public bool HeroProducts_Seen_Sport;
    public bool HeroProducts_Seen_GO;
    public bool HeroProducts_Seen_Analog;
    public bool HeroProducts_Seen_Smart;
    public bool HeroProducts_Seen_Next;
}

[System.Serializable]
public class Rooms
{
    public string RoomName;
    public Podcasts podcast;
}

[System.Serializable]
public class Podcasts
{
    public string PodcastName;
    public bool PodcastPlayed;
    public int PodcastTime;
}

#endregion SaveData

[System.Serializable]
public class LoginDetails
{
    public string URL;
    public string UserName;
    public string PassWord;

    public string Authenticate()
    {
        string auth = UserName + ":" + PassWord;
        auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
        auth = "Basic " + auth;
        return auth;
    }
}