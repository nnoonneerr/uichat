using System;
using System.Collections.Generic;
using uichat.core;

namespace uichat
{
    public class ProfilesModel : Model
    {
        private Dictionary<int, Profile> _allProfiles;

        public Profile GetProfile(int id)
        {
            if(_allProfiles == null)
            {
                _allProfiles = APIService.Fetch<Dictionary<int, Profile>>("users.json");
            }


            if(_allProfiles.ContainsKey(id))
            {
                return _allProfiles[id];
            }
            else
            {
                throw new Exception("Profile id was not found in the database.");
            }
        }
    }
}