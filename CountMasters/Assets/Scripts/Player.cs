using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerCreator playerCreator;
    private Transform Center;
    [SerializeField] private float speed;

    private void Awake()
    {
        playerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<PlayerCreator>();
        Center = GameObject.FindGameObjectWithTag("PlayerBase").transform;
    }

    void FixedUpdate()
    {
        if (!playerCreator.holdoff)
        {
            transform.position = Vector3.MoveTowards(transform.position, Center.position, Time.fixedDeltaTime * speed);
        }
    }
}
