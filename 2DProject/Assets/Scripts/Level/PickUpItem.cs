using System;
using UnityEngine;
using UnityEngine.VFX;
using System.Collections.Generic;

public class PickUpItem : MonoBehaviour
{
    [Header("Scripts")] public static PickUpItem instance;
    [Header("Variables")] public bool isGem, isHealth;
    private bool isCollected;
    public float followSpeed;
    [SerializeField] private string id;
    [Header("Game Objects")] public GameObject pickupEffect;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (gameObject.CompareTag("Gem"))
        {
            isGem = true;
        }
        else if (gameObject.CompareTag("Health"))
        {
            isHealth = true;
        }

        if (PlayerController.instance.isUnderWater)
        {
            var distance = Vector2.Distance(PlayerController.instance.transform.position, transform.position);

            if (Mathf.Abs(distance) < 10f)
            {
                if (gameObject.CompareTag("Health") && PlayerHealthController.instance.currentHealth < 6)
                {
                    transform.position = Vector2.MoveTowards(transform.position,
                        PlayerController.instance.transform.position, followSpeed);
                }

                if (gameObject.CompareTag("Gem"))
                {
                    transform.position = Vector2.MoveTowards(transform.position,
                        PlayerController.instance.transform.position, followSpeed);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isCollected)
        {
            if (isGem)
            {
                LevelManager.gemsCollected++;
                this.isCollected = true;
                this.gameObject.SetActive(false);
                var transform1 = transform;
                Instantiate(pickupEffect, transform1.position, transform1.rotation);
                UIController.instance.UpdateGemCount();
                AudioManager.instance.PlaySfx(6);
            }
            else if (isHealth)
            {
                if (PlayerHealthController.instance.currentHealth == PlayerHealthController.instance.maxHealth) return;
                PlayerHealthController.instance.HealPlayer(1);
                this.isCollected = true;
                this.gameObject.SetActive(false);
                var transform1 = transform;
                Instantiate(pickupEffect, transform1.position, transform1.rotation);
                AudioManager.instance.PlaySfx(7);
            }
        }
    }
}