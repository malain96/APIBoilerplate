using System;
using System.Collections.Generic;

namespace API.Shared.DTOs
{
    /// <summary>
    /// Error message returned by the API
    /// </summary>
    [Serializable]
    public class ErrorResponse
    {
        /// <summary>
        /// Contructor for a single message (string)
        /// </summary>
        /// <param name="status">Status code of the error</param>
        /// <param name="message">Message</param>
        public ErrorResponse(int status, string message)
        {
            Status = status;
            Messages = new List<string>();

            if (!string.IsNullOrWhiteSpace(message)) Messages.Add(message);
        }

        /// <summary>
        /// Contructor for multiple messages (list)
        /// </summary>
        /// <param name="status">Status code of the error</param>
        /// <param name="messages">Message list</param>
        public ErrorResponse(int status, List<string> messages)
        {
            Messages = messages ?? new List<string>();
            Status = status;
        }

        /// <summary>
        /// Status code of the error
        /// </summary>
        public int Status { get; }

        /// <summary>
        /// List of messages related to the error
        /// </summary>
        public List<string> Messages { get; }
    }
}