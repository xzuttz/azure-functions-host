// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Azure.WebJobs.Script.Properties;
using Microsoft.Extensions.Logging;

namespace Microsoft.Azure.WebJobs.Script.Diagnostics.Extensions
{
    internal static class LoggerExtension
    {
        private static readonly EventId _extensionsManagerRestoring = new EventId(0, $"{nameof(ExtensionsManagerRestoring)}");
        private static readonly EventId _extensionsManagerRestoreSucceeded = new EventId(0, $"{nameof(ExtensionsManagerRestoreSucceeded)}");

        private static readonly EventId _scriptStartUpErrorLoadingExtensionBundle = new EventId(0, $"{nameof(ScriptStartUpErrorLoadingExtensionBundle)}");
        private static readonly EventId _scriptStartUpLoadingExtensionBundle = new EventId(0, $"{nameof(ScriptStartUpLoadingExtensionBundle)}");
        private static readonly EventId _scriptStartUpLoadingStartUpExtension = new EventId(0, $"{nameof(ScriptStartUpLoadingStartUpExtension)}");
        private static readonly EventId _scriptStartUpBelongExtension = new EventId(0, $"{nameof(ScriptStartUpBelongExtension)}");
        private static readonly EventId _scriptStartUpUnableToLoadExtension = new EventId(0, $"{nameof(ScriptStartUpUnableToLoadExtension)}");
        private static readonly EventId _scriptStartUpTypeIsNotValid = new EventId(0, $"{nameof(ScriptStartUpTypeIsNotValid)}");
        private static readonly EventId _scriptStartUpUnableParseMetadataMissingProperty = new EventId(0, $"{nameof(ScriptStartUpUnableParseMetadataMissingProperty)}");
        private static readonly EventId _scriptStartUpUnableParseMetadata = new EventId(0, $"{nameof(ScriptStartUpUnableParseMetadata)}");

        private static readonly EventId _packageManagerStartingPackagesRestore = new EventId(0, $"{nameof(PackageManagerStartingPackagesRestore)}");
        private static readonly EventId _packageManagerRestoreFailed = new EventId(0, $"{nameof(PackageManagerRestoreFailed)}");
        private static readonly EventId _packageManagerProcessDataReceived = new EventId(0, $"{nameof(PackageManagerProcessDataReceived)}");

        private static readonly EventId _debugManagerUnableToUpdateSentinelFile = new EventId(0, $"{nameof(DebugManagerUnableToUpdateSentinelFile)}");

        private static readonly EventId _functionMetadataManagerLoadingFunctionsMetadata = new EventId(0, $"{nameof(FunctionMetadataManagerLoadingFunctionsMetadata)}");
        private static readonly EventId _functionMetadataManagerFunctionsLoaded = new EventId(0, $"{nameof(FunctionMetadataManagerFunctionsLoaded)}");

        private static readonly EventId _primaryHostCoordinatorLockLeaseAcquired = new EventId(0, $"{nameof(PrimaryHostCoordinatorLockLeaseAcquired)}");
        private static readonly EventId _primaryHostCoordinatorFailedToRenewLockLease = new EventId(0, $"{nameof(PrimaryHostCoordinatorFailedToRenewLockLease)}");
        private static readonly EventId _primaryHostCoordinatorFailedToAcquireLockLease = new EventId(0, $"{nameof(PrimaryHostCoordinatorFailedToAcquireLockLease)}");
        private static readonly EventId _primaryHostCoordinatorReleasedLocklLease = new EventId(0, $"{nameof(PrimaryHostCoordinatorReleasedLocklLease)}");

        private static readonly EventId _autoRecoveringFileSystemWatcherFailureDetected = new EventId(0, $"{nameof(AutoRecoveringFileSystemWatcherFailureDetected)}");
        private static readonly EventId _autoRecoveringFileSystemWatcherRecoveryAborted = new EventId(0, $"{nameof(AutoRecoveringFileSystemWatcherRecoveryAborted)}");
        private static readonly EventId _autoRecoveringFileSystemWatcherRecovered = new EventId(0, $"{nameof(AutoRecoveringFileSystemWatcherRecovered)}");
        private static readonly EventId _autoRecoveringFileSystemWatcherAttemptingToRecover = new EventId(0, $"{nameof(AutoRecoveringFileSystemWatcherAttemptingToRecover)}");
        private static readonly EventId _autoRecoveringFileSystemWatcherUnableToRecover = new EventId(0, $"{nameof(AutoRecoveringFileSystemWatcherUnableToRecover)}");

        public static void ExtensionsManagerRestoring(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _extensionsManagerRestoring, "Restoring extension packages");
        }

        public static void ExtensionsManagerRestoreSucceeded(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _extensionsManagerRestoreSucceeded, "Extensions packages restore succeeded.");
        }

        public static void ScriptStartUpErrorLoadingExtensionBundle(this ILogger logger)
        {
            logger.Log(LogLevel.Error, _scriptStartUpErrorLoadingExtensionBundle, "Unable to find or download extension bundle");
        }

        public static void ScriptStartUpLoadingExtensionBundle(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _scriptStartUpLoadingExtensionBundle, "Loading Extension bundle from {0}");
        }

        public static void ScriptStartUpLoadingStartUpExtension(this ILogger logger, string startupExtensionName)
        {
            logger.Log(LogLevel.Information, _scriptStartUpLoadingStartUpExtension, $"Loading startup extension '{startupExtensionName}'");
        }

        public static void ScriptStartUpBelongExtension(this ILogger logger, string typeName)
        {
            logger.Log(LogLevel.Warning, _scriptStartUpBelongExtension, $"The extension startup type '{typeName}' belongs to a builtin extension");
        }

        public static void ScriptStartUpUnableToLoadExtension(this ILogger logger, string startupExtensionName, string typeName)
        {
            logger.Log(LogLevel.Warning, _scriptStartUpUnableToLoadExtension, $"Unable to load startup extension '{startupExtensionName}' (Type: '{typeName}'). The type does not exist. Please validate the type and assembly names.");
        }

        public static void ScriptStartUpTypeIsNotValid(this ILogger logger, string typeName, string className)
        {
            logger.Log(LogLevel.Warning, _scriptStartUpTypeIsNotValid, $"Type '{typeName}' is not a valid startup extension. The type does not implement {className}.");
        }

        public static void ScriptStartUpUnableParseMetadataMissingProperty(this ILogger logger, string metadataFilePath)
        {
            logger.Log(LogLevel.Error, _scriptStartUpUnableParseMetadataMissingProperty, $"Unable to parse extensions metadata file '{metadataFilePath}'. Missing 'extensions' property.");
        }

        public static void ScriptStartUpUnableParseMetadata(this ILogger logger, Exception ex, string metadataFilePath)
        {
            logger.Log(LogLevel.Error, _scriptStartUpUnableParseMetadata, $"Unable to parse extensions metadata file '{metadataFilePath}'");
        }

        public static void PackageManagerStartingPackagesRestore(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _packageManagerStartingPackagesRestore, "Starting packages restore");
        }

        public static void PackageManagerRestoreFailed(this ILogger logger, Exception ex, string functionDirectory, string projectPath, string nugetHome, string nugetFilePath, string currentLockFileHash)
        {
            string message = $@"Package restore failed with message: '{ex.Message}'
Function directory: {functionDirectory}
Project path: {projectPath}
Packages path: {nugetHome}
Nuget client path: {nugetFilePath}
Lock file hash: {currentLockFileHash}";

            logger.Log(LogLevel.Error, _packageManagerRestoreFailed, message);
        }

        public static void PackageManagerProcessDataReceived(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Information, _packageManagerProcessDataReceived, message);
        }

        public static void DebugManagerUnableToUpdateSentinelFile(this ILogger logger, Exception ex)
        {
            logger.Log(LogLevel.Error, _debugManagerUnableToUpdateSentinelFile, "Unable to update the debug sentinel file.");
        }

        public static void FunctionMetadataManagerLoadingFunctionsMetadata(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _functionMetadataManagerLoadingFunctionsMetadata, "Loading functions metadata");
        }

        public static void FunctionMetadataManagerFunctionsLoaded(this ILogger logger, int count)
        {
            logger.Log(LogLevel.Information, _functionMetadataManagerFunctionsLoaded, $"{count} functions loaded");
        }

        public static void PrimaryHostCoordinatorLockLeaseAcquired(this ILogger logger, string websiteInstanceId)
        {
            logger.Log(LogLevel.Information, _primaryHostCoordinatorLockLeaseAcquired, $"Host lock lease acquired by instance ID '{websiteInstanceId}'.");
        }

        public static void PrimaryHostCoordinatorFailedToRenewLockLease(this ILogger logger, string reason)
        {
            logger.Log(LogLevel.Information, _primaryHostCoordinatorFailedToRenewLockLease, $"Failed to renew host lock lease: {reason}");
        }

        public static void PrimaryHostCoordinatorFailedToAcquireLockLease(this ILogger logger, string websiteInstanceId, string reason)
        {
            logger.Log(LogLevel.Debug, _primaryHostCoordinatorFailedToAcquireLockLease, $"Host instance '{websiteInstanceId}' failed to acquire host lock lease: {reason}");
        }

        public static void PrimaryHostCoordinatorReleasedLocklLease(this ILogger logger, string websiteInstanceId)
        {
            logger.Log(LogLevel.Debug, _primaryHostCoordinatorReleasedLocklLease, $"Host instance '{websiteInstanceId}' released lock lease.");
        }

        public static void AutoRecoveringFileSystemWatcherFailureDetected(this ILogger logger, string errorMessage, string path)
        {
            logger.Log(LogLevel.Warning, _autoRecoveringFileSystemWatcherFailureDetected, $"Failure detected '{errorMessage}'. Initiating recovery... (path: '{path}')");
        }

        public static void AutoRecoveringFileSystemWatcherRecoveryAborted(this ILogger logger, string path)
        {
            logger.Log(LogLevel.Error, _autoRecoveringFileSystemWatcherRecoveryAborted, $"Recovery process aborted. (path: '{path}')");
        }

        public static void AutoRecoveringFileSystemWatcherRecovered(this ILogger logger, string path)
        {
            logger.Log(LogLevel.Information, _autoRecoveringFileSystemWatcherRecovered, $"File watcher recovered. (path: '{path}')");
        }

        public static void AutoRecoveringFileSystemWatcherAttemptingToRecover(this ILogger logger, string path)
        {
            logger.Log(LogLevel.Warning, _autoRecoveringFileSystemWatcherAttemptingToRecover, $"Attempting to recover... (path: '{path}')");
        }

        public static void AutoRecoveringFileSystemWatcherUnableToRecover(this ILogger logger, Exception ex, string path)
        {
            logger.Log(LogLevel.Error, _autoRecoveringFileSystemWatcherUnableToRecover, $"Unable to recover (path: '{path}')");
        }
    }
}
