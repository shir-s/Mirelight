namespace BossLevel.Core.Managers
{
    public class MirelightCoreManager
    {
        public static MirelightCoreManager Instance { get; private set; }

        public MirelightCoreManager()
        {
            Instance = this;
            // Initialize any core managers or systems here
        }
    }
}
