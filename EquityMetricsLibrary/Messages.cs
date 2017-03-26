﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquityMetrics.Utilities {
    public class Messages {
        private Queue<string> _messages = new Queue<string>();
        private static readonly Messages _instance = new Messages();
        public event EventHandler HandleMessage;

        /// <summary>
        /// Prevents a default instance of the <see cref="Messages"/> class from being created.
        /// </summary>
        private Messages() {
        }

        /// <summary>
        /// Gets the instance of the class.
        /// </summary>
        public static Messages Instance {
            get {
                return _instance;
            }
        }

        /// <summary>
        /// Gets the current messages Queue.
        /// </summary>
        public Queue<string> CurrentMessages {
            get {
                return _messages;
            }
        }

        /// <summary>
        /// Notifies any of the subscribers that a new message has been received.
        /// </summary>
        /// <param name="message">The message.</param>
        public void NotifyNewMessage(string message) {
            EventHandler handler = HandleMessage;
            if (handler != null) {
                // This will call the any form that is currently "wired" to the event, notifying them
                // of the new message.
                handler(this, new MessageEventArgs(message));
            }
        }

        /// <summary>
        /// Adds a new messages to the "central" Queue
        /// </summary>
        /// <param name="message">The message.</param>
        public void AddMessage(string message) {
            _messages.Enqueue(message);
            NotifyNewMessage(message);
        }

        /// <summary>
        /// Adds a new messages to the "central" Queue
        /// </summary>
        /// <param name="message">The message.</param>
        public string GetMessage() {
            if (_messages.Count > 0) {
                return _messages.Dequeue();
            } else {
                return "";
            }
        }
    }

    /// <summary>
    /// Special Event Args used to pass the message data to the subscribers.
    /// </summary>
    class MessageEventArgs: EventArgs {
        private string _message = string.Empty;
        public MessageEventArgs(string message) {
            _message = message;
        }

        public String Message {
            get {
                return _message;
            }
        }
    }
}
