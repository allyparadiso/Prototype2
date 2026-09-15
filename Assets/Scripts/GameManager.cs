using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static CatScript;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CatScript catScript;
    public int randomNumber;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        
    }

    public void TriggerRandomEffect()
    {
        //catScript = CatScript.Instance;

        randomNumber = Random.Range(1, 3);
        if (randomNumber == 1)
        {
            catScript.PurrEffect();
        }
        else if (randomNumber == 2)
        {
            catScript.NoPurr();
        }
    }
}
