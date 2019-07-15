using System;
using UIWidgetsGallery.gallery;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;

namespace uichat
{
    /// a CircleAvatar with an onTap callback
    public class AvatarButton : StatelessWidget
    {
        private readonly string _photoURL;
        private readonly Action _onTap;

        public AvatarButton(Action onTap, string photoURL)
        {
            _onTap = onTap;
            _photoURL = photoURL;
        }

        public override Widget build(BuildContext context)
        {
            return new GestureDetector(
                onTap: () => { _onTap.Invoke(); },
                child : new CircleAvatar(
                    backgroundColor: Colors.black54,
                    backgroundImage: new NetworkImage(_photoURL)
                )
            );
        }
    }
}