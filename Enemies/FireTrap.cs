using UnityEngine;
using System.Collections;

public class FireTrap : MonoBehaviour
{
    [Header ("FireTrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    [Header ("Damage")]
    [SerializeField] private float damage;

    private bool triggered; //when trap gets triggered
    private bool active; //when trap is active

    private void Awake(){
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            if (!triggered){
                StartCoroutine(ActivateFiretrap());
            }
            if(active){
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
    private IEnumerator ActivateFiretrap(){
        //turns sprite red to notify player of danger
        triggered = true;
        spriteRend.color = Color.red; 

        //Wait for Delay, activate trap, turn on animation, color back to normal
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white; //turns sprite back
        active = true;
        anim.SetBool("activated", true);

        //Wait x seconds, deavtivate trap and reset all variables and animator
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
