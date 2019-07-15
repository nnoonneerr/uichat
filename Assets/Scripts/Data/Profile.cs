using System;

namespace uichat
{
    [Serializable]
    public class Profile
    {
        public string DisplayName;

        // using an int here will definitely not scale well. consider maybe guid or an alternative value type
        public int Id;
        public string PhotoURL;

        public Profile(string displayName, int id, string photoURL)
        {
            this.DisplayName = displayName;
            this.Id = id;
            this.PhotoURL = photoURL;
        }
    }
}