using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRQualityTesting.Scripts.PickAndPlace
{
    public class PAPObject
    {
        public DateTime BirthTimestamp { get; } = DateTime.Now;
        public DateTime DeathTimestamp { get; set; }
        public bool isPlaced = false;
        public Vector3 spawnPosition;
        public List<PAPObstacle> clutter;

        public PAPObject(Vector3 spawnPosition)
        {
            this.spawnPosition = spawnPosition;
        }

        public void setPlaced(bool isPlaced)
        {
            this.isPlaced = isPlaced;
        }

        public void setClutter(List<PAPObstacle> clutter)
        {
            this.clutter = clutter;
        }
    }

    public class PAPObstacle
    {
        public Vector3 initialCoords;
        public Vector3 finalCoords;

        public PAPObstacle(
            Vector3 initialCoordinates,
            Vector3 finalCoordinates
        )
        {
            this.initialCoords = initialCoordinates;
            this.finalCoords = finalCoordinates;
        }
    }
}