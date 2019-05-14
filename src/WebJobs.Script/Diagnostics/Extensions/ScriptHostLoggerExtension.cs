// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Microsoft.Azure.WebJobs.Script.Diagnostics.Extensions
{
    internal static class ScriptHostLoggerExtension
    {
        private static readonly string _codeArea = nameof(ScriptHostLoggerExtension).Replace("LoggerExtension", string.Empty);

        private static readonly EventId _hostIdIsSet = new EventId(0, $"{_codeArea}\\{nameof(HostIdIsSet)}");
        private static readonly EventId _startingHost = new EventId(0, $"{_codeArea}\\{nameof(StartingHost)}");
        private static readonly EventId _functionsErrors = new EventId(0, $"{_codeArea}\\{nameof(FunctionsErrors)}");
        private static readonly EventId _addingDescriptorProvidersForAllLanguages = new EventId(0, $"{_codeArea}\\{nameof(AddingDescriptorProvidersForAllLanguages)}");
        private static readonly EventId _addingDescriptorProviderForLanguage = new EventId(0, $"{_codeArea}\\{nameof(AddingDescriptorProviderForLanguage)}");
        private static readonly EventId _creatingDescriptors = new EventId(0, $"{_codeArea}\\{nameof(CreatingDescriptors)}");
        private static readonly EventId _descriptorsCreated = new EventId(0, $"{_codeArea}\\{nameof(DescriptorsCreated)}");
        private static readonly EventId _errorPurgingLogFiles = new EventId(0, $"{_codeArea}\\{nameof(ErrorPurgingLogFiles)}");
        private static readonly EventId _deletingLogDirectory = new EventId(0, $"{_codeArea}\\{nameof(DeletingLogDirectory)}");
        private static readonly EventId _failedToLoadType = new EventId(0, $"{_codeArea}\\{nameof(FailedToLoadType)}");
        private static readonly EventId _configurationError = new EventId(0, $"{_codeArea}\\{nameof(ConfigurationError)}");
        private static readonly EventId _versionRecommendation = new EventId(0, $"{_codeArea}\\{nameof(VersionRecommendation)}");
        private static readonly EventId _scriptHostInitialized = new EventId(0, $"{_codeArea}\\{nameof(ScriptHostInitialized)}");
        private static readonly EventId _scriptHostStarted = new EventId(0, $"{_codeArea}\\{nameof(ScriptHostStarted)}");

        public static void HostIdIsSet(this ILogger logger)
        {
            logger.Log(LogLevel.Warning, _hostIdIsSet, "Host id explicitly set in configuration. This is not a recommended configuration and may lead to unexpected behavior.");
        }

        public static void StartingHost(this ILogger logger, string hostId, string instanceId, string version, bool inDebugMode, bool inDiagnosticMode, string extensionVersion)
        {
            logger.Log(LogLevel.Information, _startingHost, $"Starting Host (HostId={hostId}, InstanceId={instanceId}, Version={version}, ProcessId={Process.GetCurrentProcess().Id}, AppDomainId={AppDomain.CurrentDomain.Id}, InDebugMode={inDebugMode}, InDiagnosticMode={inDiagnosticMode}, FunctionsExtensionVersion={extensionVersion})");
        }

        public static void FunctionsErrors(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Error, _functionsErrors, message);
        }

        public static void AddingDescriptorProvidersForAllLanguages(this ILogger logger)
        {
            logger.Log(LogLevel.Debug, _addingDescriptorProvidersForAllLanguages, "Adding Function descriptor providers for all languages.");
        }

        public static void AddingDescriptorProviderForLanguage(this ILogger logger, string workerRuntime)
        {
            logger.Log(LogLevel.Debug, _addingDescriptorProviderForLanguage, $"Adding Function descriptor provider for language {workerRuntime}.");
        }

        public static void CreatingDescriptors(this ILogger logger)
        {
            logger.Log(LogLevel.Debug, _creatingDescriptors, "Creating function descriptors.");
        }

        public static void DescriptorsCreated(this ILogger logger)
        {
            logger.Log(LogLevel.Debug, _descriptorsCreated, "Function descriptors created.");
        }

        public static void ErrorPurgingLogFiles(this ILogger logger, Exception ex)
        {
            logger.Log(LogLevel.Error, _errorPurgingLogFiles, ex, "An error occurred while purging log files");
        }

        public static void DeletingLogDirectory(this ILogger logger, string logDir)
        {
            logger.Log(LogLevel.Debug, _deletingLogDirectory, $"Deleting log directory '{logDir}'");
        }

        public static void FailedToLoadType(this ILogger logger, string typeName, string path)
        {
            logger.Log(LogLevel.Warning, _failedToLoadType, $"Failed to load type '{typeName}' from '{path}'");
        }

        public static void ConfigurationError(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Information, _configurationError, message);
        }

        public static void VersionRecommendation(this ILogger logger, string extensionVersion)
        {
            string message = $"Site extension version currently set to '{extensionVersion}'. " +
                        $"It is recommended that you target a major version (e.g. ~2) to avoid unintended upgrades. " +
                        $"You can change that value by updating the '{EnvironmentSettingNames.FunctionsExtensionVersion}' App Setting.";

            logger.Log(LogLevel.Warning, _versionRecommendation, message);
        }

        public static void ScriptHostInitialized(this ILogger logger, long ms)
        {
            logger.Log(LogLevel.Information, _scriptHostInitialized, $"Host initialized ({ms}ms)");
        }

        public static void ScriptHostStarted(this ILogger logger, long ms)
        {
            logger.Log(LogLevel.Information, _scriptHostStarted, $"Host started ({ms}ms)");
        }
    }
}
