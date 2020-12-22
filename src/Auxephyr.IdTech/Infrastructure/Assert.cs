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
        public static void Contains<T>(ICollection<T> subject, T item, string name) => 
            That(() => subject.Contains(item), $"Var '{name}' does not contain the specified item.");

        /// <summary>
        /// Assert that an object is not null.
        /// </summary>
        public static void IsNotNull(object subject, string name) => 
            That(() => subject != null, $"Var '{name}' is null.");
        
        /// <summary>
        /// Assert that a string is not null or empty.
        /// </summary>
        public static void IsNotNullOrEmpty(string subject, string name) => 
            That(() => !string.IsNullOrEmpty(subject), $"Var '{name}' is null or empty.");

        /// <summary>
        /// Assert that an enum contains a value.
        /// </summary>
        public static void IsDefinedIn<TEnum>(object subject) =>
            That(() => Enum.GetValues(typeof(TEnum)).Cast<object>().Contains(subject), $"Value '{subject}' is not valid for {typeof(TEnum).Name}.");

        /// <summary>
        /// Assert that an array has a specific length.
        /// </summary>
        public static void Length(Array array, int count, string name) =>
            That(() => array?.Length == count, $"Var '{name}' does not have expected length {count}.");
    }
}