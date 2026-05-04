namespace CupkekGames.Cameras
{
    /// <summary>
    /// Reserved kind constants for the impulse sources registered on
    /// <see cref="CinemachineManager"/>. Games are free to add their own keys —
    /// these are just the package-level defaults used by sibling Cupkek packages.
    /// </summary>
    public static class CinemachineScreenShakeKinds
    {
        public const string Default = "Default";
        public const string Critical = "Critical";
        public const string Rumble = "Rumble";
    }
}
