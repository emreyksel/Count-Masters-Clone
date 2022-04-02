using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerCreator playerCreator;
    private GameManager gm;
    [SerializeField] private LayerMask Wall;
    private float minXBound = -4.75f;
    private float maxXBound = 4.75f;
    public float speed = 15f;
    
    private void Awake()
    {
        playerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<PlayerCreator>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gm.gameStart)
        {
            if (Input.GetMouseButton(0))
            {
                Move();
            }

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXBound, maxXBound), transform.position.y, transform.position.z);
            CheckXBound();  
        }
    }

    private void FixedUpdate()
    {
        if (gm.gameStart)
        {
            transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
        }        
    }

    public void Move()
    {
        float swipeSpeed = 5;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.localPosition.z;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray,out RaycastHit hit, 50))
        {
            Vector3 hitPoint = hit.point;
            hitPoint.y = transform.position.y;
            hitPoint.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, hitPoint, Time.deltaTime * swipeSpeed);
        }
    }

    private void CheckXBound()
    {
        float minX = transform.position.x;
        float maxX = transform.position.x;

        for (int i = 0; i < playerCreator.players.Count; i++)
        {
            if (playerCreator.players[i].transform.position.x < minX)
            {
                minX = playerCreator.players[i].transform.position.x;
            }
            if (playerCreator.players[i].transform.position.x > maxX)
            {
                maxX = playerCreator.players[i].transform.position.x;
            }
        }

        Vector3 LeftControl = new Vector3(minX, transform.position.y, transform.position.z);
        Vector3 RightControl = new Vector3(maxX, transform.position.y, transform.position.z);

        if (Physics.Raycast(LeftControl, Vector3.left, 0.5f, Wall))
        {
            minXBound = transform.position.x;
        }
        else
        {
            minXBound = -4.75f;
        }

        if (Physics.Raycast(RightControl, Vector3.right, 0.5f, Wall))
        {
            maxXBound = transform.position.x;
        }
        else
        {
            maxXBound = 4.75f;
        }
    }
}
