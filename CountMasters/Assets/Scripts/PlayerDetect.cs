using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    private PlayerController playerController;
    private EnemyCreator enemyCreator;
    public SphereCollider sphCol;
    private bool isplayerDetect = false;
    public bool rush = false;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<PlayerController>();
        enemyCreator = GameObject.FindGameObjectWithTag("EnemyBase").GetComponent<EnemyCreator>();       
    }

    private void Start()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isplayerDetect == false)
        {
            isplayerDetect = true;
            StartCoroutine(Stop());
            sphCol.enabled = false;
            rush = true;
        }
    }

    private IEnumerator Stop()
    {
        playerController.enabled = false;
        yield return new WaitUntil(() => enemyCreator.isEnemyAlive == false);
        playerController.enabled = true;
    }
}
