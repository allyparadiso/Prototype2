using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    [SerializeField] private float tolerance = 0.99f;

    private Rigidbody rb;
    private bool stoppedRolling = false;
    private bool throwDelayFinished = false;
    private int diceIndex = -1; // if multiple dice

    public static UnityAction<int, int> OnDiceResult;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!throwDelayFinished) { return; }

        if (!stoppedRolling && rb.linearVelocity.sqrMagnitude == 0)
        {
            stoppedRolling = true;
            GetSideUp();
        }
    }

    private void GetSideUp()
    {
        Vector3[] sides = new Vector3[]
        {
            transform.right.normalized,     //1
            -transform.forward.normalized,  //2
            -transform.up.normalized,       //3
            transform.up.normalized,        //4
            transform.forward.normalized,   //5
            -transform.right.normalized     //6
            
        };

        for (int i = 0; i < sides.Length; i++)
        {
            if (Vector3.Dot(sides[i], Vector3.up) > tolerance)
            {
                OnDiceResult?.Invoke(diceIndex, i+1);
            }
        }

        Debug.Log("No sides within tolerance");
    }

    internal void RollDice(float _throwForce, float _rollForce, int _diceIndex)
    {
        diceIndex = _diceIndex;

        float randomVarience = Random.Range(-1f, 1f);
        rb.AddForce(transform.forward * (_throwForce + randomVarience), ForceMode.Impulse);

        float rollX = Random.Range(0f, 1f);
        float rollY = Random.Range(0f, 1f);
        float rollZ = Random.Range(0f, 1f);

        rb.AddTorque(new Vector3(rollX, rollY, rollZ) * (_rollForce * randomVarience));

        StartCoroutine(ThrowDelay());
    }

    private IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(1);
        throwDelayFinished = true;
    }
}
