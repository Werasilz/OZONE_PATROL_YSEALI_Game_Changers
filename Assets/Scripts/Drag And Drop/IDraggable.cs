public interface IDraggable
{
    bool IsReadyToDrag { get; }
    void OnStartDrag();
    void OnEndDrag();
}
