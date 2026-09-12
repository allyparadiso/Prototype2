using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiceRoller : MonoBehaviour
{
    public Dice dicePrefab;
    public int amountOfDice = 2;
    public float throwForce = 5f;
    public float rollForce = 10f;

    public static UnityAction<int> OnDiceRoll;

    private List<GameObject> spawnedDice = new List<GameObject>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RollDice());
        }
    }

    private IEnumerator RollDice()
    {
        OnDiceRoll?.Invoke(amountOfDice);

        if (dicePrefab == null) { yield break; }

        foreach (var die in spawnedDice)
        {
            Destroy(die);
        }

        for (int i = 0; i < amountOfDice; i++)
        {
            Dice dice = Instantiate(dicePrefab, transform.position, transform.rotation);
            spawnedDice.Add(dice.gameObject);
            dice.RollDice(throwForce, rollForce, i);
            yield return null;
        }
    }
}
