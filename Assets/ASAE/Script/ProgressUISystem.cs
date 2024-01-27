using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressUISystem : MonoBehaviour
{
    
   [SerializeField, Range(1.0f, 100.0f)]
    public float progress;

    float maxProgress;
    [SerializeField]
    private Transform progressObject;
    [SerializeField]
    private float progressSpeed;
    // Start is called before the first frame update
    void Start()
    {
        maxProgress = 27.2f;
    }

    // Update is called once per frame
    void Update()
    {

        if (progressObject.position.y > maxProgress + 1) {
            GameInProgress();
        }
        else
        {
            //ƒQ[ƒ€ƒNƒŠƒAˆ—
        }

    }

    void GameInProgress()
    {
        if (progressObject.position.y > maxProgress + 1) return;
        progress = progressObject.position.y / maxProgress;
        Debug.Log(progress * 100 + "%");
        progressObject.Translate(0,progressSpeed*Time.deltaTime, 0);
    }
}
