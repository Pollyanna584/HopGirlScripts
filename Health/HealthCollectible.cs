using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healing;

    // public void Update(){

    // }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            collision.GetComponent<Health>().GainHealth(healing);
            gameObject.SetActive(false);
        }

    }
}
