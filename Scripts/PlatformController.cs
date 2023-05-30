using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    [SerializeField] private Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-1 * speed, 0);
    }
}
