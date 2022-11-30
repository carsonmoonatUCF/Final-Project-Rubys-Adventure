using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectable : MonoBehaviour
{
    public AudioClip collectedClip;

    public GameObject ammoEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ammo += 4;

            controller.PlaySound(collectedClip);

            //GameObject ammoEff = Instantiate(this.ammoEffect, controller.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            //Destroy(ammoEff, 1.0f);

            Destroy(gameObject);
        }
    }
}
