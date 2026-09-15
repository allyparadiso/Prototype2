using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CatScript : MonoBehaviour, IInteractable
{
    public static CatScript Instance { get; private set; }
    public GameManager gameManager;
    public Animator animator;
    public AudioSource purrSound;
    public float duration = 2f;
    private bool isObjectOnCooldown = false;

    [SerializeField] private TextMeshProUGUI scoreText;

    private int score;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        animator = GetComponent<Animator>();
        purrSound = GetComponent<AudioSource>();
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
        scoreText.text = score.ToString();
    }

    public bool CanInteract()
    {
        return !isObjectOnCooldown;
    }

    private IEnumerator InteractionCooldown()
    {
        isObjectOnCooldown = true;

        yield return new WaitForSeconds(2);

        isObjectOnCooldown = false;
    }
    public void Interact()
    {
        StartCoroutine(InteractionCooldown());
    }

    public void PurrEffect()
    {
        animator.SetTrigger("PurrTrigger");
        Debug.Log("Purr triggered");
        PlaySound();

        UpdateScore();
    }

    public void NoPurr()
    {

    }
    public void PlaySound()
    {
        StartCoroutine(SoundCoroutine());
    }

    private IEnumerator SoundCoroutine()
    {
        purrSound.Play();
        yield return new WaitForSeconds(duration);
        purrSound.Stop();
    }
}
