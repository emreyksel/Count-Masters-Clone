using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public enum SpawnerState
    {
        additive,
        multiplier
    }

    public SpawnerState currentMathState;
    private PlayerCreator playerCreator;
    private TextMesh sizeText;
    private MeshRenderer meshRenderer;
    public int size;
    public static bool isGateActive = true;

    private void Awake()
    {
        playerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<PlayerCreator>();
        sizeText = GetComponentInChildren<TextMesh>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        switch (currentMathState)
        {
            case SpawnerState.additive:
                sizeText.text = "+" + size.ToString();
                break;
            case SpawnerState.multiplier:
                sizeText.text = "x" + size.ToString();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGateActive)
        {
            meshRenderer.enabled = false;
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(GateActive());

            switch (currentMathState)
            {
                case SpawnerState.additive:
                    playerCreator.SpawnPlayer(size);
                    break;
                case SpawnerState.multiplier:
                    int multiplierSize = playerCreator.players.Count * size - playerCreator.players.Count;
                    playerCreator.SpawnPlayer(multiplierSize);
                    break;
            }
        }
    }

    public IEnumerator GateActive()
    {
        isGateActive = false;
        yield return new WaitForSeconds(1.7f);
        isGateActive = true;
    }
}
