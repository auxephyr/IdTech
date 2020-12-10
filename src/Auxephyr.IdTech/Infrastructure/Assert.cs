using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

// ReSharper disable UnusedMember.Global

namespace Auxephyr.IdTech.Infrastructure
{
    /// <summary>
    /// Quick and dirty assertion helper class.
    /// </summary>
    [DebuggerStepThrough]
    internal static class Assert
    {
        /// <summary>
        /// Assert that a particular condition is true.
        /// </summary>
        public static void That(Func<bool> assertion, string message)
        {
            if (!assertion())
                throw new Exception(message);
        }

        /// <summary>
        /// Assert that a collection contains a specific value.
        /// </summary>
        public static void Contains<T>(ICollection<T> subject, T item, string message) => 
            That(() => subject.Contains(item), message);

        /// <summary>
        /// Assert that an object is not null.
        /// </summary>
        public static void IsNotNull(object subject, string message) => 
            That(() => subject != null, message);
        
        /// <summary>
        /// Assert that a string is not null or empty.
        /// </summary>
        public static void IsNotNullOrEmpty(string subject, string message) => 
            That(() => !string.IsNullOrEmpty(subject), message);

        /// <summary>
        /// Assert that an enum contains a value.
        /// </summary>
        public static void IsDefinedIn<TEnum>(object subject, string message) =>
            That(() => Enum.GetValues(typeof(TEnum)).Cast<object>().Contains(subject), message);
    }
}