using UnityEngine;

public class Projectile : MonoBehaviour
{
  public Vector3 direction = Vector3.up;
    public float speed = 25f;
    public System.Action onDestroy;
    
    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.right * this.speed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.CompareTag("Enemy"))
        this.onDestroy?.Invoke();
        Destroy(this.gameObject);

    }
}
