namespace VRQualityTesting.Scripts.Utility
{
    public static class SettingsKeys
    {
        public static class MainMenu
        {
            private const string Prefix = "MainMenu";

            public const string StudyID = Prefix + "StudyID";
            public const string ParticipantID = Prefix + "ParticipantID";
            public const string SelectedGameIndex = Prefix + "SelectedGameIndex";
        }

        public static class Shooter
        {
            private const string Prefix = "Shooter";

            public const string MinDistance = Prefix + "MinDistance";
            public const string MaxDistance = Prefix + "MaxDistance";
            public const string MinHeight = Prefix + "MinHeight";
            public const string MaxHeight = Prefix + "MaxHeight";
            public const string SpawnAngle = Prefix + "SpawnAngle";
            public const string SpawnCount = Prefix + "SpawnCount";
            public const string DurationBetweenSpawns = Prefix + "DurationBetweenSpawns";
            public const string RoundDuration = Prefix + "RoundDuration";

            public const string MinSize = Prefix + "MinSize";
            public const string MaxSize = Prefix + "MaxSize";
            public const string MovingProbability = Prefix + "MovingProbability";
            public const string MinVelocity = Prefix + "MinVelocity";
            public const string MaxVelocity = Prefix + "MaxVelocity";
            public const string MinOffset = Prefix + "MinOffset";
            public const string MaxOffset = Prefix + "MaxOffset";

            public const string WeaponType = Prefix + "WeaponType";
            public const string UseLaser = Prefix + "UseLaser";
            public const string ShowBulletTrajectory = Prefix + "ShowBulletTrajectory";
            public const string ShowMuzzleFlash = Prefix + "ShowMuzzleFlash";
        }

        public static class BoxSmasher
        {
            private const string Prefix = "BoxSmasher";
        }
    }
}