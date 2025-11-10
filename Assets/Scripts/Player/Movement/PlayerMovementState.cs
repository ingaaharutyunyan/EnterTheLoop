using UnityEngine;
using System;
using DG.Tweening;

namespace Movement
{
    public class IdleState : State
    {
        private Rigidbody2D rb;

        public IdleState(Rigidbody2D rb)
        {
            this.rb = rb;
        }

        public override void OnEnter()
        {
            // Stop movement immediately
            rb.linearVelocity = Vector2.zero;
        }

        public override State OnUpdate(PlayerInputHandler PlayerInputHandler)
        {
            if (PlayerInputHandler.MoveInput != 0f && PlayerInputHandler.MovementAllowed)
            {
                return new WalkState(rb);
            }
            return null;
        }

        public override void OnExit()
        {
        }
    }
        public class WalkState : State
    {
        private Rigidbody2D rb;
        private float moveSpeed = 5f;
        private Transform characterTransform;
        private Tween ambleTween;

        public WalkState(Rigidbody2D rb)
        {
            this.rb = rb;
            this.characterTransform = rb.transform;
        }

        public override void OnEnter()
        {
            // Ambling rotation tween (like a chess piece rocking)
            ambleTween = characterTransform
                .DORotate(new Vector3(0, 0, 10f), 0.1f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        public override State OnUpdate(PlayerInputHandler PlayerInputHandler)
        {
            if (!PlayerInputHandler.MovementAllowed)
            {
                rb.linearVelocity = Vector2.zero;
                return new IdleState(rb);
            }

            float moveInput = PlayerInputHandler.MoveInput;
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

            if (Mathf.Abs(moveInput) <= 0.01f)
            {
                return new IdleState(rb);
            }

            return null;
        }

        public override void OnExit()
        {
            ambleTween.Kill();
            characterTransform.rotation = Quaternion.identity; // reset rotation
        }
    }
}
