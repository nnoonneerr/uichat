using System.Collections.Generic;
using RSG;
using UIWidgetsGallery.gallery;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace uichat
{
    /// The list of recent conversations for the logged in user
    public class ConversationsScreen : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            Profile user = App.UserModel.Profile;

            List<int> recents = App.UserModel.GetRecents();

            return new Scaffold(
                appBar: new AppBar(backgroundColor : Colors.black54, title : new Text("Messages")),
                body : new Column(
                    children: new List<Widget>
                    {
                        new Flexible(
                            child: ListView.seperated(
                                physics : new AlwaysScrollableScrollPhysics(),
                                padding : EdgeInsets.all(8.0f),
                                separatorBuilder: (builcContext, index) => new Divider(height: 1f),
                                itemBuilder: (buildContext, index) =>
                                {
                                    Profile otherPerson = App.ProfilesModel.GetProfile(recents[index]);

                                    List<Message> convo = App.UserModel.GetConversation(otherPersonId: otherPerson.Id);

                                    return new ListTile(
                                        leading: new AvatarButton(onTap: () => { this.OnProfileTap(context, otherPerson : otherPerson); }, photoURL : otherPerson.PhotoURL),
                                        title : new Text(otherPerson.DisplayName),
                                        subtitle : new Text(convo[convo.Count - 1].TimeStamp.ToShortDateString()),
                                        onTap: () => { this.OnConversationTap(context, otherPerson); });
                                },
                                itemCount : recents.Count
                            )
                        )
                    }
                )
            );
        }

        /// Load the Chat Screen with the selected profile
        private void OnConversationTap(BuildContext context, Profile otherPerson)
        {
            Navigator.of(context).push(new MaterialPageRoute((buildContext) => new ChatScreen(otherPerson: otherPerson)));
        }

        /// Load the Profile Screen for the selected profile
        private void OnProfileTap(BuildContext context, Profile otherPerson)
        {
            Navigator.of(context).push(new MaterialPageRoute((buildContext) => new ProfileScreen(profile: otherPerson)));
        }
    }
}