using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private PlayerCreator playerCreator;

    private void Awake()
    {
        playerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<PlayerCreator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SetActive(false);
        playerCreator.players.Remove(collision.gameObject);
        collision.transform.parent = null;
        StartCoroutine(HoldOff());
    }

    IEnumerator HoldOff()
    {
        playerCreator.holdoff = true;
        yield return new WaitForSeconds(0.75f);
        playerCreator.holdoff = false;
    }
}
