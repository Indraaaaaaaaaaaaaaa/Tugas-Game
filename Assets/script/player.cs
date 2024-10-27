using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Kecepatan gerak karakter secara horizontal
    public float speed = 5f;
    // Kekuatan untuk melompat
    public float jumpForce = 10f;
    // Menandakan apakah karakter bisa melompat (true jika di tanah)
    private bool canJump = true;
    // Referensi ke komponen Rigidbody2D untuk interaksi fisika
    private Rigidbody2D body;
    // Referensi ke komponen Animator untuk mengatur animasi
    private Animator anim;

    // Start dipanggil sebelum frame pertama
    void Start()
    {
        // Menginisialisasi komponen Rigidbody2D
        body = GetComponent<Rigidbody2D>();
        // Menginisialisasi komponen Animator
        anim = GetComponent<Animator>();
    }

    // Update dipanggil sekali per frame
    void Update()
    {
        // Mendapatkan input horizontal (-1 untuk kiri, 1 untuk kanan)
        float horizontalInput = Input.GetAxis("Horizontal");
        // Mengatur kecepatan horizontal berdasarkan input dan kecepatan
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // Jika bergerak ke kanan, atur skala karakter menghadap ke kanan
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        // Jika bergerak ke kiri, atur skala karakter menghadap ke kiri
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);

        // Memeriksa jika tombol Space ditekan dan karakter bisa melompat
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            // Mengatur kecepatan vertikal untuk membuat karakter melompat
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            // Mengubah canJump menjadi false setelah melompat
            canJump = false;
        }

        // Mengatur animasi 'lari' saat ada input horizontal
        anim.SetBool("lari", horizontalInput != 0);
    }

    // Dipanggil saat karakter menyentuh objek lain
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Jika menyentuh objek dengan tag "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Reset canJump menjadi true saat menyentuh tanah
            canJump = true;
        }
    }
}
