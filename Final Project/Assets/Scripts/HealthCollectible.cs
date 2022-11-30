using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    public GameObject healingEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(1);

                controller.PlaySound(collectedClip);

                GameObject healingEff = Instantiate(this.healingEffect, controller.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                Destroy(healingEff, 1.0f);

                Destroy(gameObject);
            }

        }
    }
}
