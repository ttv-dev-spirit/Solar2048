#nullable enable
namespace Solar2048
{
    public interface IActivatable
    {
        public bool IsActive { get; }
        public void Activate();
        public void Deactivate();
    }
}