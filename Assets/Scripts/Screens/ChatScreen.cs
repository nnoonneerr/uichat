using System;
using System.Collections.Generic;
using RSG;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace uichat
{
    /// the visual list of messages between two users
    public class ChatScreen : StatefulWidget
    {
        public readonly Profile otherPerson;

        public ChatScreen(Profile otherPerson)
        {
            this.otherPerson = otherPerson;
        }

        public override State createState()
        {
            return new ChatState();
        }
    }

    public class ChatState : State<ChatScreen>
    {
        public override void initState()
        {
            App.UserModel.addListener(this.Refresh);
        }

        public override void dispose()
        {
            App.UserModel.removeListener(this.Refresh);
        }

        private void Refresh()
        {
            /// this triggers a rebuild
            this.setState();
        }

        public override Widget build(BuildContext context)
        {
            Profile userProfile = App.UserModel.Profile;

            /// grab the messages between the current and other user
            List<Message> messages = App.UserModel.GetConversation(otherPersonId: widget.otherPerson.Id);

            return new Scaffold(
                appBar: new AppBar(backgroundColor : Colors.black54, title : new Text($"Chat with {widget.otherPerson.DisplayName}")),
                body : new Column(
                    children: new List<Widget>
                    {
                        new Flexible(
                            child: ListView.seperated(
                                physics : new AlwaysScrollableScrollPhysics(),
                                padding : EdgeInsets.all(8.0f),
                                reverse : true,
                                itemBuilder: (buildContext, index) =>
                                {
                                    Message m = messages[index];

                                    return new ChatBubble(message: m.Body, time: m.TimeStamp.ToShortTimeString(), sender: m.Sender);
                                },
                                itemCount : messages.Count,
                                separatorBuilder: (buildContext, index) => new SizedBox(height: 5f)
                            )
                        ),
                        new Divider(height: 1.0f),
                        /// the text input 
                        new Container(
                            decoration: new BoxDecoration(color : Theme.of(context).cardColor),
                            child : new TextComposer(this.OnSend)
                        )
                    }
                )
            );
        }

        private void OnSend(string text)
        {
            App.UserModel.SendMessage(recipient: widget.otherPerson.Id, message: text);
        }
    }
}