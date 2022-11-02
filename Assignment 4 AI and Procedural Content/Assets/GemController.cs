using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //gameObject.SetActive(false);
            ToNewPosition();
        }
    }

    private void ToNewPosition()
    {
        float posX = Random.Range(-8.46f, 8.46f);
        transform.position = new Vector3(posX, 5.29f, transform.position.z);
    }
}
