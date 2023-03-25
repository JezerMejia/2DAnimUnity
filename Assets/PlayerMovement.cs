using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movimiento")]
    private float horizontalMov = 0f;
    private float verticalMov = 0f;
    [SerializeField] private float movementSpeed;
    [Range(0, 0.3f)] [SerializeField] private float suavizadoDeMovimiento;
    private Vector3 velocity = Vector3.zero;

    [Header ("Animacion")]
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      horizontalMov = Input.GetAxisRaw("Horizontal") * movementSpeed;
      verticalMov = Input.GetAxisRaw("Vertical") * movementSpeed;

      animator.SetFloat("horizontal", Mathf.Abs(horizontalMov) + Mathf.Abs(verticalMov));
    }

    private void FixedUpdate() {
        Move(horizontalMov * Time.fixedDeltaTime, verticalMov * Time.fixedDeltaTime);
    }

    private void Move(float horizontal, float vertical) {
        Vector3 targetVelocity = new Vector2(horizontalMov, verticalMov);
        velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref velocity, suavizadoDeMovimiento);

        Vector3 scale = transform.localScale;
        if (horizontalMov > 0) {
            scale.x = - Mathf.Abs(scale.x);
        } else if (horizontalMov < 0) {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
        transform.Translate(velocity);
    }
}
