using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] private Vector2 speed;
    [SerializeField] private float timer;

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = speed;
        Destroy(gameObject, timer);
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = speed;
    }
}
