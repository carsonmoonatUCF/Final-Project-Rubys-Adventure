using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowbotController : MonoBehaviour
{
    float normalSpeed = 0.0f;
    public float slowedSpeed = 0.0f;

    public Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other) {
        RubyController controller = other.GetComponent<RubyController>();

        if(controller != null){
            normalSpeed = controller.speed;

            controller.speed = slowedSpeed;
        }

    }

    private void OnTriggerExit2D(Collider2D other) {
        RubyController controller = other.GetComponent<RubyController>();

        if(controller != null){
            controller.speed = normalSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        //UIFixedRobots.instance.FixedRobot();
        //broken = false;
        rb.simulated = false;
        this.gameObject.SetActive(false);    
        //animator.SetTrigger("Fixed");
        //smokeEffect.Stop();
    }
}
