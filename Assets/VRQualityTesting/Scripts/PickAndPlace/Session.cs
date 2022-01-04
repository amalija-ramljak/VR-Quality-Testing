using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using VRQualityTesting.Scripts.Core;
using VRQualityTesting.Scripts.MainMenu;
using VRQualityTesting.Scripts.PickAndPlaceMenu;
using VRQualityTesting.Scripts.Utility;

namespace VRQualityTesting.Scripts.PickAndPlace
{
    public class Session : ISession
    {
        private const string WeaponHitsInformationHeader = "Distance to target, Distance to center, Lifetime, Size, Velocity, Offset, Hand side, Birth timestamp, Death timestamp";
        private const string TimestampFormat = "HH:mm:ss:fff";

        public string StudyID => Settings.GetString(MainMenuKeys.StudyID);
        public string ParticipantID => Settings.GetString(MainMenuKeys.ParticipantID);
        public GameTitle GameTitle => GameTitle.PickAndPlace;

        private readonly List<PAPObject> _objects;

        public Session(List<PAPObject> objects)
        {
            _objects = objects;
        }

        public List<string> GeneralInformation
        {
            get
            {
                int objSpawned = _objects.Count;
                int objPlaced = _objects.Count(obj => obj.isPlaced);

                return new List<string>
                {
                    "# Round results",
                    $"Objects spawned: {objSpawned}",
                    "",
                    $"Objects placed: {objPlaced}",
                    "",
                    $"Duration: {Settings.GetFloat(PickAndPlaceKeys.RoundDuration).ToString(CultureInfo.InvariantCulture)}",

                    $"{Environment.NewLine}# Object settings",
                    $"Minimum distance: {Settings.GetFloat(PickAndPlaceKeys.ObjectMinDistance).ToString(CultureInfo.InvariantCulture)}",
                    $"Maximum distance: {Settings.GetFloat(PickAndPlaceKeys.ObjectMaxDistance).ToString(CultureInfo.InvariantCulture)}",
                    $"Maximum rotation offset: {Settings.GetFloat(PickAndPlaceKeys.ObjectMaxRotationOffset).ToString(CultureInfo.InvariantCulture)}",
                    $"Minimum size: {Settings.GetFloat(PickAndPlaceKeys.ObjectMinSize).ToString(CultureInfo.InvariantCulture)}",
                    $"Maximum size: {Settings.GetFloat(PickAndPlaceKeys.ObjectMaxSize).ToString(CultureInfo.InvariantCulture)}",
                    $"Use square objects: {Settings.GetBool(PickAndPlaceKeys.UseObjectTypeSquare).ToString(CultureInfo.InvariantCulture)}",
                    $"Use cylinder objects: {Settings.GetBool(PickAndPlaceKeys.UseObjectTypeCylinder).ToString(CultureInfo.InvariantCulture)}",
                    $"Use sphere objects: {Settings.GetBool(PickAndPlaceKeys.UseObjectTypeSphere).ToString(CultureInfo.InvariantCulture)}",

                    $"{Environment.NewLine}# Clutter settings",
                    $"Minimum distance: {Settings.GetFloat(PickAndPlaceKeys.ObstacleMinDistance).ToString(CultureInfo.InvariantCulture)}",
                    $"Maximum distance: {Settings.GetFloat(PickAndPlaceKeys.ObstacleMaxDistance).ToString(CultureInfo.InvariantCulture)}",
                    $"Minimum count: {Settings.GetInt(PickAndPlaceKeys.ObstacleMinCount).ToString(CultureInfo.InvariantCulture)}",
                    $"Maximum count: {Settings.GetInt(PickAndPlaceKeys.ObstacleMaxCount).ToString(CultureInfo.InvariantCulture)}",
                    $"Minimum size: {Settings.GetFloat(PickAndPlaceKeys.ObstacleMinSize).ToString(CultureInfo.InvariantCulture)}",
                    $"Maximum size: {Settings.GetFloat(PickAndPlaceKeys.ObstacleMaxSize).ToString(CultureInfo.InvariantCulture)}",

                    $"{Environment.NewLine}# Goal settings",
                    $"Distance: {Settings.GetFloat(PickAndPlaceKeys.GoalDistance).ToString(CultureInfo.InvariantCulture)}",
                    $"Rotation offset: {Settings.GetFloat(PickAndPlaceKeys.GoalRotationOffset).ToString(CultureInfo.InvariantCulture)}",
                    $"Size: {Settings.GetInt(PickAndPlaceKeys.GoalSize).ToString(CultureInfo.InvariantCulture)}",
                    $"Elevation: {Settings.GetInt(PickAndPlaceKeys.GoalHeight).ToString(CultureInfo.InvariantCulture)}",
                };
            }
        }

        public List<string> DetailedInformation
        {
            get
            {
                var detailedInformation = new List<string> { };
                int idx = 0;
                foreach (var obj in _objects)
                {
                    idx++;
                    detailedInformation.AddRange(new List<string> {
                        Environment.NewLine, Environment.NewLine,
                        $"# Object {idx}",
                        $"Created at time: {obj.BirthTimestamp.ToString(TimestampFormat)}",
                        $"Placed at time: {obj.DeathTimestamp.ToString(TimestampFormat)}",
                        $"Clutter data ({obj.clutter.Count} objects in clutter)",
                        "Initial position, Final position, Euclidean distance",
                    });

                    detailedInformation.AddRange(
                        obj.clutter.Select(
                            clutterObj =>
                                $"{clutterObj.initialCoords}, " +
                                $"{clutterObj.finalCoords}, " +
                                $"{Vector3.Distance(clutterObj.initialCoords, clutterObj.finalCoords)}"
                        )
                    );
                }
                return detailedInformation;
            }
        }
    }
}
