using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C: MonoBehaviour
{
    public const float SafeAreaTopShiftValue = 100f;
    public const float SafeAreaBotShiftValue = 100f;
    public const float Unit = 100f;
    public const float HalfUnit = 50f;
    public const float ButtonPanelHeight = 400f;
    public const float InfoPanelHeight = 200f;
    public const int RoomIDBoss = 1000;
    public const int SplashSceneIndex = 0;
    public const int MainMenuSceneIndex = 1;
    public const int LevelSelectionSceneIndex = 2;
    public const int Level1SceneIndex = 3;
    public const int Level2SceneIndex = 4;
    public const int Level3SceneIndex = 5;
    public const int SettingsSceneIndex = 6;
    public const int IntroSceneIndex = 7;
    public const int Level0SceneIndex = 8;
    public const int DeathSceneIndex = 9;
    public const int LevelModeSceneIndex = 10;
    public const int RecordsSceneIndex = 11;
    public const int OutroSceneIndex = 12;
    public const int StoreSceneIndex = 13;
    public const float DefaulTimeScale = 1f;
    public const float SlowMotionTimeScale = .5f;

    // Tags
    public const string PlayerTag = "Player";
    public const string BlockTag = "Block";
    public const string CaveBlockTag = "CaveBlock";
    public const string RoomTag = "Room";
    public const string InvulnirableEnemyHitBoxTag = "InvulnirableEnemyHitBox";
    public const string BootsTag = "Boots";
    public const string BodyTag = "Body";
    public const string SwordTag = "Sword";
    public const string SpikesTag = "Spikes";
    public const string EnemyHitBoxTag = "EnemyHitBox";
    public const string EnemyTag = "Enemy";
    public const string ButtonUITag = "ButtonUI";
    public const string DragonTag = "Dragon";
    public const string ProjectileTag = "Projectile";

    // Layers
    public const string BlockLayer = "Block";

    // Shop
    public const string SHOP_ITEM_SOLD_OUT_TEXT = "THIS ITEM IS NOT AVAILABLE";



    // META
    public const string PREFS_PRACTICE_MODE = "PRACTICE";               // int

    // PlayerPrefs
    public const string PREFS_CURRENT_HEALTH = "CURRENT_HEALTH";        // int
    public const string PREFS_MONEY = "MONEY";                          // int
    // PlayerPrefs Keys
    public const string PREFS_KEY_1_STATE = "KEY_1";                    // int
    public const string PREFS_KEY_2_STATE = "KEY_2";                    // int
    public const string PREFS_KEY_3_STATE = "KEY_3";                    // int
    // PlayerPrefs Items
    public const string PREFS_BOOTS_COUNT = "BOOTS_COUNT";              // int
    public const string PREFS_DEPOSIT_COUNT = "DEPOSIT_COUNT";          // int
    public const string PREFS_SLOWMOS_COUNT = "SLOWMOS_COUNT";          // int
    public const string PREFS_SHIELDS_COUNT = "SHIELDS_COUNT";          // int
    // PlayerPrefs Stages
    public const string PREFS_STAGE_2_OPENED = "STAGE_2";               // int
    public const string PREFS_STAGE_3_OPENED = "STAGE_3";               // int
    public const string PREFS_GAME_COMPLETED = "GAME_COMPLETED";        // int
    // PlayerPrefs Settings
    public const string PREFS_MUSIC = "MUSIC";                          // int
    public const string PREFS_SOUNDS = "SOUNDS";                        // int
    public const string PREFS_FIRST_LAUNCH = "FIRST_LAUNCH";            // int

    // PlayerPrefs Records
    public const string PREFS_TUTORIAL_BEST_TIME = "TUTORIAL_BEST_TIME";        //float
    public const string PREFS_ENTRANCE_BEST_TIME = "ENTRANCE_BEST_TIME";        //float
    public const string PREFS_DEPTHS_BEST_TIME = "DEPTHS_BEST_TIME";            //float
    public const string PREFS_OCEAN_BED_BEST_TIME = "OCEAN_BED_BEST_TIME";      //float
    public const string PREFS_DEATH_COUNT = "DEATH_COUNT";                      //int
    public const string PREFS_SOULS_COLLECTED = "SOULS_COLLECTED";              //int
    public const string PREFS_MONSTERS_KILLED = "MONSTERS_KILLED";              //int

    public const string PREFS_ROOM_UNLOCKED = "ROOM_UNLOCKED";                //int


}


