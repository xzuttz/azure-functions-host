// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Azure.WebJobs.Script.Properties;
using Microsoft.Extensions.Logging;

namespace Microsoft.Azure.WebJobs.Script.Diagnostics.Extensions
{
    internal static class ExtensionBundleLoggerExtension
    {
        private static readonly string _codeArea = nameof(ExtensionBundleLoggerExtension).Replace("LoggerExtension", string.Empty);

        private static readonly EventId _contentProviderNotConfigured = new EventId(0, $"{_codeArea}\\{nameof(ContentProviderNotConfigured)}");
        private static readonly EventId _contentFileNotFound = new EventId(0, $"{_codeArea}\\{nameof(ContentFileNotFound)}");
        private static readonly EventId _locateExtensionBundle = new EventId(0, $"{_codeArea}\\{nameof(LocateExtensionBundle)}");
        private static readonly EventId _extensionBundleFound = new EventId(0, $"{_codeArea}\\{nameof(ExtensionBundleFound)}");
        private static readonly EventId _extractingBundleZip = new EventId(0, $"{_codeArea}\\{nameof(ExtractingBundleZip)}");
        private static readonly EventId _zipExtractionComplete = new EventId(0, $"{_codeArea}\\{nameof(ZipExtractionComplete)}");
        private static readonly EventId _downloadingZip = new EventId(0, $"{_codeArea}\\{nameof(DownloadingZip)}");
        private static readonly EventId _errorDownloadingZip = new EventId(0, $"{_codeArea}\\{nameof(ErrorDownloadingZip)}");
        private static readonly EventId _downloadComplete = new EventId(0, $"{_codeArea}\\{nameof(DownloadComplete)}");
        private static readonly EventId _fetchingVersionInfo = new EventId(0, $"{_codeArea}\\{nameof(FetchingVersionInfo)}");
        private static readonly EventId _errorFetchingVersionInfo = new EventId(0, $"{_codeArea}\\{nameof(ErrorFetchingVersionInfo)}");
        private static readonly EventId _matchingBundleNotFound = new EventId(0, $"{_codeArea}\\{nameof(MatchingBundleNotFound)}");

        public static void ContentProviderNotConfigured(this ILogger logger, string path)
        {
            logger.Log(LogLevel.Information, _contentProviderNotConfigured, "Extension bundle configuration is not present in host.json.Cannot load content for file {0}", path);
        }

        public static void ContentFileNotFound(this ILogger logger, string contentFilePath)
        {
            logger.Log(LogLevel.Error, _contentFileNotFound, "File not found at {0}.", contentFilePath);
        }

        public static void LocateExtensionBundle(this ILogger logger, string id, string path)
        {
            logger.Log(LogLevel.Information, _locateExtensionBundle, "Looking for extension bundle {0} at {1}", id, path);
        }

        public static void ExtensionBundleFound(this ILogger logger, string bundlePath)
        {
            logger.Log(LogLevel.Error, _extensionBundleFound, "Found a matching extension bundle at {0}", bundlePath);
        }

        public static void ExtractingBundleZip(this ILogger logger, string bundlePath)
        {
            logger.Log(LogLevel.Information, _extractingBundleZip, "Extracting extension bundle at {0}", bundlePath);
        }

        public static void ZipExtractionComplete(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _zipExtractionComplete, "Zip extraction complete");
        }

        public static void DownloadingZip(this ILogger logger, Uri zipUri, string filePath)
        {
            logger.Log(LogLevel.Information, _downloadingZip, "Downloading extension bundle from {0} to {1}", zipUri.ToString(), filePath);
        }

        public static void ErrorDownloadingZip(this ILogger logger, Uri zipUri, HttpResponseMessage response)
        {
            logger.Log(LogLevel.Error, _errorDownloadingZip, "Error downloading zip content {0}. Status Code:{1}. Reason:{2}", zipUri.ToString(), response.StatusCode.ToString(), response.ReasonPhrase.ToString());
        }

        public static void DownloadComplete(this ILogger logger, Uri zipUri, string filePath)
        {
            logger.Log(LogLevel.Information, _downloadComplete, "Completed downloading extension bundle from {0} to {1}", zipUri.ToString(), filePath);
        }

        public static void FetchingVersionInfo(this ILogger logger, string id, Uri uri)
        {
            logger.Log(LogLevel.Information, _fetchingVersionInfo, "Fetching information on versions of extension bundle {0} available on {1}{2}", id, uri.ToString());
        }

        public static void ErrorFetchingVersionInfo(this ILogger logger, string id)
        {
            logger.Log(LogLevel.Error, _errorFetchingVersionInfo, "Error fetching version information for extension bundle {0}", id);
        }

        public static void MatchingBundleNotFound(this ILogger logger, string version)
        {
            logger.Log(LogLevel.Information, _matchingBundleNotFound, "Bundle version matching the {0} was not found", version);
        }
    }
}
