using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private RubyController controller;

    public AudioClip explosionSFX;
    public GameObject explosionEffect;

    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Fix();
        }

        HardEnemyController hE = other.collider.GetComponent<HardEnemyController>();
        if (hE != null)
        {
            hE.Fix();
        }

        TrackerBotController tB = other.collider.GetComponent<TrackerBotController>();
        if(tB != null){
            tB.Fix();
        }

        SlowbotController sB = other.collider.GetComponent<SlowbotController>();
        if(sB != null){
            sB.Fix();
        }

        controller.PlaySound(explosionSFX);
        GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.0f);

        Destroy(gameObject);
    }

    public void GetRubyController(RubyController ruby){
        controller = ruby;
    }
}
