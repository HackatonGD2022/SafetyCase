using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraControll : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float interpolation = 0.125f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(target.position, desiredPosition, interpolation);

        
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }
}
