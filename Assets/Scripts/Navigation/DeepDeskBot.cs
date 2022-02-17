using UnityEngine;
using System.Collections;
public enum DeepDeskRoutes
{
    None,
    Inactive,
    Teleport,
    Radio
}

public class DeepDeskBot : MonoBehaviour
{
    public int activePath = -1;
    public GameObject FirstDisplayMessage;
    public GameObject RefreshButton;
    public TextPath[] textPaths;
    private TextPath ActivePath;

    public void UpdatePath()
    {
        ActivePath.FollowPath();
        if (ActivePath.StoryProgression >= ActivePath.MaxInteractions)
        {
            RefreshScreen();
        }
    }

    public void SetActivePath(GameObject _hit)
    {
        for (int i = 0; i < textPaths.Length; i++)
        {
            if (_hit == textPaths[i].FirstChoice)
            {
                ActivePath = textPaths[i];
                activePath = i;
                break;
            }
        }
    }

    public void RefreshScreen()
    {
        StartCoroutine(ClearScreen());
    }
    public IEnumerator ClearScreen()
    {
        yield return new WaitForSeconds(3f);
        ActivePath.RefreshUI();
        activePath = -1;
        ActivePath = null;
    }

}

[System.Serializable]
public class TextPath
{
    [Header("Stats (DO NOT CHANGE)")]
    public DeepDeskRoutes Route;

    public int PathNumber;
    public int StoryProgression;
    public int MaxInteractions;

    [Header("GameObjects")]
    public GameObject FirstChoice;
    public GameObject FirstChoiceDisplay;
    public GameObject FirstChoiceDisplayBackground;
    public GameObject FirstResponse;
    public GameObject SecondChoice;
    public GameObject SecondChoiceDisplay;
    public GameObject SecondResponse;

    public GameObject Lines;

    public void SetMaxInteractions(int i) => MaxInteractions = i;

    public void InactivePath()
    {
        if (StoryProgression == 0)
        {
            FirstChoiceDisplayBackground.SetActive(true);
            FirstChoiceDisplay.SetActive(true);
            FirstResponse.SetActive(true);
            StoryProgression++;
        }
    }

    public void TeleportPath()
    {
        if (StoryProgression == 0)
        {
            FirstChoiceDisplayBackground.SetActive(true);

            FirstResponse.SetActive(true);
            FirstChoiceDisplay.SetActive(true);
            FirstChoice.transform.parent.gameObject.SetActive(false);
            StoryProgression++;
            if (StoryProgression < MaxInteractions)
            {
                SecondChoice.SetActive(true);
            }
        }
        else if (StoryProgression == 1)
        {
            StoryProgression++;
        }
    }
    public void RadioPath()
    {
        if (StoryProgression == 0)
        {
            FirstChoiceDisplayBackground.SetActive(true);
            FirstChoiceDisplay.SetActive(true);
            FirstResponse.SetActive(true);
            StoryProgression++;
        }
    }
    public void RefreshUI()
    {
        if(FirstChoiceDisplayBackground)
        FirstChoiceDisplayBackground.SetActive(false);
        if(FirstChoiceDisplay)
        FirstChoiceDisplay.SetActive(false);
        if(FirstResponse)
        FirstResponse.SetActive(false);
        if(FirstChoice)
        FirstChoice.transform.parent.gameObject.SetActive(true);
        if(SecondChoice)
        SecondChoice.SetActive(false);
        StoryProgression = 0;
    }

    public void FollowPath()
    {
        switch (Route)
        {
            //Nothing Keep Empty
            case DeepDeskRoutes.None:
                break;

            //Player wants to look around
            case DeepDeskRoutes.Inactive:
                SetMaxInteractions(1);
                InactivePath();
                break;
            //Player teleported to wrong room
            case DeepDeskRoutes.Teleport:
                SetMaxInteractions(2);
                TeleportPath();
                break;
            //Player want to go to radio
            case DeepDeskRoutes.Radio:
                SetMaxInteractions(1);
                RadioPath();
                break;
        }
    }
}