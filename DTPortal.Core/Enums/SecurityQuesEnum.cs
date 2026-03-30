using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTPortal.Core.Enums
{
    // security
    public enum SecurityQuesEnum
    {
        [Description("Which is your favourite car?")]
        favouritecar = 1,
        [Description("Which is your favourite city?")]
        favoritecity = 2,
        [Description("Which is your first college name?")]
        firstcollege=3,
        [Description("What is  maiden name of your mother?")]
        maidenMother =4,
        [Description("Which was your first school ?")]
        firstSchool=5,
        [Description("Who is your favourite actor/actress?")]
        favouriteactor=6
    }

    public enum InputType
    {
        phoneno = 1,
        emailId=2,
        NIN= 3,
        Passport = 4,
        userId=5,
        emiratesId= 6
    }
}
