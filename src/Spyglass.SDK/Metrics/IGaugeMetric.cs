﻿namespace Spyglass.SDK.Metrics
{
    /// <summary>
    /// A metric whose value can change
    /// </summary>
    public interface IGaugeMetric : IMetric, IMetricValueProvider<double>
    {
        /// <summary>
        /// Set the value of the metric
        /// </summary>
        /// <param name="value">value to set</param>
        void Set(double value);
    }
}