using UnityEngine;
public class NPCIdleState : INPCState
{
    public void EnterState(NPC npc)
    {
        npc.dialogueActive = false;
        Debug.Log("NPC is idle."); // if in idle state do nothing
    }

    public void UpdateState(NPC npc)
    {
        //do nothing
    }

    public void OnPlayerEnter(NPC npc)
    {
        npc.TransitionToState(new NPCEngageState()); //transistion to engage state when play enters radius
    }

    public void OnPlayerExit(NPC npc)
    {
        //do nothing
    }
}
