// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using static System.Environment;

namespace Microsoft.Azure.WebJobs.Script.Diagnostics.Extensions
{
    public static class HostLoggerExtension
    {
        private static readonly string _codeArea = nameof(HostLoggerExtension).Replace("LoggerExtension", string.Empty);

        private static readonly EventId _hostConfigApplied = new EventId(0, $"{_codeArea}\\{nameof(HostConfigApplied)}");
        private static readonly EventId _hostConfigReading = new EventId(0, $"{_codeArea}\\{nameof(HostConfigReading)}");
        private static readonly EventId _hostConfigRead = new EventId(0, $"{_codeArea}\\{nameof(HostConfigRead)}");
        private static readonly EventId _hostConfigEmpty = new EventId(0, $"{_codeArea}\\{nameof(HostConfigEmpty)}");
        private static readonly EventId _hostConfigNotFound = new EventId(0, $"{_codeArea}\\{nameof(HostConfigNotFound)}");
        private static readonly EventId _hostConfigCreationFailed = new EventId(0, $"{_codeArea}\\{nameof(HostConfigCreationFailed)}");
        private static readonly EventId _hostConfigFileSystemReadOnly = new EventId(0, $"{_codeArea}\\{nameof(HostConfigFileSystemReadOnly)}");
        private static readonly EventId _debugerManagerUnableToUpdateSentinelFile = new EventId(0, $"{_codeArea}\\{nameof(DebugerManagerUnableToUpdateSentinelFile)}");

        public static void HostConfigApplied(this ILogger logger)
        {
            logger.Log(LogLevel.Debug, _hostConfigApplied, "Host configuration applied.");
        }

        public static void HostConfigReading(this ILogger logger, string hostFilePath)
        {
            logger.Log(LogLevel.Information, _hostConfigReading, "Reading host configuration file '{0}'", hostFilePath);
        }

        public static void HostConfigRead(this ILogger logger, string sanitizedJson)
        {
            logger.Log(LogLevel.Information, _hostConfigRead, $"Host configuration file read:{NewLine}{sanitizedJson}");
        }

        public static void HostConfigEmpty(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _hostConfigEmpty, $"Empty host configuration file found. Creating a default {ScriptConstants.HostMetadataFileName} file.");
        }

        public static void HostConfigNotFound(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _hostConfigNotFound, $"No host configuration file found. Creating a default {ScriptConstants.HostMetadataFileName} file.");
        }

        public static void HostConfigCreationFailed(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _hostConfigCreationFailed, $"Failed to create {ScriptConstants.HostMetadataFileName} file. Host execution will continue.");
        }

        public static void HostConfigFileSystemReadOnly(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _hostConfigFileSystemReadOnly, $"File system is read-only. Skipping {ScriptConstants.HostMetadataFileName} creation.");
        }

        public static void DebugerManagerUnableToUpdateSentinelFile(this ILogger logger, Exception ex)
        {
            logger.Log(LogLevel.Error, _debugerManagerUnableToUpdateSentinelFile, "Unable to update the debug sentinel file.");
        }
    }
}
