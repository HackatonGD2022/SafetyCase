using UnityEngine;

public class TakeCarControll : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject ExitPoint;

    [SerializeField] private Camera CarCamera;
    private Camera MainCamera = null;

    private bool CanTakeControll = false;
    private bool TakedControll = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E is pressed!");
            if(CanTakeControll)
            {
                if (!TakedControll)
                {
                    MainCamera = Camera.main;
                    MainCamera.enabled = false;
                    CarCamera.enabled = true;
                    TakedControll = true;
                    Player.SetActive(false);
                    Debug.Log("Setup car camera");
                    CarBehaviour component = GetComponentInParent<CarBehaviour>();
                    
                    if (!component)
                        Debug.Log("No car behaviour component in parent object");
                    else
                    {
                        component.enabled = true;
                        component.Controll = true;

                        CarAutoDriving carAutoDriving = GetComponentInParent<CarAutoDriving>();
                        carAutoDriving.enabled = true;
                    }


                }
                else
                {
                    CarCamera.enabled = false;
                    MainCamera.enabled = true;
                    TakedControll = false;

                    Player.transform.position = ExitPoint.transform.position;
                    Player.transform.rotation = ExitPoint.transform.rotation;

                    Player.SetActive(true);
                    Debug.Log("Setup main camera");

                    CarBehaviour component = GetComponentInParent<CarBehaviour>();
                    
                    if (!component)
                        Debug.Log("No car behaviour component in parent object");
                    else
                    {
                        component.enabled = false;
                        component.Controll = false;

                        CarAutoDriving carAutoDriving = GetComponentInParent<CarAutoDriving>();
                        carAutoDriving.enabled = false;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CanTakeControll = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            CanTakeControll = false;
        }
    }
}
