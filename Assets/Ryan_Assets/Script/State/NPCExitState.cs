using UnityEngine;
public class NPCExitState : INPCState
{
    public void EnterState(NPC npc)
    {
        npc.EndDialogue();
        Debug.Log("NPC is exiting dialogue."); //when player enters exit state end the dialogue
    }

    public void UpdateState(NPC npc)
    {
        npc.TransitionToState(new NPCIdleState()); // return to idle after exit tasks
    }

    public void OnPlayerEnter(NPC npc)
    {
        npc.TransitionToState(new NPCEngageState()); //if in exit state and player enters the range again go to engage state   
    }

    public void OnPlayerExit(NPC npc)
    {
        //do nothing this state already handles the exit 
    }
}
