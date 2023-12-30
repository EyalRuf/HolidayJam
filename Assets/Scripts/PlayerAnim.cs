using System.Collections;
using TarodevController;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerAnim : MonoBehaviour {
        [Header("Ref")]
        public Animator animator;
        public PlayerController player;

        [Header("AnimParams")]
        public float goingUpMin = 2f;
        public float goingDownMin = -0.5f;
        public bool isRunning;

        // Update is called once per frame
        void Update() {
            isRunning = player._grounded && Mathf.Abs(player._rb.velocity.x) > 0;
            animator.SetBool("isRunning", isRunning);
            animator.SetBool("isGrounded", player._grounded);

            animator.SetBool("goingUp", player._rb.velocity.y > goingUpMin);
            animator.SetBool("goingDown", player._rb.velocity.y < goingDownMin);
        }

        public void Jump() {
            animator.SetTrigger("jumpTrigger");
            StartCoroutine(ResetJump());
        }

        IEnumerator ResetJump() {
            yield return new WaitForSeconds(0.1f);
            animator.ResetTrigger("jumpTrigger");
        }

        public void Dash () {
            animator.SetTrigger("dashTrigger");
            StartCoroutine(ResetDash());
        }

        IEnumerator ResetDash () {
            yield return new WaitForSeconds(0.1f);
            animator.ResetTrigger("dashTrigger");
        }
    }
}