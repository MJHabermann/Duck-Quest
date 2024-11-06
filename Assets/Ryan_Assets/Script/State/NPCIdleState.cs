using UnityEngine;
public class NPCIdleState : INPCState
{
    public void EnterState(NPC npc)
    {
        npc.dialogueActive = false;
        Debug.Log("NPC is idle.");
    }

    public void UpdateState(NPC npc)
    {
        // Idle behavior, can be expanded as needed
    }

    public void OnPlayerEnter(NPC npc)
    {
        npc.TransitionToState(new NPCEngageState());
    }

    public void OnPlayerExit(NPC npc)
    {
        // No action needed in idle state on exit
    }
}
