using UnityEngine;

[CreateAssetMenu(fileName = "SlidingMission", menuName = "Mission/Sliding", order = 1)]
public class SlidingMission : MissionBase
{
    float m_PreviousWorldDist;

    public override string GetMissionDesc()
    {
        return $"Slide for {missionObjectives.GetCurrentObjective().Objective}m";
    }
    
    public override void RunStart(TrackManager manager)
    {
        m_PreviousWorldDist = manager.worldDistance;
    }

    public override void UpdateMission(TrackManager manager)
    {
        if (manager.characterController.isSliding)
        {
            float dist = manager.worldDistance - m_PreviousWorldDist;
            missionObjectives.AdvanceObjective((int)dist);
        }

        m_PreviousWorldDist = manager.worldDistance;
    }
}