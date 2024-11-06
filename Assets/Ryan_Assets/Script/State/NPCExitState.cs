using UnityEngine;
public class NPCExitState : INPCState
{
    public void EnterState(NPC npc)
    {
        npc.EndDialogue();
        Debug.Log("NPC is exiting dialogue.");
    }

    public void UpdateState(NPC npc)
    {
        npc.TransitionToState(new NPCIdleState()); // Return to idle after exit tasks
    }

    public void OnPlayerEnter(NPC npc)
    {
        npc.TransitionToState(new NPCEngageState());
    }

    public void OnPlayerExit(NPC npc)
    {
        // No action needed here
    }
}
