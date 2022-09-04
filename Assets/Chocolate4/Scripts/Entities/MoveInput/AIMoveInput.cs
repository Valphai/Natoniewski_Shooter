using UnityEngine;

namespace Chocolate4.Entities.MoveInput
{
    public class AIMoveInput : IMoveInput
    {
        public Vector3 Translation { get; set; }
        private Transform playerTr, thisTr;
        private float chaseRange;

        public AIMoveInput(
            Transform playerTr,
            Transform thisTr, float chaseRange
        )
        {
            this.playerTr = playerTr;
            this.thisTr = thisTr;
            this.chaseRange = chaseRange;
        }
        public void ReadMoveInput(float moveSpeed)
        {
            Translation = IsInRange() ? 
                playerTr.position : thisTr.position;
        }
        public bool IsInRange()
        {
            float distanceToPlayer = 
                Vector3.Distance(playerTr.position, thisTr.position);
            
            return distanceToPlayer <= chaseRange;
        }
    }
}