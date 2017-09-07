namespace MasujimaRyohei
{
    using UnityEngine;


    public class Tags
    {
        // Default tags
        public const string UNTAGGED = "Untagged";
        public const string RESPAWN = "Respawn";
        public const string FINISH = "Finish";
        public const string EDITER_ONLY = "EditerOnly";
        public const string MAIN_CAMERA = "MainCamera";
        public const string PLAYER = "Player";
        public const string GAME_CONTROLLER = "GameController";
        public const string GAME_CLEAR = "GameClear";
        public const string GAME_OVER = "GameOver";
        public const string ENEMY = "Enemy";

        // Original tags
        public const string BUTTON = "Button";
    }

    public class Layers
    {
        public const string BACKGROUND = "Background";
    }

    public class SortingLayers
    {
        public const string UI = "UI";
        public const string ENEMY = "Enemy";
        public const string BACKGROUND = "Background";

    }

    public class Scenes
    {
        public const string LOGO = "Logo";
        public const string TITLE = "Title";
        public const string LOAD = "Load";
        public const string DEMO = "Demo";
        public const string MAIN = "Main";
        public const string RESULT = "Result";
        public const string CLEAR = "Clear";
    }

    public enum MARKS
    {
        ZERO,
        POSITIVE,
        NEGATIVE
    }
    public class Direction
    {
        public enum ENUM
        {
            UPPER,
            UPPER_RIGHT,
            RIGHT,
            LOWER_RIGHT,
            LOWER,
            LOWER_LEFT,
            LEFT,
            UPPER_LEFT,
            DIRECTION_NUM,
        }

        public enum COMPASS
        {
            NORTH,
            NORTH_EAST,
            EAST,
            SOUTH_EAST,
            SOUTH,
            SOUTH_WEST,
            WEST,
            NORTH_WEST
        }
        public enum HORIZONTAL
        {
            LEFT = 2,
            RIGHT
        }
        public enum AXIS
        {
            X,Y,Z
                       }
    }
    public class BGM
    {
        public const string PATH = "Audio/BGM";
        public const string VOLUME = "BGM_VOLUME_KEY";

        public class DEFAULT
        {
            public const float VOLUME = 1.0f;
        }
    }
    public class SE
    {
        public const string PATH = "Audio/SE";
        public const string VOLUME = "SE_VOLUME_KEY";
        public class Title
        {
            //public const string Kick = "Kick";
        }
        public class DEFAULT
        {
            public const float VOLUME = 1.0f;
        }
    }
    public class ASSETS
    {
        public class PREFABS
        {
            public class MANAGERS
            {
                public const string ABSOLUTE_PATH = "Assets/Resources/Prefabs/Managers/";
                public const string AUTO_PUT = "Assets/Resources/Prefabs/Managers/AutomaticPutting/";
                public const string PREFAB_EXTENSION = ".prefab";
                public const string AUDIO_PATH = ABSOLUTE_PATH + "AutomaticPutting/AudioManager.prefab";
                public const string FADE_PATH = ABSOLUTE_PATH + "AutomaticPutting/FadeManager.prefab";
                public const string TIME_PATH = ABSOLUTE_PATH + "AutomaticPutting/TimeManager.prefab";
            }
        }
    }
    public class MANAGERS
    {
        public class NAME
        {
            // Those name same with script name. 
            public const string AUDIO_MANAGER = "AudioManager";
            public const string FADE_MANAGER = "FadeManager";
            public const string TIME_MANAGER = "TimeManager";
        }
    }
}