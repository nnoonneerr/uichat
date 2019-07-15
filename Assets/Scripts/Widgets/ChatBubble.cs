using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Color = Unity.UIWidgets.ui.Color;

namespace uichat
{
    public class ChatBubble : StatelessWidget
    {
        private readonly string message;
        private readonly string time;

        private readonly int sender;

        public ChatBubble(int sender, string message = null, string time = null)
        {
            this.sender = sender;
            this.message = message;
            this.time = time;
        }

        public override Widget build(BuildContext context)
        {
            bool isLoggedInUser = sender == App.UserModel.Profile.Id;

            // messages from the current logged in user show up on the right, others on the left
            MainAxisAlignment align = isLoggedInUser ? MainAxisAlignment.end : MainAxisAlignment.start;

            List<Widget> childList = this.BuildChildList(context, isLoggedInUser);

            // we reverse the widget list so that the bubble appears to the left of the avatar image for the logged in user
            if (isLoggedInUser)
            {
                childList.Reverse();
            }

            return new Row(
                mainAxisAlignment: align,
                children: childList
            );
        }

        private List<Widget> BuildChildList(BuildContext context, bool isLoggedInUser)
        {

            Profile senderProfile = App.ProfilesModel.GetProfile(sender);

            Radius corners = Radius.circular(10.0f);

            /// color differentiation between users in the chat
            Color bg = isLoggedInUser ? Colors.blue.shade100 : Colors.white;

            /// this makes the chat bubble point left or right, depending on user
            BorderRadius radius = isLoggedInUser ?
                BorderRadius.only(
                    topLeft: corners,
                    topRight: corners,
                    bottomLeft: corners
                ) :
                BorderRadius.only(
                    topLeft: corners,
                    topRight: corners,
                    bottomRight: corners
                );

            return new List<Widget>
            {
                /// the image of the sender for this message, which opens the ProfileScreen when tapped
                new AvatarButton(onTap: () =>
                {
                    Navigator.of(context).push(new MaterialPageRoute((buildContext) => new ProfileScreen(profile: senderProfile)));

                }, senderProfile.PhotoURL),
                new Column(
                    children: new List<Widget>
                    {
                        /// the chat bubble container, using border radius and box shadow to create the bubble effect
                        new Container(
                            margin: EdgeInsets.all(3.0f),
                            padding: EdgeInsets.all(8.0f),
                            decoration: new BoxDecoration(
                                boxShadow : new List<BoxShadow>
                                {
                                    new BoxShadow(
                                        blurRadius: 0.6f,
                                        spreadRadius: 1.0f,
                                        color: Colors.black.withOpacity(0.16f)
                                    )
                                },
                                color : bg,
                                borderRadius : radius
                            ),
                            /// display the actual message inside the bubble
                            child : new Text(message)
                        ),
                        // we tack on the date underneath the bubble container
                        new Padding(padding: isLoggedInUser ? EdgeInsets.only(right: 5f) : EdgeInsets.only(left: 5f),
                            child:
                            new Text(time,
                                style : new TextStyle(
                                    color: Colors.black38,
                                    fontSize: 10.0f
                                )
                            )
                        )
                    }
                )
            };
        }
    }
}