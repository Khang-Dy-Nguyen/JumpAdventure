using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Tốc độ di chuyển
    public float jumpForce = 100f;  // Lực nhảy
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Nhận input từ bàn phím (A, D, mũi tên trái/phải)
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.AddForce(new Vector2(moveInput * moveSpeed, 0), ForceMode2D.Force);
        // Kiểm tra nếu nhấn Space và nhân vật đang trên mặt đất thì nhảy
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu chạm đất
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Kiểm tra nếu rời khỏi mặt đất
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
