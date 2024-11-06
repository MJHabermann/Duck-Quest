public interface INPCState
{
    void EnterState(NPC npc);
    void UpdateState(NPC npc);
    void OnPlayerEnter(NPC npc);
    void OnPlayerExit(NPC npc);
}

