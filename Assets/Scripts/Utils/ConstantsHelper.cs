using UnityEngine;

namespace FlappyBird.Utils
{
    // A static class to have a common references of the constant attribute values used around the game
    public static class ConstantsHelper
    {
        #region Player Attributes

        // added BASE prefix here as the game can be scaled later to have abilities/power ups that allow players
        // to have multipliers to these attribute 
        public const float BASE_MOVEMENT_SPEED = 2.5f;
        public const float BASE_IMPULSE_MAGNITUDE = 4f;
        
        #endregion

        #region Tags

        public const string BLOCKER_TAG = "Blocker";

        #endregion

        #region Scene Related

        public const int MAIN_MENU_SCENE_INDEX = 0;
        public const int GAME_SCENE_INDEX = 1;

        #endregion

        #region Level Related

        public const int NUMBER_OF_INITIAL_BLOCKERS = 10;
        public const int NUMBER_OF_INITIAL_PLATFORMS = 3;

        #endregion

    }
}
