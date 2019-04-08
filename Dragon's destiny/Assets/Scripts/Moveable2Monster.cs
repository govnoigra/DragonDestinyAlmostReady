﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Moveable2Monster : Monster
{
    [SerializeField]
    private float rasst;
    [SerializeField]
    private float speed = 2.0F;

    private Vector3 direction;

    public int monsterDirection = 1;
    public CharacterInCampaighController fireDirectionInMonster;

    private Fire fire;

    private SpriteRenderer sprite;

    new public Rigidbody2D rigidbody;

    //  private Animator animator;

    /*   private CharState State
       {
           get { return (CharState)animator.GetInteger("State"); }
           set { animator.SetInteger("State", (int)value); }
       }
       */

    protected override void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        //       animator = GetComponent<Animator>();
        fire = Resources.Load<Fire>("Fire");
    }

    protected override void Start()
    {
        direction = transform.right;
    }

    protected override void Update()
    {
        //     State = CharState.Run;
        Move();

        //    fireDirectionInMonster.fireDirection += 0;
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        //     Unit unit = collider.GetComponent<Unit>();
        CharacterInCampaighController character = collider.GetComponent<CharacterInCampaighController>();
     //   if (unit && unit is CharacterInCampaighController)
          if (character)
        {
            rasst = Mathf.Abs(character.transform.position.x - transform.position.x);
            //         if (rasst < 0.7F && fireDirectionInMonster.fireDirection > 0 && monsterDirection < 0) ReceiveDamage();
            if (rasst < 0.6F)
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce(transform.up * 8.0F, ForceMode2D.Impulse);
                ReceiveDamage(); //damageSound.Play(); 
            }
            else
            {
            
                character.ReceiveDamage(); //damageSound.Play();
             }
        }
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 1.0F + transform.right * direction.x * 0.3F, 0.25F);

        if (colliders.Length > 1 && colliders.All(x => !x.GetComponent<CharacterInCampaighController>()))
        {
            direction *= -1.0F;
            monsterDirection *= -1;
            sprite.flipX = direction.x > 0.0f;
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
}