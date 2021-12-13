namespace VRQualityTesting.Scripts.PickAndPlaceMenu
{
    public static class PickAndPlaceKeys
    {
        private const string Prefix = "PickAndPlace";

        // object to pick and place
        public const string ObjectMinDistance = Prefix + "ObjectMinDistance";
        public const string ObjectMaxDistance = Prefix + "ObjectMaxDistance";
        public const string ObjectMaxRotationOffset = Prefix + "ObjectMaxRotationOffset";

        // goal for placing
        public const string GoalMinDistance = Prefix + "GoalMinDistance";
        public const string GoalMaxDistance = Prefix + "GoalMaxDistance";
        public const string GoalMaxRotationOffset = Prefix + "GoalMaxRotationOffset";

        // obstacles around object
        public const string ObstacleMinDistance = Prefix + "ObstacleMinDistance";
        public const string ObstacleMaxDistance = Prefix + "ObstacleMaxDistance";
        public const string ObstacleMinCount = Prefix + "ObstacleMinCount";
        public const string ObstacleMaxCount = Prefix + "ObstacleMaxCount";

        // object types
        public const string UseObjectTypeSquare = Prefix + "UseObjectTypeSquare";
        public const string UseObjectTypeCylinder = Prefix + "useObjectTypeCylinder";
        public const string UseObjectTypeSphere = Prefix + "UseObjectTypeSphere";

        // round
        public const string RoundDuration = Prefix + "RoundDuration";
    }
}
