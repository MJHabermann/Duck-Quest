using UnityEngine;
public class NPCEngageState : INPCState
{
    public void EnterState(NPC npc)
    {
        npc.StartDialogue();
        Debug.Log("NPC is engaging in dialogue.");
    }

    public void UpdateState(NPC npc)
    {
        // Any ongoing behavior while engaging
    }

    public void OnPlayerEnter(NPC npc)
    {
        // Already engaging, so no action needed
    }

    public void OnPlayerExit(NPC npc)
    {
        npc.TransitionToState(new NPCExitState());
    }
}
