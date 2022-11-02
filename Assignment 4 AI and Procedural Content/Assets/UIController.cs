using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI highestPointsText;

    private int points = 0;
    private int highestPoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        highestPoints = PlayerPrefs.GetInt("HighestPoints", 0);

        pointsText.text = points.ToString() + " POINTS";
        highestPointsText.text = "HIGHEST: " + highestPoints.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint()
    {
        points += 1;
        pointsText.text = points.ToString() + " POINTS";

        if (highestPoints < points)
        {
            PlayerPrefs.SetInt("HighestPoints", points);
        }
    }

    private void Awake()
    {
        instance = this;
    }
}
