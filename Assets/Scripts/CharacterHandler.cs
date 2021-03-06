﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UniAvatar
{
    public class CharacterHandler : AnimationTargetBase, IFlip, IPan, ITint, IJump, ISpriteChange
    {
        private Image m_targetImage;
        private Tween m_panTween;
        private Tween m_tintTween;

        [SerializeField]
        private Animator m_jumpAnimator;

        protected void Awake()
        {
            Init();
        }

        private void Init()
        {
            m_targetImage = transform.GetComponentInChildren<Image>();
        }

        public void Flip()
        {
            float currentFlip = transform.localScale.x;
            transform.localScale = new Vector3(currentFlip * -1, 1, 1);
        }

        public void Pan(float localValue, float time)
        {
            m_panTween = m_targetImage.transform.DOLocalMoveY(localValue, time);
            m_panTween.SetEase(Ease.OutSine);

            m_panTween.OnComplete(() => m_panTween = null);
            m_panTween.OnKill(() =>
            {
                m_panTween = null;
                // m_targetImage.transform.SetLocalPositionY(localValue);
            });
        }

        public void Tint(Color tintTarget, float time)
        {
            m_tintTween = m_targetImage.DOColor(tintTarget, time);
            m_tintTween.SetEase(Ease.OutQuad);

            m_tintTween.OnComplete(() => m_tintTween = null);
            m_tintTween.OnKill(() =>
            {
                m_tintTween = null;
                // m_targetImage.color = tintTarget;
            });
        }

        public void InterruptPan()
        {
            m_panTween?.Kill();
            m_panTween.OnKill(null);
        }

        public void InterruptTint()
        {
            m_tintTween?.Kill();
            m_tintTween.OnKill(null);
        }

        public void Jump()
        {
            m_jumpAnimator?.SetTrigger("Jump");
        }

        public void InterruptJump()
        {
            // Do nothing.
        }

        public void Change(Sprite sprite)
        {
            m_targetImage.sprite = sprite;
        }
    }
}