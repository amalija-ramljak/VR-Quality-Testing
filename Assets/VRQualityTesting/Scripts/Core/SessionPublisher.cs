using System;
using System.IO;
using UnityEngine;

namespace VRQualityTesting.Scripts.Core
{
    /// <summary>
    /// Component with a single job: saving the provided session information to two separate files,
    /// one containing general session information, other detailed session information.
    /// </summary>
    public static class SessionPublisher
    {
        private const string TimestampFormat = "yyyy-MM-dd_HH-mm-ss-fff";
        private static readonly char DirectorySeparator = Path.DirectorySeparatorChar;
        private static readonly string RootSaveDirectory = Application.persistentDataPath;

        public static void Publish(ISession session, string GeneralInformationExtension = ".txt", string DetailedInformationExtension = ".csv")
        {
            var filePath = GetSaveFilePath(session);
            File.WriteAllLines(filePath + "_general" + GeneralInformationExtension, session.GeneralInformation);
            File.WriteAllLines(filePath + "_details" + DetailedInformationExtension, session.DetailedInformation);
        }

        private static string GetSaveFilePath(ISession session)
        {
            var saveFileDirectory = RootSaveDirectory + DirectorySeparator + session.StudyID + DirectorySeparator + session.ParticipantID;

            if (!Directory.Exists(saveFileDirectory))
            {
                Directory.CreateDirectory(saveFileDirectory);
            }

            return saveFileDirectory + DirectorySeparator + session.GameTitle + "_" + DateTime.Now.ToString(TimestampFormat);
        }
    }
}
