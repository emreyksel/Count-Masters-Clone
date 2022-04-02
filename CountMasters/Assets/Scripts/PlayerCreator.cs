using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCreator : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI playerCountText;
    public List<GameObject> players = new List<GameObject>();
    [SerializeField] public bool holdoff = false;

    private void Update()
    {
        playerCountText.text = players.Count.ToString();
    }

    public void SpawnPlayer(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject newPlayer = Instantiate(player, PlayerPosition(), Quaternion.identity, transform);
            players.Add(newPlayer);           
        }
    }

    public Vector3 PlayerPosition()
    {
        Vector3 pos = Random.insideUnitSphere*0.1f;
        Vector3 newPos = transform.position + pos;
        newPos.y = 0.5f;
        return newPos;
    }
}
