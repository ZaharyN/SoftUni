using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boardgames.Constraints
{
    public class Constants
    {
        //Boardgame

        public const int BoardgameNameMinLength = 10;
        public const int BoardgameNameMaxLength = 20;

        public const double BoardgameMinRating = 1;
        public const double BoardgameMaxRating = 10;

        public const int BoardgameStartYear = 2018;
        public const int BoardgameEndYear = 2023;

        //Seller
        public const int SellerNameMinLength = 5;
        public const int SellerNameMaxLength = 20;

        public const int SellerAddressMinLength = 2;
        public const int SellerAddressMaxLength = 30;

        public const string SellerWebsiteRegEx = @"^www.[A-Za-z-]+\.com";

        //Creator
        public const int CreatorFirstNameMinLength = 2;
        public const int CreatorFirstNameMaxLength = 7;

        public const int CreatorLastNameMinLength = 2;
        public const int CreatorLastNameMaxLength = 7;

    }
}
