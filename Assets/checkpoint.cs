using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{

    private gameController gm;

    public int ID;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<gameController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("checkpoint");
            print(ID);
            gm.activeCheckpoint = ID;

            //gm.checkpoints[ID].transform.position = transform.position;
        }
    }
}
