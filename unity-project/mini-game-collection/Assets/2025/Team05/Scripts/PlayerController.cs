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

        [field: SerializeField] public float PlayerMoveSpeed { get; private set; } = 10f;


        // Update is called once per frame
        void Update()
        {
            float axisY = ArcadeInput.Players[(int)PlayerID].AxisY;
            float axisX = ArcadeInput.Players[(int)PlayerID].AxisX;

            if (PlayerID == PlayerID.Player1)
            {
                axisX = -axisX;
                axisY = -axisY;
            }

            Vector2 movement = new Vector2(axisX * Time.deltaTime * PlayerMoveSpeed, axisY * Time.deltaTime * PlayerMoveSpeed);
            
            Vector3 newPosition = transform.position + new Vector3(-movement.y, movement.x, 0);
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

                Rigidbody2D.MovePosition(newPosition);
        }

        private void OnValidate()
        {
            if (Rigidbody2D == null)
            {
                Rigidbody2D = GetComponent<Rigidbody2D>();
            }
        }
    }
}
