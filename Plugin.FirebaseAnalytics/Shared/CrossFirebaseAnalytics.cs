﻿using System;

namespace Plugin.FirebaseAnalytics
{
    /// <summary>
    /// Cross FirebaseAnalytics
    /// </summary>
    public class CrossFirebaseAnalytics
    {
        static Lazy<IFirebaseAnalytics?> implementation = new Lazy<IFirebaseAnalytics?>(() => CreateFirebaseAnalytics(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets if the plugin is supported on the current platform.
        /// </summary>
        public static bool IsSupported => implementation.Value == null ? false : true;

        /// <summary>
        /// Current plugin implementation to use
        /// </summary>
        public static IFirebaseAnalytics Current
        {
            get
            {
                IFirebaseAnalytics? ret = implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        static IFirebaseAnalytics? CreateFirebaseAnalytics()
        {
#if NETSTANDARD
            return null;
#else
#pragma warning disable IDE0022 // Use expression body for methods
            return new FirebaseAnalyticsImplementation();
#pragma warning restore IDE0022 // Use expression body for methods
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");

    }
}
