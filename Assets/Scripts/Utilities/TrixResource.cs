using Infrastructure;
using Models.Constants;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
public class TrixResource
{

public static TrixSprite Sprites { get; } = new TrixSprite();

public static TrixGameObject GameObjects { get; } = new TrixGameObject();

public static TrixAnimation Animations { get; } = new TrixAnimation();

public static TrixRuntimeAnimatorController RuntimeAnimatorControllers { get; } = new TrixRuntimeAnimatorController();

public static TrixAudioClip AudioClips { get; } = new TrixAudioClip();

public static TrixFont Fonts { get; } = new TrixFont();

public static TrixShader Shaders { get; } = new TrixShader();

public static TrixMaterial Materials { get; } = new TrixMaterial();


public class TrixSprite
{
private readonly Dictionary<string, string> _spriteEnumPathDictionary = new Dictionary<string, string>
{
{"WeaponsSprite_spr_Weapon01","Sprites/EnumWeapons/spr_Weapon01"},
{"WeaponsSprite_spr_Weapon02","Sprites/EnumWeapons/spr_Weapon02"},
{"WeaponsSprite_spr_Weapon03","Sprites/EnumWeapons/spr_Weapon03"},
{"WeaponsSprite_spr_Weapon04","Sprites/EnumWeapons/spr_Weapon04"},
{"WindDirectionSprite_spr_East","Sprites/EnumWindDirection/spr_East"},
{"WindDirectionSprite_spr_North","Sprites/EnumWindDirection/spr_North"},
{"WindDirectionSprite_spr_NorthEast","Sprites/EnumWindDirection/spr_NorthEast"},
{"WindDirectionSprite_spr_NorthWest","Sprites/EnumWindDirection/spr_NorthWest"},
{"WindDirectionSprite_spr_South","Sprites/EnumWindDirection/spr_South"},
{"WindDirectionSprite_spr_SouthEast","Sprites/EnumWindDirection/spr_SouthEast"},
{"WindDirectionSprite_spr_SouthWest","Sprites/EnumWindDirection/spr_SouthWest"},
{"WindDirectionSprite_spr_West","Sprites/EnumWindDirection/spr_West"},

};
public void GetByEnum(Enum enumValue, string path, Action<Sprite> callback)
{
if (string.Equals(enumValue.ToString(), StaticValues.UrlKey, StringComparison.OrdinalIgnoreCase))
{
ResourceManager.Instance.LoadResourceAsync(StaticValues.UrlKey, path, callback);
return;
}
var key = $"{enumValue.GetType().Name}_{enumValue}";
string pathValue;
if (_spriteEnumPathDictionary.TryGetValue(key, out pathValue))
{
ResourceManager.Instance.LoadResourceAsync(pathValue, string.Empty, callback);
return;
}
callback(null);
}

public Sprite spr_ActiveStar => ResourceManager.LoadResource<Sprite>("Sprites/spr_ActiveStar");
public void spr_ActiveStar_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/spr_ActiveStar", string.Empty, callback); 
public Sprite spr_DeactiveStar => ResourceManager.LoadResource<Sprite>("Sprites/spr_DeactiveStar");
public void spr_DeactiveStar_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/spr_DeactiveStar", string.Empty, callback); 
public Sprite spr_NoImage => ResourceManager.LoadResource<Sprite>("Sprites/spr_NoImage");
public void spr_NoImage_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/spr_NoImage", string.Empty, callback); 
public Sprite spr_Weapon01 => ResourceManager.LoadResource<Sprite>("Sprites/EnumWeapons/spr_Weapon01");
public void spr_Weapon01_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/EnumWeapons/spr_Weapon01", string.Empty, callback); 
public Sprite spr_Weapon02 => ResourceManager.LoadResource<Sprite>("Sprites/EnumWeapons/spr_Weapon02");
public void spr_Weapon02_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/EnumWeapons/spr_Weapon02", string.Empty, callback); 
public Sprite spr_Weapon03 => ResourceManager.LoadResource<Sprite>("Sprites/EnumWeapons/spr_Weapon03");
public void spr_Weapon03_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/EnumWeapons/spr_Weapon03", string.Empty, callback); 
public Sprite spr_Weapon04 => ResourceManager.LoadResource<Sprite>("Sprites/EnumWeapons/spr_Weapon04");
public void spr_Weapon04_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/EnumWeapons/spr_Weapon04", string.Empty, callback); 
public Sprite spr_East => ResourceManager.LoadResource<Sprite>("Sprites/EnumWindDirection/spr_East");
public void spr_East_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/EnumWindDirection/spr_East", string.Empty, callback); 
public Sprite spr_North => ResourceManager.LoadResource<Sprite>("Sprites/EnumWindDirection/spr_North");
public void spr_North_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/EnumWindDirection/spr_North", string.Empty, callback); 
public Sprite spr_NorthEast => ResourceManager.LoadResource<Sprite>("Sprites/EnumWindDirection/spr_NorthEast");
public void spr_NorthEast_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/EnumWindDirection/spr_NorthEast", string.Empty, callback); 
public Sprite spr_NorthWest => ResourceManager.LoadResource<Sprite>("Sprites/EnumWindDirection/spr_NorthWest");
public void spr_NorthWest_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/EnumWindDirection/spr_NorthWest", string.Empty, callback); 
public Sprite spr_South => ResourceManager.LoadResource<Sprite>("Sprites/EnumWindDirection/spr_South");
public void spr_South_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/EnumWindDirection/spr_South", string.Empty, callback); 
public Sprite spr_SouthEast => ResourceManager.LoadResource<Sprite>("Sprites/EnumWindDirection/spr_SouthEast");
public void spr_SouthEast_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/EnumWindDirection/spr_SouthEast", string.Empty, callback); 
public Sprite spr_SouthWest => ResourceManager.LoadResource<Sprite>("Sprites/EnumWindDirection/spr_SouthWest");
public void spr_SouthWest_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/EnumWindDirection/spr_SouthWest", string.Empty, callback); 
public Sprite spr_West => ResourceManager.LoadResource<Sprite>("Sprites/EnumWindDirection/spr_West");
public void spr_West_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/EnumWindDirection/spr_West", string.Empty, callback); 
public Sprite spr_BlueButton => ResourceManager.LoadResource<Sprite>("Sprites/Menu/spr_BlueButton");
public void spr_BlueButton_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/Menu/spr_BlueButton", string.Empty, callback); 
public Sprite spr_Connected => ResourceManager.LoadResource<Sprite>("Sprites/Menu/spr_Connected");
public void spr_Connected_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/Menu/spr_Connected", string.Empty, callback); 
public Sprite spr_DisabledMusic => ResourceManager.LoadResource<Sprite>("Sprites/Menu/spr_DisabledMusic");
public void spr_DisabledMusic_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/Menu/spr_DisabledMusic", string.Empty, callback); 
public Sprite spr_DisabledNotification => ResourceManager.LoadResource<Sprite>("Sprites/Menu/spr_DisabledNotification");
public void spr_DisabledNotification_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/Menu/spr_DisabledNotification", string.Empty, callback); 
public Sprite spr_DisabledSfx => ResourceManager.LoadResource<Sprite>("Sprites/Menu/spr_DisabledSfx");
public void spr_DisabledSfx_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/Menu/spr_DisabledSfx", string.Empty, callback); 
public Sprite spr_Disconnected => ResourceManager.LoadResource<Sprite>("Sprites/Menu/spr_Disconnected");
public void spr_Disconnected_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/Menu/spr_Disconnected", string.Empty, callback); 
public Sprite spr_EnabledMusic => ResourceManager.LoadResource<Sprite>("Sprites/Menu/spr_EnabledMusic");
public void spr_EnabledMusic_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/Menu/spr_EnabledMusic", string.Empty, callback); 
public Sprite spr_EnabledNotification => ResourceManager.LoadResource<Sprite>("Sprites/Menu/spr_EnabledNotification");
public void spr_EnabledNotification_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/Menu/spr_EnabledNotification", string.Empty, callback); 
public Sprite spr_EnabledSfx => ResourceManager.LoadResource<Sprite>("Sprites/Menu/spr_EnabledSfx");
public void spr_EnabledSfx_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/Menu/spr_EnabledSfx", string.Empty, callback); 
public Sprite spr_GrayButton => ResourceManager.LoadResource<Sprite>("Sprites/Menu/spr_GrayButton");
public void spr_GrayButton_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/Menu/spr_GrayButton", string.Empty, callback); 
public Sprite spr_OrangeButton => ResourceManager.LoadResource<Sprite>("Sprites/Menu/spr_OrangeButton");
public void spr_OrangeButton_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/Menu/spr_OrangeButton", string.Empty, callback); 
public Sprite spr_RedButton => ResourceManager.LoadResource<Sprite>("Sprites/Menu/spr_RedButton");
public void spr_RedButton_async(Action<Sprite> callback) => ResourceManager.Instance.LoadResourceAsync("Sprites/Menu/spr_RedButton", string.Empty, callback); 
}
public class TrixGameObject
{
private readonly Dictionary<string, string> _gameobjectEnumPathDictionary = new Dictionary<string, string>
{

};
public void GetByEnum(Enum enumValue, string path, Action<GameObject> callback)
{
if (string.Equals(enumValue.ToString(), StaticValues.UrlKey, StringComparison.OrdinalIgnoreCase))
{
ResourceManager.Instance.LoadResourceAsync(StaticValues.UrlKey, path, callback);
return;
}
var key = $"{enumValue.GetType().Name}_{enumValue}";
string pathValue;
if (_gameobjectEnumPathDictionary.TryGetValue(key, out pathValue))
{
ResourceManager.Instance.LoadResourceAsync(pathValue, string.Empty, callback);
return;
}
callback(null);
}

public GameObject ExitPopup => ResourceManager.LoadResource<GameObject>("GameObjects/pre_ExitPopup");
public void ExitPopup_async(Action<GameObject> callback) => ResourceManager.Instance.LoadResourceAsync("GameObjects/pre_ExitPopup", string.Empty, callback); 
public GameObject GoogleSaveNotificationPopup => ResourceManager.LoadResource<GameObject>("GameObjects/pre_GoogleSaveNotificationPopup");
public void GoogleSaveNotificationPopup_async(Action<GameObject> callback) => ResourceManager.Instance.LoadResourceAsync("GameObjects/pre_GoogleSaveNotificationPopup", string.Empty, callback); 
public GameObject InfoPopup => ResourceManager.LoadResource<GameObject>("GameObjects/pre_InfoPopup");
public void InfoPopup_async(Action<GameObject> callback) => ResourceManager.Instance.LoadResourceAsync("GameObjects/pre_InfoPopup", string.Empty, callback); 
public GameObject InGameNoOrangePopup => ResourceManager.LoadResource<GameObject>("GameObjects/pre_InGameNoOrangePopup");
public void InGameNoOrangePopup_async(Action<GameObject> callback) => ResourceManager.Instance.LoadResourceAsync("GameObjects/pre_InGameNoOrangePopup", string.Empty, callback); 
public GameObject InMenuNoOrangePopup => ResourceManager.LoadResource<GameObject>("GameObjects/pre_InMenuNoOrangePopup");
public void InMenuNoOrangePopup_async(Action<GameObject> callback) => ResourceManager.Instance.LoadResourceAsync("GameObjects/pre_InMenuNoOrangePopup", string.Empty, callback); 
public GameObject NoCoinPopup => ResourceManager.LoadResource<GameObject>("GameObjects/pre_NoCoinPopup");
public void NoCoinPopup_async(Action<GameObject> callback) => ResourceManager.Instance.LoadResourceAsync("GameObjects/pre_NoCoinPopup", string.Empty, callback); 
public GameObject SettingsPopup => ResourceManager.LoadResource<GameObject>("GameObjects/pre_SettingsPopup");
public void SettingsPopup_async(Action<GameObject> callback) => ResourceManager.Instance.LoadResourceAsync("GameObjects/pre_SettingsPopup", string.Empty, callback); 
public GameObject Chapter01 => ResourceManager.LoadResource<GameObject>("GameObjects/GameplayBackgrounds/pre_Chapter01");
public void Chapter01_async(Action<GameObject> callback) => ResourceManager.Instance.LoadResourceAsync("GameObjects/GameplayBackgrounds/pre_Chapter01", string.Empty, callback); 
public GameObject Chapter02 => ResourceManager.LoadResource<GameObject>("GameObjects/GameplayBackgrounds/pre_Chapter02");
public void Chapter02_async(Action<GameObject> callback) => ResourceManager.Instance.LoadResourceAsync("GameObjects/GameplayBackgrounds/pre_Chapter02", string.Empty, callback); 
public GameObject Chapter03 => ResourceManager.LoadResource<GameObject>("GameObjects/GameplayBackgrounds/pre_Chapter03");
public void Chapter03_async(Action<GameObject> callback) => ResourceManager.Instance.LoadResourceAsync("GameObjects/GameplayBackgrounds/pre_Chapter03", string.Empty, callback); 
public GameObject Chapter04 => ResourceManager.LoadResource<GameObject>("GameObjects/GameplayBackgrounds/pre_Chapter04");
public void Chapter04_async(Action<GameObject> callback) => ResourceManager.Instance.LoadResourceAsync("GameObjects/GameplayBackgrounds/pre_Chapter04", string.Empty, callback); 
public GameObject Chapter05 => ResourceManager.LoadResource<GameObject>("GameObjects/GameplayBackgrounds/pre_Chapter05");
public void Chapter05_async(Action<GameObject> callback) => ResourceManager.Instance.LoadResourceAsync("GameObjects/GameplayBackgrounds/pre_Chapter05", string.Empty, callback); 
}
public class TrixAnimation
{
private readonly Dictionary<string, string> _animationEnumPathDictionary = new Dictionary<string, string>
{

};
public void GetByEnum(Enum enumValue, string path, Action<Animation> callback)
{
if (string.Equals(enumValue.ToString(), StaticValues.UrlKey, StringComparison.OrdinalIgnoreCase))
{
ResourceManager.Instance.LoadResourceAsync(StaticValues.UrlKey, path, callback);
return;
}
var key = $"{enumValue.GetType().Name}_{enumValue}";
string pathValue;
if (_animationEnumPathDictionary.TryGetValue(key, out pathValue))
{
ResourceManager.Instance.LoadResourceAsync(pathValue, string.Empty, callback);
return;
}
callback(null);
}

}
public class TrixRuntimeAnimatorController
{
private readonly Dictionary<string, string> _runtimeanimatorcontrollerEnumPathDictionary = new Dictionary<string, string>
{

};
public void GetByEnum(Enum enumValue, string path, Action<RuntimeAnimatorController> callback)
{
if (string.Equals(enumValue.ToString(), StaticValues.UrlKey, StringComparison.OrdinalIgnoreCase))
{
ResourceManager.Instance.LoadResourceAsync(StaticValues.UrlKey, path, callback);
return;
}
var key = $"{enumValue.GetType().Name}_{enumValue}";
string pathValue;
if (_runtimeanimatorcontrollerEnumPathDictionary.TryGetValue(key, out pathValue))
{
ResourceManager.Instance.LoadResourceAsync(pathValue, string.Empty, callback);
return;
}
callback(null);
}

public RuntimeAnimatorController cancel_button => ResourceManager.LoadResource<RuntimeAnimatorController>("RuntimeAnimatorControllers/ctrl_cancel_button");
public void cancel_button_async(Action<RuntimeAnimatorController> callback) => ResourceManager.Instance.LoadResourceAsync("RuntimeAnimatorControllers/ctrl_cancel_button", string.Empty, callback); 
public RuntimeAnimatorController global_button => ResourceManager.LoadResource<RuntimeAnimatorController>("RuntimeAnimatorControllers/ctrl_global_button");
public void global_button_async(Action<RuntimeAnimatorController> callback) => ResourceManager.Instance.LoadResourceAsync("RuntimeAnimatorControllers/ctrl_global_button", string.Empty, callback); 
public RuntimeAnimatorController toggle_button => ResourceManager.LoadResource<RuntimeAnimatorController>("RuntimeAnimatorControllers/ctrl_toggle_button");
public void toggle_button_async(Action<RuntimeAnimatorController> callback) => ResourceManager.Instance.LoadResourceAsync("RuntimeAnimatorControllers/ctrl_toggle_button", string.Empty, callback); 
}
public class TrixAudioClip
{
private readonly Dictionary<string, string> _audioclipEnumPathDictionary = new Dictionary<string, string>
{

};
public void GetByEnum(Enum enumValue, string path, Action<AudioClip> callback)
{
if (string.Equals(enumValue.ToString(), StaticValues.UrlKey, StringComparison.OrdinalIgnoreCase))
{
ResourceManager.Instance.LoadResourceAsync(StaticValues.UrlKey, path, callback);
return;
}
var key = $"{enumValue.GetType().Name}_{enumValue}";
string pathValue;
if (_audioclipEnumPathDictionary.TryGetValue(key, out pathValue))
{
ResourceManager.Instance.LoadResourceAsync(pathValue, string.Empty, callback);
return;
}
callback(null);
}

public AudioClip Coin => ResourceManager.LoadResource<AudioClip>("AudioClips/sfx_Coin");
public void Coin_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/sfx_Coin", string.Empty, callback); 
public AudioClip EndChapter => ResourceManager.LoadResource<AudioClip>("AudioClips/sfx_EndChapter");
public void EndChapter_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/sfx_EndChapter", string.Empty, callback); 
public AudioClip LevelDescription => ResourceManager.LoadResource<AudioClip>("AudioClips/sfx_LevelDescription");
public void LevelDescription_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/sfx_LevelDescription", string.Empty, callback); 
public AudioClip LevelDescription2 => ResourceManager.LoadResource<AudioClip>("AudioClips/sfx_LevelDescription2");
public void LevelDescription2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/sfx_LevelDescription2", string.Empty, callback); 
public AudioClip Lose => ResourceManager.LoadResource<AudioClip>("AudioClips/sfx_Lose");
public void Lose_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/sfx_Lose", string.Empty, callback); 
public AudioClip Menu => ResourceManager.LoadResource<AudioClip>("AudioClips/sfx_Menu");
public void Menu_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/sfx_Menu", string.Empty, callback); 
public void spinner_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/sfx_spinner", string.Empty, callback); 
public void reward_spinner_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/sfx_reward_spinner", string.Empty, callback); 
public AudioClip Menu1 => ResourceManager.LoadResource<AudioClip>("AudioClips/sfx_Menu1");
public void Menu1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/sfx_Menu1", string.Empty, callback); 
public AudioClip Shoot => ResourceManager.LoadResource<AudioClip>("AudioClips/sfx_Shoot");
public void Shoot_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/sfx_Shoot", string.Empty, callback); 
public AudioClip Win => ResourceManager.LoadResource<AudioClip>("AudioClips/sfx_Win");
public void Win_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/sfx_Win", string.Empty, callback); 
public AudioClip birds1 => ResourceManager.LoadResource<AudioClip>("AudioClips/GameplayEnvironments/sfx_birds1");
public void birds1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/GameplayEnvironments/sfx_birds1", string.Empty, callback); 
public AudioClip birds2 => ResourceManager.LoadResource<AudioClip>("AudioClips/GameplayEnvironments/sfx_birds2");
public void birds2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/GameplayEnvironments/sfx_birds2", string.Empty, callback); 
public AudioClip birds3 => ResourceManager.LoadResource<AudioClip>("AudioClips/GameplayEnvironments/sfx_birds3");
public void birds3_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/GameplayEnvironments/sfx_birds3", string.Empty, callback); 
public AudioClip cars1 => ResourceManager.LoadResource<AudioClip>("AudioClips/GameplayEnvironments/sfx_cars1");
public void cars1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/GameplayEnvironments/sfx_cars1", string.Empty, callback); 
public AudioClip cars2 => ResourceManager.LoadResource<AudioClip>("AudioClips/GameplayEnvironments/sfx_cars2");
public void cars2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/GameplayEnvironments/sfx_cars2", string.Empty, callback); 
public AudioClip cars3 => ResourceManager.LoadResource<AudioClip>("AudioClips/GameplayEnvironments/sfx_cars3");
public void cars3_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/GameplayEnvironments/sfx_cars3", string.Empty, callback); 
public AudioClip cars4 => ResourceManager.LoadResource<AudioClip>("AudioClips/GameplayEnvironments/sfx_cars4");
public void cars4_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/GameplayEnvironments/sfx_cars4", string.Empty, callback); 
public AudioClip horn1 => ResourceManager.LoadResource<AudioClip>("AudioClips/GameplayEnvironments/sfx_horn1");
public void horn1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/GameplayEnvironments/sfx_horn1", string.Empty, callback); 
public AudioClip horn2 => ResourceManager.LoadResource<AudioClip>("AudioClips/GameplayEnvironments/sfx_horn2");
public void horn2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/GameplayEnvironments/sfx_horn2", string.Empty, callback); 
public AudioClip street => ResourceManager.LoadResource<AudioClip>("AudioClips/GameplayEnvironments/sfx_street");
public void street_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/GameplayEnvironments/sfx_street", string.Empty, callback); 
public AudioClip gameplay1 => ResourceManager.LoadResource<AudioClip>("AudioClips/GameplayMusics/sfx_gameplay1");
public void gameplay1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/GameplayMusics/sfx_gameplay1", string.Empty, callback); 
public AudioClip gameplay2 => ResourceManager.LoadResource<AudioClip>("AudioClips/GameplayMusics/sfx_gameplay2");
public void gameplay2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/GameplayMusics/sfx_gameplay2", string.Empty, callback); 
public AudioClip gameplay3 => ResourceManager.LoadResource<AudioClip>("AudioClips/GameplayMusics/sfx_gameplay3");
public void gameplay3_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/GameplayMusics/sfx_gameplay3", string.Empty, callback); 
public AudioClip BackClick => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/sfx_BackClick");
public void BackClick_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/sfx_BackClick", string.Empty, callback); 
public AudioClip Click => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/sfx_Click");
public void Click_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/sfx_Click", string.Empty, callback); 
public AudioClip logo => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/sfx_logo");
public void logo_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/sfx_logo", string.Empty, callback); 
public AudioClip ajdar_attack4 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_ajdar_attack4");
public void ajdar_attack4_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_ajdar_attack4", string.Empty, callback); 
public AudioClip ajdar_attack5 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_ajdar_attack5");
public void ajdar_attack5_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_ajdar_attack5", string.Empty, callback); 
public AudioClip ajdar_death1 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_ajdar_death1");
public void ajdar_death1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_ajdar_death1", string.Empty, callback); 
public AudioClip ajdar_death4 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_ajdar_death4");
public void ajdar_death4_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_ajdar_death4", string.Empty, callback); 
public AudioClip ajdar_spawn11 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_ajdar_spawn11");
public void ajdar_spawn11_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_ajdar_spawn11", string.Empty, callback); 
public AudioClip ajdar_spawn2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_ajdar_spawn2");
public void ajdar_spawn2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_ajdar_spawn2", string.Empty, callback); 
public AudioClip ajdar_spawn3 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_ajdar_spawn3");
public void ajdar_spawn3_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_ajdar_spawn3", string.Empty, callback); 
public AudioClip ajdar_spawn6 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_ajdar_spawn6");
public void ajdar_spawn6_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_ajdar_spawn6", string.Empty, callback); 
public AudioClip arash_attack2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_arash_attack2");
public void arash_attack2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_arash_attack2", string.Empty, callback); 
public AudioClip arash_attack4 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_arash_attack4");
public void arash_attack4_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_arash_attack4", string.Empty, callback); 
public AudioClip arash_death1 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_arash_death1");
public void arash_death1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_arash_death1", string.Empty, callback); 
public AudioClip arash_death2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_arash_death2");
public void arash_death2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_arash_death2", string.Empty, callback); 
public AudioClip arash_spawn1 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_arash_spawn1");
public void arash_spawn1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_arash_spawn1", string.Empty, callback); 
public AudioClip arash_spawn2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_arash_spawn2");
public void arash_spawn2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_arash_spawn2", string.Empty, callback); 
public AudioClip arash_spawn5 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_arash_spawn5");
public void arash_spawn5_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_arash_spawn5", string.Empty, callback); 
public AudioClip arash_spawn6 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_arash_spawn6");
public void arash_spawn6_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_arash_spawn6", string.Empty, callback); 
public AudioClip baba_farmon_attack2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_baba_farmon_attack2");
public void baba_farmon_attack2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_baba_farmon_attack2", string.Empty, callback); 
public AudioClip baba_farmon_attack4 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_baba_farmon_attack4");
public void baba_farmon_attack4_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_baba_farmon_attack4", string.Empty, callback); 
public AudioClip baba_farmon_death1 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_baba_farmon_death1");
public void baba_farmon_death1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_baba_farmon_death1", string.Empty, callback); 
public AudioClip baba_farmon_death3 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_baba_farmon_death3");
public void baba_farmon_death3_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_baba_farmon_death3", string.Empty, callback); 
public AudioClip baba_farmon_death4 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_baba_farmon_death4");
public void baba_farmon_death4_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_baba_farmon_death4", string.Empty, callback); 
public AudioClip baba_farmon_spawn10 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_baba_farmon_spawn10");
public void baba_farmon_spawn10_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_baba_farmon_spawn10", string.Empty, callback); 
public AudioClip baba_farmon_spawn15 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_baba_farmon_spawn15");
public void baba_farmon_spawn15_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_baba_farmon_spawn15", string.Empty, callback); 
public AudioClip baba_farmon_spawn4 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_baba_farmon_spawn4");
public void baba_farmon_spawn4_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_baba_farmon_spawn4", string.Empty, callback); 
public AudioClip baba_farmon_spawn9 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_baba_farmon_spawn9");
public void baba_farmon_spawn9_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_baba_farmon_spawn9", string.Empty, callback); 
public AudioClip bijan_bi_kale_attack1 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_bijan_bi_kale_attack1");
public void bijan_bi_kale_attack1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_bijan_bi_kale_attack1", string.Empty, callback); 
public AudioClip bijan_bi_kale_attack2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_bijan_bi_kale_attack2");
public void bijan_bi_kale_attack2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_bijan_bi_kale_attack2", string.Empty, callback); 
public AudioClip bijan_bi_kale_death1 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_bijan_bi_kale_death1");
public void bijan_bi_kale_death1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_bijan_bi_kale_death1", string.Empty, callback); 
public AudioClip bijan_bi_kale_death5 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_bijan_bi_kale_death5");
public void bijan_bi_kale_death5_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_bijan_bi_kale_death5", string.Empty, callback); 
public AudioClip bijan_bi_kale_spawn13 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_bijan_bi_kale_spawn13");
public void bijan_bi_kale_spawn13_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_bijan_bi_kale_spawn13", string.Empty, callback); 
public AudioClip bijan_bi_kale_spawn23 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_bijan_bi_kale_spawn23");
public void bijan_bi_kale_spawn23_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_bijan_bi_kale_spawn23", string.Empty, callback); 
public AudioClip gol_pari_attack1 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_gol_pari_attack1");
public void gol_pari_attack1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_gol_pari_attack1", string.Empty, callback); 
public AudioClip gol_pari_attack2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_gol_pari_attack2");
public void gol_pari_attack2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_gol_pari_attack2", string.Empty, callback); 
public AudioClip gol_pari_attack3 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_gol_pari_attack3");
public void gol_pari_attack3_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_gol_pari_attack3", string.Empty, callback); 
public AudioClip gol_pari_death1 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_gol_pari_death1");
public void gol_pari_death1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_gol_pari_death1", string.Empty, callback); 
public AudioClip gol_pari_spawn3 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_gol_pari_spawn3");
public void gol_pari_spawn3_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_gol_pari_spawn3", string.Empty, callback); 
public AudioClip gol_pari_spawn4 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_gol_pari_spawn4");
public void gol_pari_spawn4_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_gol_pari_spawn4", string.Empty, callback); 
public AudioClip gol_pari_spawn5 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_gol_pari_spawn5");
public void gol_pari_spawn5_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_gol_pari_spawn5", string.Empty, callback); 
public AudioClip healer_death1 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_healer_death1");
public void healer_death1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_healer_death1", string.Empty, callback); 
public AudioClip healer_death2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_healer_death2");
public void healer_death2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_healer_death2", string.Empty, callback); 
public AudioClip hoshang_attack2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_hoshang_attack2");
public void hoshang_attack2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_hoshang_attack2", string.Empty, callback); 
public AudioClip hoshang_attack4 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_hoshang_attack4");
public void hoshang_attack4_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_hoshang_attack4", string.Empty, callback); 
public AudioClip hoshang_death1 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_hoshang_death1");
public void hoshang_death1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_hoshang_death1", string.Empty, callback); 
public AudioClip hoshang_death3 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_hoshang_death3");
public void hoshang_death3_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_hoshang_death3", string.Empty, callback); 
public AudioClip hoshang_spawn10 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_hoshang_spawn10");
public void hoshang_spawn10_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_hoshang_spawn10", string.Empty, callback); 
public AudioClip hoshang_spawn15 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_hoshang_spawn15");
public void hoshang_spawn15_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_hoshang_spawn15", string.Empty, callback); 
public AudioClip hoshang_spawn16 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_hoshang_spawn16");
public void hoshang_spawn16_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_hoshang_spawn16", string.Empty, callback); 
public AudioClip hoshang_spawn8 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_hoshang_spawn8");
public void hoshang_spawn8_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_hoshang_spawn8", string.Empty, callback); 
public AudioClip hotan_attack1 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_hotan_attack1");
public void hotan_attack1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_hotan_attack1", string.Empty, callback); 
public AudioClip hotan_attack2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_hotan_attack2");
public void hotan_attack2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_hotan_attack2", string.Empty, callback); 
public AudioClip hotan_death1 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_hotan_death1");
public void hotan_death1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_hotan_death1", string.Empty, callback); 
public AudioClip hotan_death2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_hotan_death2");
public void hotan_death2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_hotan_death2", string.Empty, callback); 
public AudioClip hotan_spawn3 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_hotan_spawn3");
public void hotan_spawn3_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_hotan_spawn3", string.Empty, callback); 
public AudioClip hotan_spawn5 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_hotan_spawn5");
public void hotan_spawn5_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_hotan_spawn5", string.Empty, callback); 
public AudioClip nane_gohar_attack2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_nane_gohar_attack2");
public void nane_gohar_attack2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_nane_gohar_attack2", string.Empty, callback); 
public AudioClip nane_gohar_attack5 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_nane_gohar_attack5");
public void nane_gohar_attack5_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_nane_gohar_attack5", string.Empty, callback); 
public AudioClip nane_gohar_attack8 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_nane_gohar_attack8");
public void nane_gohar_attack8_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_nane_gohar_attack8", string.Empty, callback); 
public AudioClip nane_gohar_spawn2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_nane_gohar_spawn2");
public void nane_gohar_spawn2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_nane_gohar_spawn2", string.Empty, callback); 
public AudioClip nane_gohar_spawn7 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_nane_gohar_spawn7");
public void nane_gohar_spawn7_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_nane_gohar_spawn7", string.Empty, callback); 
public AudioClip salar_attack1 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_salar_attack1");
public void salar_attack1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_salar_attack1", string.Empty, callback); 
public AudioClip salar_attack2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_salar_attack2");
public void salar_attack2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_salar_attack2", string.Empty, callback); 
public AudioClip salar_death4 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_salar_death4");
public void salar_death4_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_salar_death4", string.Empty, callback); 
public AudioClip salar_death7 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_salar_death7");
public void salar_death7_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_salar_death7", string.Empty, callback); 
public AudioClip salar_spawn25 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_salar_spawn25");
public void salar_spawn25_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_salar_spawn25", string.Empty, callback); 
public AudioClip salar_spawn5 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_salar_spawn5");
public void salar_spawn5_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_salar_spawn5", string.Empty, callback); 
public AudioClip salar_spawn8 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_salar_spawn8");
public void salar_spawn8_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_salar_spawn8", string.Empty, callback); 
public AudioClip salar_spawn9 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_salar_spawn9");
public void salar_spawn9_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_salar_spawn9", string.Empty, callback); 
public AudioClip sibil_attack2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_sibil_attack2");
public void sibil_attack2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_sibil_attack2", string.Empty, callback); 
public AudioClip sibil_death1 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_sibil_death1");
public void sibil_death1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_sibil_death1", string.Empty, callback); 
public AudioClip sibil_death2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_sibil_death2");
public void sibil_death2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_sibil_death2", string.Empty, callback); 
public AudioClip sibil_spawn14 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_sibil_spawn14");
public void sibil_spawn14_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_sibil_spawn14", string.Empty, callback); 
public AudioClip sibil_spawn2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_sibil_spawn2");
public void sibil_spawn2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_sibil_spawn2", string.Empty, callback); 
public AudioClip sibil_spawn4 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_sibil_spawn4");
public void sibil_spawn4_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_sibil_spawn4", string.Empty, callback); 
public AudioClip susan_attack1 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_susan_attack1");
public void susan_attack1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_susan_attack1", string.Empty, callback); 
public AudioClip susan_attack2 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_susan_attack2");
public void susan_attack2_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_susan_attack2", string.Empty, callback); 
public AudioClip susan_death1 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_susan_death1");
public void susan_death1_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_susan_death1", string.Empty, callback); 
public AudioClip susan_spawn4 => ResourceManager.LoadResource<AudioClip>("AudioClips/SFX/Characters/sfx_susan_spawn4");
public void susan_spawn4_async(Action<AudioClip> callback) => ResourceManager.Instance.LoadResourceAsync("AudioClips/SFX/Characters/sfx_susan_spawn4", string.Empty, callback); 
}
public class TrixFont
{
private readonly Dictionary<string, string> _fontEnumPathDictionary = new Dictionary<string, string>
{

};
public void GetByEnum(Enum enumValue, string path, Action<Font> callback)
{
if (string.Equals(enumValue.ToString(), StaticValues.UrlKey, StringComparison.OrdinalIgnoreCase))
{
ResourceManager.Instance.LoadResourceAsync(StaticValues.UrlKey, path, callback);
return;
}
var key = $"{enumValue.GetType().Name}_{enumValue}";
string pathValue;
if (_fontEnumPathDictionary.TryGetValue(key, out pathValue))
{
ResourceManager.Instance.LoadResourceAsync(pathValue, string.Empty, callback);
return;
}
callback(null);
}

public Font lalezar => ResourceManager.LoadResource<Font>("Fonts/fnt_lalezar");
public void lalezar_async(Action<Font> callback) => ResourceManager.Instance.LoadResourceAsync("Fonts/fnt_lalezar", string.Empty, callback); 
public Font names => ResourceManager.LoadResource<Font>("Fonts/fnt_names");
public void names_async(Action<Font> callback) => ResourceManager.Instance.LoadResourceAsync("Fonts/fnt_names", string.Empty, callback); 
public Font notification => ResourceManager.LoadResource<Font>("Fonts/fnt_notification");
public void notification_async(Action<Font> callback) => ResourceManager.Instance.LoadResourceAsync("Fonts/fnt_notification", string.Empty, callback); 
public Font title => ResourceManager.LoadResource<Font>("Fonts/fnt_title");
public void title_async(Action<Font> callback) => ResourceManager.Instance.LoadResourceAsync("Fonts/fnt_title", string.Empty, callback); 
}
public class TrixShader
{
private readonly Dictionary<string, string> _shaderEnumPathDictionary = new Dictionary<string, string>
{

};
public void GetByEnum(Enum enumValue, string path, Action<Shader> callback)
{
if (string.Equals(enumValue.ToString(), StaticValues.UrlKey, StringComparison.OrdinalIgnoreCase))
{
ResourceManager.Instance.LoadResourceAsync(StaticValues.UrlKey, path, callback);
return;
}
var key = $"{enumValue.GetType().Name}_{enumValue}";
string pathValue;
if (_shaderEnumPathDictionary.TryGetValue(key, out pathValue))
{
ResourceManager.Instance.LoadResourceAsync(pathValue, string.Empty, callback);
return;
}
callback(null);
}

}
public class TrixMaterial
{
private readonly Dictionary<string, string> _materialEnumPathDictionary = new Dictionary<string, string>
{

};
public void GetByEnum(Enum enumValue, string path, Action<Material> callback)
{
if (string.Equals(enumValue.ToString(), StaticValues.UrlKey, StringComparison.OrdinalIgnoreCase))
{
ResourceManager.Instance.LoadResourceAsync(StaticValues.UrlKey, path, callback);
return;
}
var key = $"{enumValue.GetType().Name}_{enumValue}";
string pathValue;
if (_materialEnumPathDictionary.TryGetValue(key, out pathValue))
{
ResourceManager.Instance.LoadResourceAsync(pathValue, string.Empty, callback);
return;
}
callback(null);
}

}}}