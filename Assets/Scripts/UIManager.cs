using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score;

    public static UIManager Instance { get; private set; }
    private CatScript catScript;

    private void Awake()
    {
        Instance = this;
        catScript = CatScript.Instance;
    }

    private void Start()
    {
        score = 0;
    }

    public void UpdateScore()
    {
        score++;
        UIUpdate();
    }

    public void UIUpdate()
    {
        scoreText.text = score.ToString() + " head pats.";
    }
}
