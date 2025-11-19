using MiniGameCollection.Games2025.Team00;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameCollection.Games2025.Team05
{
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField] public PlayerID PlayerID { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        [field: SerializeField] public float PlayerMoveSpeed { get; private set; } = 20f;
        [field: SerializeField] public ScoreKeeper ScoreKeeper { get; private set; }
        [field: SerializeField] public MiniGameManager GameManager { get; private set; }

        private bool isHitTimerActive = false;
        private float hitTimer = 0;
        private float hitTimerMax = 2.0f;

        private bool canMove = false;

        private SpriteRenderer[] spriteRenderers;

        private void Start()
        {
            // Get player spriterenderer and ship spriterenderer
            spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            // Get vertical and horizontal player input
            float axisY = ArcadeInput.Players[(int)PlayerID].AxisY;
            float axisX = ArcadeInput.Players[(int)PlayerID].AxisX;

            // Modify axis if recieving player 1 input
            if (PlayerID == PlayerID.Player1)
            {
                axisX = -axisX;
                axisY = -axisY;
            }

            // Change player position based on input
            Vector2 movement = new Vector2(axisX * Time.deltaTime * PlayerMoveSpeed, axisY * Time.deltaTime * PlayerMoveSpeed);
            Vector3 newPosition = transform.position + new Vector3(-movement.y, movement.x, 0);

            // Apply player input dependent on player id
            if (PlayerID == PlayerID.Player1)
            {
                newPosition.x = Mathf.Clamp(newPosition.x, -8.7f, -7.3f);
                newPosition.y = Mathf.Clamp(newPosition.y, 0.0f, 2.988448f);
            }
            else if (PlayerID == PlayerID.Player2)
            {
                newPosition.x = Mathf.Clamp(newPosition.x, 7.3f, 8.7f);
                newPosition.y = Mathf.Clamp(newPosition.y, -2.988448f, 0.0f);
            }

            if (canMove)
            {
                // Move rigidbody
                Rigidbody2D.MovePosition(newPosition);
            }

            // Handle hit timer
            HandleHitTimer();

            HandlePlayerScore();
        }

        private void HandlePlayerScore()
        {
            if (GameManager.State != MiniGameManagerState.TimerRunning)
                return;

            if (!canMove)
            {
                canMove = true;
            }

            ScoreKeeper.instance.AddScore(this.PlayerID, 1);
        }

        // Hit timer for flashing player sprite, as well as disabling player collision
        private void HandleHitTimer()
        {
            if (isHitTimerActive)
            {
                this.GetComponent<BoxCollider2D>().enabled = false;

                if (hitTimer > hitTimerMax)
                {
                    isHitTimerActive = false;
                }

                hitTimer += Time.deltaTime;

                if (hitTimer % 1f < 0.5f)
                {
                    SetSpriteRenderersEnabled(false);
                }
                else if (hitTimer % 1f > 0.5f)
                {
                    SetSpriteRenderersEnabled(true);
                }
            }
            else
            {
                // Check first sprite renderer for enabled var, since all sprite renderers should be toggled together
                if (spriteRenderers[0].enabled != true)
                    SetSpriteRenderersEnabled(true);

                this.GetComponent<BoxCollider2D>().enabled = true;
                hitTimer = 0;
            }
        }

        // Set all sprite renderers to whatever setting needed
        private void SetSpriteRenderersEnabled(bool desiredSetting)
        {
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].enabled = desiredSetting;
            }
        }

        // Validation method
        private void OnValidate()
        {
            if (Rigidbody2D == null)
            {
                Rigidbody2D = GetComponent<Rigidbody2D>();
            }
        }

        // Check for player-obstacle collision
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<TagObstacle>() != null)
            {
                Component component = collision.gameObject.GetComponent<TagObstacle>();

                if (!isHitTimerActive)
                    isHitTimerActive = true;

                ScoreKeeper.instance.RemoveScore(this.PlayerID, 1000);
            }

            if (collision.gameObject.GetComponent<TagScorebar>() != null)
            {
                Component component = collision.gameObject.GetComponent<TagScorebar>();
                ScoreKeeper.AddScore(this.PlayerID, 500);

            }
        }
    }
}
