using UnityEditor.UIElements;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Pickups")]
    public int totalPickups = 5;
    public int collectedPickups = 0;

    [Header("UI elements")]
    public TMP_Text itemDisplay;
    public GameObject winPanel;


    bool _gameOver = false;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //Use if you want game manager to persist between scenes
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(itemDisplay != null)
        {
            UpdateUI();
        }

        if(winPanel != null)
        {
            winPanel.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPickupCollected()
    {
        if (_gameOver) return;
        collectedPickups++;
        UpdateUI();

    }

    public void OnGoalReached()
    {
        
        if (_gameOver) return;
        _gameOver = true;

        //Change the display message and activate the win panel

        if (winPanel != null)
        {
            winPanel.SetActive(true);
            winPanel.GetComponentInChildren<TMP_Text>().text = $"You did it! You collected all {collectedPickups} items. Goal reached!";
        }
    }

    void UpdateUI()
    {
        if(itemDisplay != null)
        {
            itemDisplay.text = $"Items: {collectedPickups}/{totalPickups}";
        }
    }
}
