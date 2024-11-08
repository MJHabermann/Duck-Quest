using UnityEngine;
public class NPCEngageState : INPCState
{
    public void EnterState(NPC npc)
    {
        npc.StartDialogue();
        Debug.Log("NPC is engaging in dialogue."); //when engage state is activated start the dialogue
    }

    public void UpdateState(NPC npc)
    {
        //do nothing
    }

    public void OnPlayerEnter(NPC npc)
    {
       //do nothing
    }

    public void OnPlayerExit(NPC npc)
    {
        npc.TransitionToState(new NPCExitState()); // when player exits transistion to new state
    }
}
