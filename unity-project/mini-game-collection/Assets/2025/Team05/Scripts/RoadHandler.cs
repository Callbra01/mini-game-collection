using UnityEngine;

namespace MiniGameCollection.Games2025.Team05
{
    public class RoadMove : MonoBehaviour
    {
        [field: SerializeField] public MiniGameManager GameManager { get; private set; }

        public bool movingRight = false;
        public float moveSpeed = 10.0f;
        public float resetOffset = 12.58f;

        void Update()
        {
            HandleMove();
        }

        void HandleMove()
        {
            if (GameManager.State != MiniGameManagerState.TimerRunning)
                return;

            if (this.transform.position.y >= resetOffset)
                this.transform.position = Vector3.zero;

            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + moveSpeed * Time.deltaTime, this.transform.position.z);

        }
    }
}
