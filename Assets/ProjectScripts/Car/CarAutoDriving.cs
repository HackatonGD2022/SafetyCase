using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAutoDriving : MonoBehaviour
{
    [SerializeField] private GameObject Car;

    [SerializeField] private Transform[] Waypoints;

    private int CurrentWaypointIndex = 0;

    private float MaxSpeed
    {
        get;
        set;
    } = 50.0f;

    [SerializeField] private bool driving;
    public bool Driving
    {
        get { return driving; }
        set { driving = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(driving)
        {
            CarBehaviour car = Car.GetComponent<CarBehaviour>();
            // move forward
            float distance = Vector3.Distance(Waypoints[CurrentWaypointIndex].position, Waypoints[CurrentWaypointIndex + 1].position);

            if (car.Speed < MaxSpeed)
                car.Accelerate(700);

            float brakeS = (1.0f / (254.0f * 0.4f)) * car.Speed * 10;

            Debug.Log("Speed: " + car.Speed);

            if (distance - 5 < brakeS && car.Speed > 5)
                car.Brake(500);

            // turn
            Vector3 forward = Car.transform.forward;
            Debug.Log("Forward: " + forward);

            Vector3 toPoint = Car.transform.TransformPoint(Waypoints[CurrentWaypointIndex + 1].transform.position).normalized;
            Debug.Log("To point: " + toPoint);

            //float v = -(toPoint.x - forward.x ) * 3.14f;

            //Debug.Log("v: " + v);

            //car.Turn(v);

            float sign = (toPoint.x > forward.x) ? 1.0f : -1.0f;

            float v = distance / Vector3.Angle(forward, toPoint) * sign;

            Debug.Log(v);

            car.Turn(v);


            Waypoints[CurrentWaypointIndex].position = Car.transform.position;
            car.UpdateWheels();
        }
    }

    private float AngleBetweenVector2(Vector3 vec1, Vector3 vec2)
    {
        Vector3 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector3.Angle(Vector3.right, diference) * sign;
    }
}
