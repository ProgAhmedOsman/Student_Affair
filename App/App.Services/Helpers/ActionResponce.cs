using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Service
{
    public class ActionResponse<T> where T : class
    {
        public T Entity { get; private set; }
        public bool Success { get; protected set; }
        public bool NotFoundError { get; protected set; }
        public string Message { get; protected set; }

        private ActionResponse(bool success, bool notFoundError, string message)
        {
            Success = success;
            Message = message;
            NotFoundError = notFoundError;
        }
        private ActionResponse(bool success, bool notFoundError, string message, T entity) : this(success, notFoundError, message)
        {
            Entity = entity;
        }
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="entity">Saved entity.</param>
        /// <returns>Response.</returns>
        public ActionResponse(T entity) : this(true, false, string.Empty, entity)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public ActionResponse(string message) : this(false, false, message, null)
        { }
        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="notFoundError">resource not found.</param>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public ActionResponse(bool notFoundError, string message) : this(false, notFoundError, message, null)
        { }
    }
}
