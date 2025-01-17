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
        public float placedOffset;
        public float spawnSize;
        public ObjectSpawner.Shape shape;
        public List<PAPObstacle> clutter;

        public PAPObject(Vector3 spawnPosition, float spawnSize, ObjectSpawner.Shape shape)
        {
            this.spawnPosition = spawnPosition;
            this.spawnSize = spawnSize;
            this.shape = shape;
        }

        public void setPlaced(bool isPlaced)
        {
            this.isPlaced = isPlaced;
        }

        public void setPlacedOffset(float placedOffset)
        {
            this.placedOffset = placedOffset;
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
        public float size;

        public PAPObstacle(
            Vector3 initialCoordinates,
            Vector3 finalCoordinates,
            float size
        )
        {
            this.initialCoords = initialCoordinates;
            this.finalCoords = finalCoordinates;
            this.size = size;
        }
    }
}