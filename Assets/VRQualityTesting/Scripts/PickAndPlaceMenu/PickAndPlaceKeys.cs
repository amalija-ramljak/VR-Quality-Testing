namespace VRQualityTesting.Scripts.PickAndPlaceMenu
{
    public static class PickAndPlaceKeys
    {
        private const string Prefix = "PickAndPlace";

        // object to pick and place
        public const string ObjectMinDistance = Prefix + "ObjectMinDistance";
        public const string ObjectMaxDistance = Prefix + "ObjectMaxDistance";
        public const string ObjectMinHeight = Prefix + "ObjectMinHeight";
        public const string ObjectMaxHeight = Prefix + "ObjectMaxHeight";
        public const string ObjectMaxRotationOffset = Prefix + "ObjectMaxRotationOffset";
        public const string ObjectMinSize = Prefix + "ObjectMinSize";
        public const string ObjectMaxSize = Prefix + "ObjectMaxSize";

        // goal for placing
        public const string GoalDistance = Prefix + "GoalDistance";
        public const string GoalRotationOffset = Prefix + "GoalRotationOffset";

        public const string GoalSize = Prefix + "GoalSize";
        public const string GoalHeight = Prefix + "GoalHeight";

        // obstacles around object
        public const string ObstacleMinDistance = Prefix + "ObstacleMinDistance";
        public const string ObstacleMaxDistance = Prefix + "ObstacleMaxDistance";
        public const string ObstacleMinCount = Prefix + "ObstacleMinCount";
        public const string ObstacleMaxCount = Prefix + "ObstacleMaxCount";
        
        public const string ObstacleMinSize = Prefix + "ObstacleMinSize";
        public const string ObstacleMaxSize = Prefix + "ObstacleMaxSize";

        // object types
        public const string UseObjectTypeSquare = Prefix + "UseObjectTypeSquare";
        public const string UseObjectTypeCylinder = Prefix + "useObjectTypeCylinder";
        public const string UseObjectTypeSphere = Prefix + "UseObjectTypeSphere";

        // round
        public const string RoundDuration = Prefix + "RoundDuration";
    }
}
