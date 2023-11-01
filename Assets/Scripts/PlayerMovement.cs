using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Controller
{
    public class PlayerMovement : MonoBehaviour
    {
        public int health = 100;
        private float horizontal;
        public float speed = 8f;
        public float jumpingPower = 16f;
        float timer = 3f;
        public float knockStr, knockDelay;

        public UnityEvent OnBegin, OnDone;

        [SerializeField] private Slider playerHealthBar;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Animator anim;

        void Update()
        {
            
            //Moving code
            horizontal = Input.GetAxisRaw("Horizontal");

            //Moving animation
            if (horizontal != 0)
            {
                anim.SetBool("isRunning", true);
                var newScale = transform.localScale;
                newScale.x = (horizontal > 0) ? 2 : -2;
                transform.localScale = newScale;
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
            //int random = Random.Range(0, 2) ;

            //Jump Check
            //
            if (Input.GetButtonDown("Jump")&& IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                anim.SetTrigger("jump");
                timer = 3f;
            }
            timer -= Time.deltaTime;
            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
            if(health <= 0)
            {
                Destroy(gameObject);
               
            }
            playerHealthBar.value = health;
            //Flip();
            //rb.AddForce(new Vector2(1, 0) * knockStr, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            //Position update
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }

        private bool IsGrounded()
        {
            //Ground check
            return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        }
        public void knock(Vector2 dir)
        {
            Debug.Log("dir" + dir);
            enabled = false;
            StopAllCoroutines();
            OnBegin?.Invoke();
            rb.AddForce(dir * knockStr, ForceMode2D.Impulse);   
            StartCoroutine(Reset());
        }
        private IEnumerator Reset()
        {
            yield return new WaitForSeconds(knockDelay);
            rb.velocity = Vector3.zero;
            enabled = true;
            OnDone?.Invoke();   
        }
        /*private void Flip()
        {
            if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }*/
    }

}
