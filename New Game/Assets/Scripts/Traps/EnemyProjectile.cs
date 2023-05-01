using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage //Will damage the player every time they touch
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;

    private float lifetime;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);

    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); //Execute the logic from parent script first 
        gameObject.SetActive(false); //When this object hits any object -> deactivate arrow

        if (anim != null)
            anim.SetTrigger("explode");//When the object is a fireball explode it
        else
            gameObject.SetActive(false); //When this object hits any object -> deactivate arrow
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
