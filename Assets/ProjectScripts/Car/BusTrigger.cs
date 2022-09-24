using UnityEngine;

public class BusTrigger : MonoBehaviour
{
    [SerializeField] private GameObject SeatPoint;
    [SerializeField] private GameObject ExitPoint;

    private GameObject Player;
    private bool PlayerIsEntered = false;


    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void Update()
    {
        if (PlayerIsEntered)
        {

            FPSCameraContoller contoller = Player.GetComponent<FPSCameraContoller>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!contoller.Seated)
                {
                    // make seat
                    contoller.Seated = true;

                    Player.transform.position = SeatPoint.transform.position;
                    Player.transform.rotation = SeatPoint.transform.rotation;

                    GetComponentInParent<CarBehaviour>().enabled = true;

                }
                else
                {
                    // make exit
                    Player.transform.position = ExitPoint.transform.position;
                    Player.transform.rotation = ExitPoint.transform.rotation;

                    contoller.Seated = false;

                    GetComponentInParent<CarBehaviour>().enabled = false;

                }

            }

            if(contoller.Seated)
            {
                Player.transform.position = SeatPoint.transform.position;
                Player.transform.rotation = ExitPoint.transform.rotation;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerIsEntered = true;
            Player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerIsEntered = false;
            Player = null;
        }
    }

}
