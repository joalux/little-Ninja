using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObjectOverTime : MonoBehaviour
{

    public float lifeTime;
    public GameObject blood;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            Debug.Log("COLLISION WITH FLOOR");
            gameObject.SetActive(false);
        }

   




    }
}

