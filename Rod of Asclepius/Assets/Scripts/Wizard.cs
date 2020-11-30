﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wizard : Enemy
{
    // Fields
    public GameObject spotLight;

    // Abilities
    public GameObject electricBallPrefab;
    public float electricBallCooldown;
    private float electricBallTimeTillCooldown;
    public float electricBallRange;
    public float projectileSpeed;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        electricBallCooldown = 2.0f;
        electricBallTimeTillCooldown = 0.0f;
        electricBallRange = 15f;

        projectileSpeed = 4.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // check for game state
        if (sceneMan.GetComponent<SceneMan>().gameState == GameState.Game)
        {
            // shoot electric balls when within a certain distance of the player
            if (Mathf.Pow(player.transform.position.x - transform.position.x, 2) + Mathf.Pow(player.transform.position.z - transform.position.z, 2) <= Mathf.Pow(electricBallRange, 2))
            {
                // create the electric ball and reset cooldown
                if (electricBallTimeTillCooldown >= electricBallCooldown && silenced == false)
                {
                    GameObject electricBall = Instantiate(electricBallPrefab, transform.position, Quaternion.identity);
                    electricBall.GetComponent<Rigidbody>().AddForce(new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z).normalized * projectileSpeed, ForceMode.Impulse);
                    electricBallTimeTillCooldown = 0;
                }
            }

            // slow down when within a certain distance of the player
            if (Mathf.Pow(player.transform.position.x - transform.position.x, 2) + Mathf.Pow(player.transform.position.z - transform.position.z, 2) <= Mathf.Pow(12, 2))
            {
                GetComponent<NavMeshAgent>().speed = 4.5f;
            }
            // speed up when player is farther away and do not attack
            else
            {
                GetComponent<NavMeshAgent>().speed = 7.0f;
            }

            // update cooldown
            electricBallTimeTillCooldown += Time.deltaTime;
        }
        // game state is not game
        else
        {
            GetComponent<NavMeshAgent>().speed = 6.0f;
        }
    }

    // Handles collisions w/ player
    //private void OnCollisionStay(Collision collision)
    //{
    //    // Handles collision w/ player
    //    if (collision.gameObject.tag == "Player" &&
    //        player.GetComponent<Player>().hasCollided == false &&
    //        player.GetComponent<Player>().health > 0 && silenced == false)
    //    {
    //        Debug.Log("Damaged Player");
    //        player.GetComponent<Player>().health--;
    //        player.GetComponent<Player>().hasCollided = true;
    //    }
    //}
}