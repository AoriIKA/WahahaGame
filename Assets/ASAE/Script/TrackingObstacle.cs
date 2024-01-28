using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingObstacle : MonoBehaviour
{
    [SerializeField]
    Transform targetObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = targetObject.position;
        transform.eulerAngles = new Vector3(targetObject.localEulerAngles.z, 0,0);
    }
}
