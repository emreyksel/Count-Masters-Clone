using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private PlayerCreator playerCreator;
    public GameObject failPanel;
    [Header("UI References :")]
    public Image fillImage;
    public TextMeshProUGUI tapToStartText;
    [Header("Transform References :")]
    public Transform player;
    public Transform end;
  
    private float fullDistance;
    [HideInInspector] public bool gameStart = false;

    private void Awake()
    {
        playerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<PlayerCreator>();
        fullDistance = GetDistance();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            gameStart = true;
            tapToStartText.gameObject.SetActive(false);
        }

        if (playerCreator.players.Count == 0)
        {
            failPanel.SetActive(true);
            gameStart = false;
        }

        float newDistance = GetDistance();
        float progressValue = Mathf.InverseLerp(fullDistance,0,newDistance);
        UpdateProgressFill(progressValue);
    }

    private float GetDistance()
    {
        Vector3 zPlayer = new Vector3(0, 0, player.position.z);
        Vector3 zEnd = new Vector3(0, 0, end.position.z);
        return Vector3.Distance(zPlayer, zEnd);
    }

    public void UpdateProgressFill(float value)
    {
        if (gameStart)
        {
            fillImage.fillAmount = value;
        }     
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
