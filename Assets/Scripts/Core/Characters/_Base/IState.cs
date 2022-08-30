
using Core.Characters.Player;

namespace Core.Characters._Base
{
    public interface IState
    {
        void Start();
        void Update();
        void End();
    }
}