using UnityEngine;

public class RayCastInteraction : MonoBehaviour
{
    public GameObject Cam;
    public GameObject InteractionText;
    public LayerMask IntMask;
    private GameObject SelectedObject;
    private GameObject PreviousAudio;
    [Range(0, 10)] public float Distance = 5f;


    public void Update()
    {
        if (SelectedObject)
        {
            InteractionText.SetActive(true);
        }
        if (CameraMovement.inMenu)
        {
            if (!SelectedObject) return;

            if (SelectedObject.GetComponent<NavGroup>())
                SelectedObject.GetComponent<NavGroup>().DeSelect();
            SelectedObject = null;
            InteractionText.SetActive(false);
            return;
        }
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out RaycastHit _hit, Distance, IntMask))
        {

            #region DeepDeskBot

            if (_hit.transform.GetComponentInParent<DeepDeskBot>() && _hit.transform.CompareTag("Option"))
            {
                var bot = _hit.transform.GetComponentInParent<DeepDeskBot>();
                SelectedObject = _hit.transform.gameObject;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (bot.activePath < 0)
                        bot.SetActivePath(_hit.transform.gameObject);
                    if (SelectedObject.GetComponent<NavGroup>())
                        SelectedObject.GetComponent<NavGroup>().TeleportToLocation(gameObject);
                    bot.UpdatePath();
                }
                return;
            }

            #endregion DeepDeskBot
            #region NavGroup

            if (_hit.transform.GetComponent<NavGroup>())
            {
                if (SelectedObject && SelectedObject != _hit.transform.gameObject)
                    SelectedObject.GetComponent<NavGroup>().DeSelect();
                SelectedObject = _hit.transform.gameObject;
                SelectedObject.GetComponent<NavGroup>().Select();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SelectedObject.GetComponent<NavGroup>().TeleportToLocation(gameObject);
                }
                return;
            }

            #endregion NavGroup
            #region CustomVideoPlayer

            if (_hit.transform.GetComponentInParent<CustomVideoPlayer>())
            {
                SelectedObject = _hit.transform.gameObject;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    switch (_hit.transform.tag)
                    {
                        case "Play":
                            if (PreviousAudio) PreviousAudio.GetComponent<AudioSource>().Pause();
                            _hit.transform.GetComponentInParent<CustomVideoPlayer>().PlayFunction();
                            PreviousAudio = _hit.transform.GetComponentInParent<CustomVideoPlayer>().gameObject;
                            break;

                        case "Pause":
                            _hit.transform.GetComponentInParent<CustomVideoPlayer>().PauseFunction();
                            PreviousAudio = null;
                            break;

                        default:
                            break;
                    }
                }
                return;
            }

            #endregion CustomVideoPlayer
            #region HeroProducts

            if (_hit.transform.GetComponent<HeroProductsBookClass>())
            {
                SelectedObject = _hit.transform.gameObject;
                var book = _hit.transform.GetComponent<HeroProductsBookClass>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    book.StartCoroutineTween();

                }
                return;
            }

            #endregion HeroProducts
            #region Audio
            if (_hit.transform.GetComponent<PlayAudio>())
            {
                PlayAudio Source = _hit.transform.GetComponent<PlayAudio>();
                if (!Input.GetKeyDown(KeyCode.E)) return;
                 
                if (!Source.isPlaying)
                {
                    if (PreviousAudio) PreviousAudio.GetComponent<AudioSource>().Pause();
                    Source.Play();
                    PreviousAudio = _hit.transform.gameObject;
                }
                else
                {
                    Source.Pause();
                    PreviousAudio = null;
                }               
               
            }
            #endregion
        }

        else
        {
            if (!SelectedObject) return;

            if(SelectedObject.GetComponent<NavGroup>())
            SelectedObject.GetComponent<NavGroup>().DeSelect();
            SelectedObject = null;
            InteractionText.SetActive(false);
            return;
        }


    }
}