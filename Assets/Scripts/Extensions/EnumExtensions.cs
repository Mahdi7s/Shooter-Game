using System;
using Models.Constants;
using UnityEngine;

namespace Extensions
{
    public static class EnumExtensions
    {
        public static string GetPath(this Scenes scenes)
        {
            return $"{StaticValues.DirectoryScenes}{scenes}.unity";
        }

        public static Vector2 ToVector2(this WindDirectionSprite windDirection)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (windDirection)
            {
                case WindDirectionSprite.spr_East:
                    return new Vector2(1, 0);
                case WindDirectionSprite.spr_North:
                    return new Vector2(0, 1);
                case WindDirectionSprite.spr_NorthEast:
                    return new Vector2(1, 1);
                case WindDirectionSprite.spr_NorthWest:
                    return new Vector2(-1, 1);
                case WindDirectionSprite.spr_South:
                    return new Vector2(0, -1);
                case WindDirectionSprite.spr_SouthEast:
                    return new Vector2(1, -1);
                case WindDirectionSprite.spr_SouthWest:
                    return new Vector2(-1, -1);
                case WindDirectionSprite.spr_West:
                    return new Vector2(-1, 0);
                default:
                    return new Vector2(0, 0);
            }
        }
    }
}
