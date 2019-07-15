using System;
using System.Collections.Generic;
using uichat.core;
using UnityEngine;

namespace uichat
{
    public class UserModel : Model
    {
        /// This would normally get pulled from an API
        private Profile _profile = new Profile(displayName: "Test Test", id: 99, photoURL: "https://randomuser.me/api/portraits/men/99.jpg");

        public Profile Profile
        {
            get
            {
                return _profile;
            }

            set
            {
                _profile = value;

                notifyListeners();
            }
        }

        private List<int> _recents;

        public List<int> GetRecents()
        {
            if (_recents == null)
            {
                /// we would ideally do this via an asynchronous request and call
                /// notifyListeners() after completion
                _recents = APIService.Fetch<List<int>>("recents.json");
            }

            return _recents;
        }

        /// In reality, these conversations would be stored via an id, with a reference to all participant ids and the messages.
        /// For the sake of this exericse, this is a simpler data setup but will not really function in a real-world scenario
        /// where multiple users are communicating via a shared server
        private Dictionary<int, List<Message>> _conversations = new Dictionary<int, List<Message>>();

        private List<Message> FetchConversation(int otherId)
        {
            /// we would ideally do this via an asynchronous request and call
            /// notifyListeners() after completion
            List<Message> convo = APIService.Fetch<List<Message>>($"{otherId}.json");

            _conversations.Add(otherId, convo);

            return convo;
        }

        public List<Message> GetConversation(int otherPersonId)
        {
            if(_conversations.ContainsKey(otherPersonId))
            {
                return _conversations[otherPersonId];
            }
            else
            {
                return FetchConversation(otherPersonId);
            }
        }

        public void SendMessage(int recipient, string message)
        {
            /// TODO: fix this to add the message to the proper end of the list (it currently adds messages to the top of the stack instead of the bottom)
            /// possibly swap out List for Stack or Queue for the conversations?
            /*
            Message m = new Message(sender: this.Profile.Id, body: message, timeStamp: DateTime.Now);

            // TODO: this should be an async network call,
            // waiting for the server to tell us the message has been delivered
            if (_conversations.ContainsKey(recipient))
            {
                _conversations[recipient].Add(m);
            }
            else
            {
                _conversations.Add(recipient, new List<Message> { m });
            }
            
            notifyListeners();
            */
        }
    }
}