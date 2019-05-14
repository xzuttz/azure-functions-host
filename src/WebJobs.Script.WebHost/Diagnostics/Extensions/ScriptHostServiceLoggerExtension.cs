// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Microsoft.Azure.WebJobs.Script.WebHost.Diagnostics.Extensions
{
    public static class ScriptHostServiceLoggerExtension
    {
        private static readonly string _codeArea = nameof(ScriptHostServiceLoggerExtension).Replace("LoggerExtension", string.Empty);

        private static readonly EventId _scriptHostServiceInitCanceledByRuntime = new EventId(0, $"{_codeArea}\\{nameof(ScriptHostServiceInitCanceledByRuntime)}");
        private static readonly EventId _unehealthyCountExceeded = new EventId(0, $"{_codeArea}\\{nameof(UnhealthyCountExceeded)}");
        private static readonly EventId _offline = new EventId(0, $"{_codeArea}\\{nameof(Offline)}");
        private static readonly EventId _initializing = new EventId(0, $"{_codeArea}\\{nameof(Initializing)}");
        private static readonly EventId _initialization = new EventId(0, $"{_codeArea}\\{nameof(Initialization)}");
        private static readonly EventId _inStandByMode = new EventId(0, $"{_codeArea}\\{nameof(InStandByMode)}");
        private static readonly EventId _unhealthyRestart = new EventId(0, $"{_codeArea}\\{nameof(UnhealthyRestart)}");
        private static readonly EventId _stopping = new EventId(0, $"{_codeArea}\\{nameof(Stopping)}");
        private static readonly EventId _didNotShutDown = new EventId(0, $"{_codeArea}\\{nameof(DidNotShutDown)}");
        private static readonly EventId _shutDownCompleted = new EventId(0, $"{_codeArea}\\{nameof(ShutDownCompleted)}");
        private static readonly EventId _skipRestart = new EventId(0, $"{_codeArea}\\{nameof(SkipRestart)}");
        private static readonly EventId _restarting = new EventId(0, $"{_codeArea}\\{nameof(Restarting)}");
        private static readonly EventId _restarted = new EventId(0, $"{_codeArea}\\{nameof(Restarted)}");
        private static readonly EventId _building = new EventId(0, $"{_codeArea}\\{nameof(Building)}");
        private static readonly EventId _startupWasCanceled = new EventId(0, $"{_codeArea}\\{nameof(StartupWasCanceled)}");
        private static readonly EventId _errorOccured = new EventId(0, $"{_codeArea}\\{nameof(ErrorOccured)}");
        private static readonly EventId _errorOccuredInactive = new EventId(0, $"{_codeArea}\\{nameof(ErrorOccuredInactive)}");
        private static readonly EventId _cancellationRequested = new EventId(0, $"{_codeArea}\\{nameof(CancellationRequested)}");

        public static void ScriptHostServiceInitCanceledByRuntime(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _scriptHostServiceInitCanceledByRuntime, "Initialization cancellation requested by runtime.");
        }

        public static void UnhealthyCountExceeded(this ILogger logger, int healthCheckThreshold, TimeSpan healthCheckWindow)
        {
            logger.Log(LogLevel.Error, _unehealthyCountExceeded, $"Host unhealthy count exceeds the threshold of {healthCheckThreshold} for time window {healthCheckWindow}. Initiating shutdown.");
        }

        public static void Offline(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _offline, "Host is offline.");
        }

        public static void Initializing(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _initializing, "Initializing Host.");
        }

        public static void Initialization(this ILogger logger, int attemptCount, int startCount)
        {
            logger.Log(LogLevel.Information, _initialization, $"Host initialization: ConsecutiveErrors={attemptCount}, StartupCount={startCount}");
        }

        public static void InStandByMode(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _inStandByMode, "Host is in standby mode");
        }

        public static void UnhealthyRestart(this ILogger logger)
        {
            logger.Log(LogLevel.Error, _unhealthyRestart, "Host is unhealthy. Initiating a restart.");
        }

        public static void Stopping(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _stopping, "Stopping host...");
        }

        public static void DidNotShutDown(this ILogger logger)
        {
            logger.Log(LogLevel.Warning, _didNotShutDown, "Host did not shutdown within its allotted time.");
        }

        public static void ShutDownCompleted(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _shutDownCompleted, "Host shutdown completed.");
        }

        public static void SkipRestart(this ILogger logger, ScriptHostState state)
        {
            logger.Log(LogLevel.Debug, _skipRestart, $"Host restart was requested, but current host state is '{state}'. Skipping restart.");
        }

        public static void Restarting(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _restarting, "Restarting host.");
        }

        public static void Restarted(this ILogger logger)
        {
            logger.Log(LogLevel.Information, _restarted, "Host restarted.");
        }

        public static void Building(this ILogger logger, bool skipHostStartup, bool skipHostJsonConfiguration)
        {
            logger.Log(LogLevel.Information, _building, $"Building host: startup suppressed:{skipHostStartup}, configuration suppressed: {skipHostJsonConfiguration}");
        }

        public static void StartupWasCanceled(this ILogger logger)
        {
            logger.Log(LogLevel.Debug, _startupWasCanceled, "Host startup was canceled.");
        }

        public static void ErrorOccured(this ILogger logger, Exception ex)
        {
            logger.Log(LogLevel.Debug, _errorOccured, ex, "A host error has occurred");
        }

        public static void ErrorOccuredInactive(this ILogger logger, Exception ex)
        {
            logger.Log(LogLevel.Debug, _errorOccuredInactive, ex, "A host error has occurred on an inactive host");
        }

        public static void CancellationRequested(this ILogger logger)
        {
            logger.Log(LogLevel.Debug, _cancellationRequested, "Cancellation requested. A new host will not be started.");
        }
    }
}
