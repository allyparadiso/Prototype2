using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject resultContainer;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private List<GameObject> resultsTextBoxes = new List<GameObject>();

    private void OnEnable()
    {
        DiceRoller.OnDiceRoll += SetDiceUI;
        Dice.OnDiceResult += SetText;
    }

    private void OnDisable()
    {
        DiceRoller.OnDiceRoll += SetDiceUI;
        Dice.OnDiceResult += SetText;
    }

    private void SetDiceUI(int _diceRolled)
    {
        foreach (var textBox in resultsTextBoxes)
        {
            Destroy(textBox);
        }

        resultsTextBoxes.Clear();

        for (int i = 0; i < _diceRolled; i++)
        {
            resultsTextBoxes.Add(Instantiate(textPrefab, resultContainer.transform));
        }
    }

    private void SetText(int _diceIndex, int _diceResult)
    {
        resultsTextBoxes[_diceIndex].GetComponent<TMP_Text>().text = $"Dice {_diceIndex +  1} rolled a {_diceResult}";
    }
}
