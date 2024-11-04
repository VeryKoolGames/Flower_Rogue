using System;
using DefaultNamespace.Events;
using DG.Tweening;
using Events;
using Events.PlayerMoveEvents;
using KBCore.Refs;
using Player;
using ScriptableObjectScripts;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PetalAttacks
{
    public abstract class PlayerMove : MonoBehaviour
    {
        public int placeInHand;
        public PetalSO PetalSo;
        public OnPetalDeathEvent onPetalDeathEvent;
        public OnCommandCreationEvent onCommandCreationEvent;
        public OnTurnEndListener onRedrawTurnListener;
        public OnTurnEndListener onCombatStartListener;
        public OnDrawPetalEvent onDrawPetalEvent;
        public bool isRedrawEnabled = false;
        public PetalBoostsManager petalBoostsManager;

        protected virtual void Awake()
        {
            onCombatStartListener.Response.AddListener(OnCombatStart);
            onRedrawTurnListener.Response.AddListener(OnRedrawTurnEnd);
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 0.25f);
            petalBoostsManager = GetComponent<PetalBoostsManager>();
            // SetRandomColorToSpriteTexture();
        }
        
        private void SetRandomColorToSpriteTexture()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null || spriteRenderer.sprite == null)
            {
                Debug.LogWarning("SpriteRenderer or Sprite is missing.");
                return;
            }

            Texture2D originalTexture = spriteRenderer.sprite.texture;
            Texture2D newTexture = new Texture2D(originalTexture.width, originalTexture.height);
            newTexture.filterMode = originalTexture.filterMode;
            newTexture.wrapMode = originalTexture.wrapMode;

            newTexture.SetPixels(originalTexture.GetPixels());
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            // Change each pixel color to a random color in the new texture
            Color[] pixels = newTexture.GetPixels();
            for (int i = 0; i < pixels.Length; i++)
            {
                if (pixels[i].a == 0) continue;
                pixels[i] = randomColor;
            }

            // Apply the changes to the new texture
            newTexture.SetPixels(pixels);
            newTexture.Apply();

            // Assign the new texture to the sprite without affecting the original
            spriteRenderer.sprite = Sprite.Create(newTexture, spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f));
        }



        private void OnCombatStart()
        {
            isRedrawEnabled = true;
            gameObject.GetComponent<PetalDrag>().enabled = false;
        }
        
        private void OnRedrawTurnEnd()
        {
            isRedrawEnabled = false;
            gameObject.GetComponent<PetalDrag>().enabled = true;
        }
        
        private void OnDisable()
        {
            onCombatStartListener.Response.RemoveListener(OnCombatStart);
            onRedrawTurnListener.Response.RemoveListener(OnRedrawTurnEnd);
        }
        
    }

    public abstract class PlayerAttackMove : PlayerMove
    {
        public int passiveValue;
        public int activeValue;
        public int boostCount = 0;
        public PetalBoostUI petalBoostUI;
        
        protected new virtual void Awake()
        {
            base.Awake();
            passiveValue = PetalSo.petalAttributes.passiveValue;
            activeValue = PetalSo.petalAttributes.activeValue;
        }
    }
    
    public abstract class PlayerBoostMove : PlayerMove
    {
        public PetalBoostSO petalBoostSo;
        public bool boostLeft = true;
        public bool boostRight = true;
        public virtual void ApplyBoostEffect(PlayerMove target){}
    }
    
    public abstract class PlayerPassiveMove : PlayerMove
    {
        [SerializeField] protected OnTurnEndListener onTurnEndEventListener;
    }
}