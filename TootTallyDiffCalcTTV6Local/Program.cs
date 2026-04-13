namespace TootTallyDiffCalcTTV6Local
{
    internal static class Program
    {
        public const string EXPORT_DIRECTORY = "export/";
        public const string DOWNLOAD_DIRECTORY = "downloads/";
        public const string CACHE_DIRECTORY = "cache/";

        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JHaF5cWWdCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdlWXxcdXVSQmRcUUJxWkBWYEo=");
            Directory.CreateDirectory(EXPORT_DIRECTORY);
            Directory.CreateDirectory(DOWNLOAD_DIRECTORY);
            Directory.CreateDirectory(CACHE_DIRECTORY);
            Application.Run(new MainForm());
        }
    }
}