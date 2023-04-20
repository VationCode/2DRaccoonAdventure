using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    // Update is called once per frame
    float t = 0;
    public float speed;
    void Update()
    {
        transform.position = transform.position + transform.up * Time.deltaTime * speed;
        t += Time.deltaTime;
        if (t > 20.0f)
            Destroy(gameObject);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        Player controller = other.GetComponent<Player>();

        if (controller != null)
        {
            //the controller will take care of ignoring the damage during the invincibility time.
            controller.ChangeHealth(-1);
        }
    }
}
