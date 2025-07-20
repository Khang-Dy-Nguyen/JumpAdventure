using UnityEngine;

public class Invader : MonoBehaviour
{
    public System.Action killed;
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            Debug.Log("Invader hit by projectile!");
            this.killed?.Invoke();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            
        }
    }
}