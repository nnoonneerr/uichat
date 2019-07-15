using System.Collections.Generic;
using UIWidgetsGallery.gallery;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;

namespace uichat
{
    /// displays a photo and any additional information about a profile
    public class ProfileScreen : StatelessWidget
    {
        private Profile _profile;

        public ProfileScreen(Profile profile)
        {
            _profile = profile;
        }

        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                body: new Stack(
                    alignment : Alignment.center,
                    children : new List<Widget>
                    {
                        // background graphics and bottom contents
                        new Column(
                            children: new List<Widget>
                            {
                                new Container(
                                    height: 100.0f,
                                    color: Colors.black26
                                ),
                                new Expanded(
                                    child: new Container(
                                        color : Colors.black12,
                                        child : new Center(
                                            child: new Text(_profile.DisplayName, style: new TextStyle(fontSize: 26))
                                        )
                                    )
                                )
                            }
                        ),
                        /// Profile image
                        /// TODO: make this properly responsive
                        new Positioned(
                            top: 35.0f,
                            child : new CircleAvatar(
                                radius: 75f,
                                backgroundImage: new NetworkImage(_profile.PhotoURL)
                            )
                        ),
                        /// Back button
                        new Positioned(
                            left: 0f,
                            top: 0f,
                            child: new IconButton(
                                color : Colors.white,
                                icon : new Icon(icon: Icons.arrow_back),
                                onPressed: () => { Navigator.of(context).pop(); }
                            )
                        )
                    }
                )
            );
        }

    }
}