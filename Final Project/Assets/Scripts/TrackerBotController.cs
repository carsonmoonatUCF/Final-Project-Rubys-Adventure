using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerBotController : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed = 0.0f;
    Transform target = null;

    bool isFollowing = false;

    float horizontal;
    float vertical;

    float hitCooldown = 0.0f;
    public float hitTimer;

    public Animator animator;

    public AudioClip beepSFX;
    public bool hasPlayedSFX = false;

    private void Update() {
        if(isFollowing && hitCooldown < 0){
            Vector3 followDirection = (target.position - transform.position).normalized;

            horizontal = followDirection.x;
            vertical = followDirection.y;
        }

        hitCooldown -= Time.deltaTime;
    }

    private void FixedUpdate() {
        if(isFollowing && hitCooldown < 0){
            Vector2 position = rb.position;
            position.x = position.x + speed * horizontal * Time.deltaTime;
            position.y = position.y + speed * vertical * Time.deltaTime;

            rb.MovePosition(position);
        }else{
            rb.velocity = Vector3.zero;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        RubyController controller = other.GetComponent<RubyController>();

        if(controller != null){
            isFollowing = true;
            animator.SetInteger("chase", 1);

            if(!hasPlayedSFX){
                controller.PlaySound(beepSFX);
                hasPlayedSFX = true;
            }

            target = controller.transform;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
            hitCooldown = hitTimer;
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
