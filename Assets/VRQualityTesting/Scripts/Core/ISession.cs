using System.Collections.Generic;

namespace VRQualityTesting.Scripts.Core
{
    /// <summary>
    /// Represents a single game session (round) which generates results once finished.
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// What exactly is being tested?
        /// </summary>
        string StudyID { get; }

        /// <summary>
        /// Who are we testing?
        /// </summary>
        string ParticipantID { get; }

        /// <summary>
        /// Which game do we test on?
        /// </summary>
        GameTitle GameTitle { get; }

        /// <summary>
        /// Returns summary of the session without going into small details.
        /// </summary>
        List<string> GeneralInformation { get; }

        /// <summary>
        /// Returns details of the session which are used for a more thorough analysis.
        /// </summary>
        List<string> DetailedInformation { get; }
    }
}
