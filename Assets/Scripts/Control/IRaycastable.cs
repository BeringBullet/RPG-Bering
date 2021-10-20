namespace RPG.Control
{
    public interface IRaycastable
    {
        CursorType GetCursorType();
        bool HangleRaycast(PlayerController callingControler);
    }
}
