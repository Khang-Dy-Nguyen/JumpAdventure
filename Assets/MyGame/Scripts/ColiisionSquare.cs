using UnityEngine;

public class ColiiSquare : MonoBehaviour
{
void OnCollisionEnter2D(Collision2D collision) 
{
    Debug.Log("Collision detected with: " + collision.gameObject.name);
    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.color = new Color(0, 255, 255);
}
}
