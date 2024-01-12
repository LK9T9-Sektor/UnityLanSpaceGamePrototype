namespace Assets.Scripts.Session
{
    public class LevelData
    {
        private int _number = 0;
        public int LevelNumber
        { 
            get
            {
                return _number;
            }
            private set
            {
                _number = value;
            }
        }

        private int _enemies = 0;
        public int Enemies
        {
            get
            {
                return _enemies;
            }
            private set
            {
                _enemies = value;
            }
        }

        public void StartNewLevel(int enemies)
        {
            _number++;
            _enemies = enemies;
        }

    }
}
