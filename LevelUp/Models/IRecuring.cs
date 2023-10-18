using LevelUp.DataAccess;

namespace LevelUp.Models
{
    public interface IRecuring
    {
        void Reset(LevelUpContext? context);
    }
}
