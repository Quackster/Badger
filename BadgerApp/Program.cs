﻿using Badger;

namespace BadgerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var badge = GetFromServer.ParseBadgeData("b0508Xs35101s14057");//"b0502Xs13181s01014");// s05010s05011s05012s05013s05014s05015s05016s05017s05018");

            badge.Render();

            // Console.WriteLine("Hello, World!");
        }
    }
}