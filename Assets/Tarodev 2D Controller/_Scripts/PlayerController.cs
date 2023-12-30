using Assets.Scripts;
using CardboardCore.DI;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace TarodevController
{
    /// <summary>
    /// Hey!
    /// Tarodev here. I built this controller as there was a severe lack of quality & free 2D controllers out there.
    /// I have a premium version on Patreon, which has every feature you'd expect from a polished controller. Link: https://www.patreon.com/tarodev
    /// You can play and compete for best times here: https://tarodev.itch.io/extended-ultimate-2d-controller
    /// If you hve any questions or would like to brag about your score, come to discord: https://discord.gg/tarodev
    /// </summary>
    [Injectable]
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [Header("interactions")]
        public bool isInteracting = false;

        [Header("etc")]
        private int isPaused = 0;
        private Vector3 pausePos = Vector3.zero;
        private Vector3 pauseVel = Vector3.zero;
        public PlayerAnim playerAnimator;
        public SpriteRenderer sr;


        [Header("shit")]
        [SerializeField] private ScriptableStats _stats;
        public Rigidbody2D _rb;
        private CapsuleCollider2D _col;
        private FrameInput _frameInput;
        private Vector2 _frameVelocity;
        private bool _cachedQueryStartInColliders;

        // Dash-related variables
        private bool isDashing;
        private float lastClickTime;
        private float _time;
        private int dashCounter;
        private int dashCounterUp;

        #region Interface
        public Vector2 FrameInput => _frameInput.Move;
        public event Action<bool, float> GroundedChanged;
        public event Action Jumped;

        #endregion

        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<CapsuleCollider2D>();

            _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
        }

        public void PauseCharacter(int pauseLevel) {
            isPaused = pauseLevel;

            _frameInput.Move = Vector2.zero;
            _frameInput.JumpHeld = false;
            _frameInput.JumpDown = false;

            if (pauseLevel == 2) {
                pausePos = _rb.position;
                pauseVel = _rb.velocity;
                _rb.velocity = Vector3.zero;

                playerAnimator.animator.speed = 0;
            }
        }

        public void UnpauseCharacter() {
            if (isPaused == 2) {
                _rb.position = pausePos;
                _rb.velocity = pauseVel;
            }

            playerAnimator.animator.speed = 1;
            isPaused = 0;
        }

        private void Update() {
            if (isPaused > 0) {
                if (isPaused > 1) {
                    _rb.position = pausePos;
                    _rb.velocity = Vector3.zero;
                }

                return;
            }

            _time += Time.deltaTime;
            GatherInput();

            HandleDashInput();
        }

        private void GatherInput() {
            _frameInput = new FrameInput {
                JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
                JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
                Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
            };

            if (_stats.SnapInput) {
                _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
                _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
            }

            if (_frameInput.JumpDown) {
                _jumpToConsume = true;
                _timeJumpWasPressed = _time;
            }

            if (_frameInput.Move.x > 0) {
                sr.flipX = false;
            } else if (_frameInput.Move.x < 0) {
                sr.flipX = true;
            }
        }

        private void HandleDashInput()
        {
            // Dash mechanic
            bool rDown = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
            bool lDown = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
            bool rUp = Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow);
            bool lUp = Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow);
            int direction = rDown ? 1 : lDown ? -1 : 0;
            int directionUp = rUp ? 1 : lUp ? -1 : 0;

            if (!isDashing) {
                if (direction == 0) {
                    if (directionUp != 0) {
                        dashCounterUp = directionUp;
                    }
                } else {
                    if (dashCounter == direction && direction == dashCounterUp && Time.time - lastClickTime <= 0.2f) {
                        //dash
                        _frameVelocity += new Vector2(direction * _stats.DashPower, 0f);
                        isDashing = true;
                        playerAnimator.Dash();
                        StartCoroutine(Dash());
                    }

                    lastClickTime = Time.time;
                    dashCounter = direction;
                }
            }
        }

        IEnumerator Dash() {
            yield return new WaitForSeconds(_stats.DashCooldown);
            isDashing = false;
        }

        private void FixedUpdate() {
            if (isPaused > 1) {
                return;
            }

            CheckCollisions();

            HandleJump();
            HandleDirection();
            HandleGravity();
            ApplyMovement();
        }

        #region Collisions

        private float _frameLeftGrounded = float.MinValue;
        public bool _grounded;

        private void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;

            // Ground and Ceiling
            bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _stats.GrounderDistance, ~_stats.PlayerLayer);
            bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.GrounderDistance, ~_stats.PlayerLayer);

            // Hit a Ceiling
            if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

            // Landed on the Ground
            if (!_grounded && groundHit)
            {
                _grounded = true;
                _coyoteUsable = true;
                _bufferedJumpUsable = true;
                _endedJumpEarly = false;
                GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
            }
            // Left the Ground
            else if (_grounded && !groundHit)
            {
                _grounded = false;
                _frameLeftGrounded = _time;
                GroundedChanged?.Invoke(false, 0);
            }

            Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
        }

        #endregion


        #region Jumping

        private bool _jumpToConsume;
        private bool _bufferedJumpUsable;
        private bool _endedJumpEarly;
        private bool _coyoteUsable;
        private float _timeJumpWasPressed;

        private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
        private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

        private void HandleJump()
        {
            if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

            if (!_jumpToConsume && !HasBufferedJump) return;

            if (_grounded || CanUseCoyote) ExecuteJump();

            _jumpToConsume = false;
        }

        private void ExecuteJump()
        {
            _endedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            _frameVelocity.y = _stats.JumpPower;
            playerAnimator.Jump();
            Jumped?.Invoke();
        }

        #endregion

        #region Horizontal

        private void HandleDirection()
        {
            if (_frameInput.Move.x == 0)
            {
                var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
            }
        }

        #endregion

        #region Gravity

        private void HandleGravity()
        {
            if (_grounded && _frameVelocity.y <= 0f)
            {
                _frameVelocity.y = _stats.GroundingForce;
            }
            else
            {
                var inAirGravity = _stats.FallAcceleration;
                if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            }
        }

        #endregion

        private void ApplyMovement() => _rb.velocity = _frameVelocity;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_stats == null) Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
        }
#endif
    }

    public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
    }

    public interface IPlayerController
    {
        public event Action<bool, float> GroundedChanged;

        public event Action Jumped;
        public Vector2 FrameInput { get; }
    }
}