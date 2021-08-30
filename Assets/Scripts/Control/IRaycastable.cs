namespace RPG.Control
{
    public interface IRaycastable
    {
        CursorType GetCursorType();
        bool HangleRaycast(PlayerControler callingControler);
    }
}
