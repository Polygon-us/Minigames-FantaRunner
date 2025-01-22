using UnityEngine;

[CreateAssetMenu(fileName = "BarrierJumpMission", menuName = "Mission/BarrierJump", order = 1)]
public class BarrierJumpMission : MissionBase
{
    protected const int k_HitColliderCount = 8;
    protected readonly Vector3 k_CharacterColliderSizeOffset = new Vector3(-0.3f, 2f, -0.3f);
   
    private Obstacle m_Previous;
    private Collider[] m_Hits;
    
    public override string GetMissionDesc()
    {
        return $"Jump over {missionObjectives.GetCurrentObjective().Objective} barriers";
    }

    public override void RunStart(TrackManager manager)
    {
        m_Previous = null;
        m_Hits = new Collider[k_HitColliderCount];
    }

    public override void UpdateMission(TrackManager manager)
    {
        if (!manager.characterController.isJumping)
            return;
        
        Vector3 boxSize = manager.characterController.characterCollider.collider.size + k_CharacterColliderSizeOffset;
        Vector3 boxCenter = manager.characterController.transform.position - Vector3.up * boxSize.y * 0.5f;

        int count = Physics.OverlapBoxNonAlloc(boxCenter, boxSize * 0.5f, m_Hits);

        for (int i = 0; i < count; ++i)
        {
            Obstacle obs = m_Hits[i].GetComponent<Obstacle>();

            if (obs != null && obs is AllLaneObstacle)
            {
                if (obs != m_Previous)
                {
                    missionObjectives.AdvanceObjective(1);
                }

                m_Previous = obs;
            }
        }
    }
}