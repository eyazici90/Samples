﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlexGateway_v3.Gateway.Shared
{
    public static class Settings

    {
        public static string PGW_CORRELATION_ID { get; } = $"PGW-Correlation-Id";

        public static string PGW_CACHE_HEADER { get; } = $"PGW-Cache-X";

        public static string PGW_QUEUE_HEADER { get; } = $"PGW-Queue-X";

        public static string PGW_HEALTHCHECK_URL { get; } = $"/healthcheck";

        public static string PGW_IDEMPOTANCY_HEADER { get; } = $"PGW-Idempotancy-X";

        public static string PGW_APPSETTINGS_PATH { get; } = $"appsettings.json";

        public static string PGW_CONFIGURATION_DIRECTORY { get; } = $"configurations";

        public static string PGW_CONFIGURATION_PATH { get; } = $"configuration.json";

        public static int EXCEPTION_ALLOWED_BEFORE_BREAKING { get; } = 3;

        public static int CIRCUIT_BREAKER_OPEN_STATE_DURATION_SECONDS { get; } = 10;
    }
}