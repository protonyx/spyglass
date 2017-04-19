using System;
using System.Collections.Generic;
using System.Text;

namespace Spyglass.Core.Metrics
{
    /// <summary>
    /// Measure the rate at which a set of events occur.
    /// </summary>
    public interface IMeterMetric : IMetric
    {
        /// <summary>
        /// Mark the occurence of an event.
        /// </summary>
        void Mark();

        /// <summary>
        /// Mark the occurence of multiple events.
        /// </summary>
        /// <param name="amount">Number of events</param>
        void Mark(long amount);

        /// <summary>
        /// Mark the occurence of an event in a set.
        /// </summary>
        /// <param name="item">Item from the set for which to record the event</param>
        void Mark(string item);

        /// <summary>
        /// Mark the occurence of multiple events in a set.
        /// </summary>
        /// <param name="item">Item from the set for which to record the events</param>
        /// <param name="amount">Number of events</param>
        void Mark(string item, long amount);
    }
}
