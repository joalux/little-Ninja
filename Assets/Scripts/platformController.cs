using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformController : MonoBehaviour
{

    public GameObject[] movingPLatforms;

    public Transform[] platformSpots;

    private int position = 1;

    public float platformSpeed = 5;

    public float waitTime, startWaitTime;

    
    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        movingPLatforms[0].transform.position = Vector2.MoveTowards(transform.position, platformSpots[position].position, platformSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, platformSpots[position].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                
                if (position == 0)
                    position = 1;
                else
                    position = 0;

                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

    }
}
