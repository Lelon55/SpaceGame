using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    private float speed;
    private Vector2 movement;
    public Rigidbody2D gravity;

    private Transform player;
    public Transform enemy;

    private bool moving = false;
    private float reactionMovement;

    private void Start()
    {
        player = GameObject.Find("spaceship").GetComponent<Transform>();
        SetReactionMovement(Random.Range(0.1f, 0.2f));
        SetEnemySpeed(Random.Range(7f, 14f));
    }

    private void SetEnemySpeed(float speed)
    {
        this.speed = speed;
    }

    private float GetEnemySpeed()
    {
        return speed;
    }

    private void SetReactionMovement(float reaction)
    {
        reactionMovement = reaction;
    }

    private float GetReactionMovement()
    {
        return reactionMovement;
    }

    internal void MovementEnemy()
    {
        movement = new Vector2(GetEnemySpeed(), 0f);

        if (enemy.position.x > player.position.x + 0.3f)
        {
            movement.x = SetDirection("left");
            SetEnemyMoving(true);
        }
        else if (enemy.position.x < player.position.x - 0.3f)
        {
            movement.x = SetDirection("right");
            SetEnemyMoving(true);
        }
        else if (enemy.position.x == player.position.x || enemy.position.x <= player.position.x + 0.2f || enemy.position.x >= player.position.x - 0.2f)
        {
            movement.x = SetDirection("stop");
            SetEnemyMoving(false);
        }
        gravity.velocity = movement;
    }

    private float SetDirection(string direct)
    {
        if (direct == "left")
        {
            return GetEnemySpeed() * -GetReactionMovement();
        }
        if (direct == "right")
        {
            return GetEnemySpeed() * GetReactionMovement();
        }
        return 0f;
    }

    private void SetEnemyMoving(bool moving)
    {
        this.moving = moving;
    }

    internal bool GetEnemyMoving()
    {
        return moving;
    }

}
