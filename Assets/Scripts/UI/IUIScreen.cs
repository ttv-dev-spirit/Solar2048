#nullable enable
namespace Solar2048.UI
{
    public interface IUIScreen
    {
        public bool IsShown { get; }
        public void Show();
        public void Hide();
    }
}