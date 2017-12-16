using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager
{

    static private UserManager _instance = new UserManager();

    static public UserManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public UserLocal localUser = new UserLocal();

}
