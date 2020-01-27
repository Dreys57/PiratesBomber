using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float timer;

    private Rigidbody2D rigidBody;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            animator.SetBool("explode", true);
        }
    }

    private void DestroyBomb()
    {
        Destroy(gameObject);
    }
}
