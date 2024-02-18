using System;
using System.Collections.Generic;

namespace BankA.Domain.Enums
{
    public static class Colors
    {
        public static string GetRandom()
        {
            Random rand = new Random();
            var r = rand.Next(List.Count);

            return List[r];
        }

        public static string Get(int position)
        {
            var safePosition = position % List.Count;
            return List[safePosition];
        }

        public static readonly List<string> List = new List<string>() { "#87F1FF",
                                                        "#C0F5FA",
                                                        "#E8FCCF",
                                                        "#96E072",
                                                        "#3DA35D",
                                                        "#B3EFB2",
                                                        "#7A9E7E",
                                                        "#FF87AB",
                                                        "#FCC8C2",
                                                        "#F5ECCD",
                                                        "#70A0AF",
                                                        "#A0C1B9",
                                                        "#D6A99A",
                                                        "#92DCE5",
                                                        "#FCA17D",
                                                        "#C7D9B7",
                                                        "#CC7178",
                                                        "#7BB2D9",
                                                        "#BBA0B2",
                                                        "#858AE3",
                                                        "#DFBE99",
                                                        "#BE95C4",
                                                        "#E0B1CB",
                                                        "#92B4F4",
                                                        "#5E7CE2",
                                                        "#EDADC7",
                                                        "#D199B6",
                                                        "#CACAAA",
                                                        "#D0D38F",
                                                        "#F2F3AE",
                                                        "#D7BEA8",
                                                        "#A3B18A"};

    }
}
