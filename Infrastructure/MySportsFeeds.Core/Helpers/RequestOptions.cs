﻿using MySportsFeeds.Core.Enums;

namespace MySportsFeeds.Core.Helpers
{
    public class RequestOptions
    {
        /// <summary>
        /// Gets or sets for date.
        /// </summary>
        /// <value>
        /// Must be formatted as YYYYMMDD
        /// </value>
        public string ForDate { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public Status? Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RequestOptions"/> is force.
        /// </summary>
        /// <value>
        ///   <c>true</c> if force; otherwise, <c>false</c>.
        /// </value>
        public bool? Force { get; set; }

        /// <summary>
        /// Gets or sets an array of team names.
        /// </summary>
        /// <value>
        /// An appreviated array of team names(eg bos, mil).  THis will filter the list
        /// to the team names included.
        /// </value>
        public string[] Teams { get; set; }

        /// <summary>
        /// Gets or sets the game identifier.
        /// </summary>
        /// <value>
        /// The game identifier.
        /// </value>
        public string GameId { get; set; }

        /// <summary>
        /// Gets or sets the player stats MLB.
        /// </summary>
        /// <value>
        /// The player stats MLB.
        /// </value>
        public string[] PlayerStatsMlb { get; set; }

        /// <summary>
        /// Gets or sets the team stats MLB.
        /// </summary>
        /// <value>
        /// The team stats MLB.
        /// </value>
        public string[] TeamStatsMlb { get; set; }
    }
}