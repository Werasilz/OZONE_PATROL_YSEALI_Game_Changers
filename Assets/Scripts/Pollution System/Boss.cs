using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IInteractable
{
    Animator animator;
    [SerializeField] private int bossHP = 1000;
    [SerializeField] private int damage = 50;
    private bool isDead;

    [Header("Spawn Point")]
    [SerializeField] private Transform bottomLeftSpawnPoint;
    [SerializeField] private Transform bottomRightSpawnPoint;
    [SerializeField] private Transform topLeftSpawnPoint;
    [SerializeField] private Transform topRightSpawnPoint;

    [Header("User Interface")]
    [SerializeField] private GameObject successScreen;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (isDead == false)
        {
            if (bossHP <= 0)
            {
                bossHP = 0;
                isDead = true;
                animator.Play("Dead");
                successScreen.SetActive(true);
            }
            else
            {
                bossHP -= damage;
                animator.Play("Hit");
                Vector3 spawnPosition = new Vector3(Random.Range(bottomLeftSpawnPoint.position.x, bottomRightSpawnPoint.position.x), Random.Range(bottomLeftSpawnPoint.position.y, topLeftSpawnPoint.position.y));
                PollutionManager.Instance.ReducePollutionScore(damage, spawnPosition);
            }
        }
    }
}
